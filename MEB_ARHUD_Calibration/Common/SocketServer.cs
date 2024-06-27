using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace MEB_ARHUD_Calibration.Common
{
    public class SocketServer
    {
        public SocketServer()
        {
            StartListenSocket();
        }

        public SocketServer(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
            StartListenSocket();

            Thread t_CheckConnect = new Thread(CheckConnectStateThread);
            t_CheckConnect.IsBackground = true;
            t_CheckConnect.Start();
        }

        public event Action<string> ReceiveMessageEvent = null;
        public event Action<byte[]> ReceiveDatasEvent = null;

        private Socket socketWatch = null;
        private TimeSpan timeSpan;
        private string ip = "172.22.24.4";
        private int port = 2000;
        private int PLCConnectFlag = 0;


        public bool RobotConnected => PLCConnectFlag > 0;

        public TimeSpan TimeSpan
        {
            get { return timeSpan; }
            set { timeSpan = value; }
        }

        public string IP
        {
            get { return ip; }
            set { ip = value; }
        }

        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        private void StartListenSocket()
        {
            try
            {
                socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress address = IPAddress.Parse(ip);
                IPEndPoint point = new IPEndPoint(address, port);
                socketWatch.Bind(point);
                socketWatch.Listen(20);

                Thread threadwatch = new Thread(WatchConnecting);
                threadwatch.IsBackground = true;
                threadwatch.Start();
                Console.WriteLine("开启监听 " + address + ":" + port);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void CheckConnectStateThread()
        {
            while (true)
            {
                Thread.Sleep(1000);
                PLCConnectFlag--;
            }
        }

        private void WatchConnecting()
        {
            Socket connection = null;

            while (true)
            {
                try
                {
                    connection = socketWatch.Accept();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }

                IPAddress clientIP = (connection.RemoteEndPoint as IPEndPoint).Address;
                int clientPort = (connection.RemoteEndPoint as IPEndPoint).Port;

                string remoteEndPoint = connection.RemoteEndPoint.ToString();
                Console.WriteLine("成功与" + remoteEndPoint + "客户端建立连接！\t\n");
                IPEndPoint netpoint = connection.RemoteEndPoint as IPEndPoint;
                clients.Add(connection);

                ParameterizedThreadStart pts_Rec = new ParameterizedThreadStart(Recv);
                Thread thread_Rec = new Thread(pts_Rec);
                thread_Rec.IsBackground = true;
                thread_Rec.Start(connection);

                ParameterizedThreadStart pts_conn = new ParameterizedThreadStart(CheckConnect);
                Thread thread_conn = new Thread(pts_conn);
                thread_conn.IsBackground = true;
                thread_conn.Start(connection);
            }
        }

        List<Socket> clients = new List<Socket>();

        private void CheckConnect(object socketclientpara)
        {
            Socket socketServer = socketclientpara as Socket;

            while (true)
            {
                if (socketServer == null || !socketServer.Connected)
                    break;
                try
                {
                    PLCConnectFlag = 25;
                    if (socketServer.Poll(-1, SelectMode.SelectRead))
                    {
                        PLCConnectFlag = 0;
                        Console.WriteLine("客户端" + socketServer.RemoteEndPoint + "已经中断连接\r\n");
                        clients.Remove(socketServer);
                        socketServer.Close();
                        break;
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        PLCConnectFlag = 0;
                        Console.WriteLine("客户端" + socketServer.RemoteEndPoint + "已经中断连接" + "\r\n" + ex.Message + "\r\n" + ex.StackTrace + "\r\n");
                        clients.Remove(socketServer);
                        socketServer.Close();
                    }
                    catch
                    {
                    }
                    break;
                }
            }
        }

        private void Recv(object socketclientpara)
        {
            Socket socketServer = socketclientpara as Socket;

            while (true)
            {
                if (socketServer == null || !socketServer.Connected)
                    break;
                try
                {
                    byte[] arrServerRecMsg = new byte[1024 * 1024];
                    int length = socketServer.Receive(arrServerRecMsg);
                    string strSRecMsg = Encoding.UTF8.GetString(arrServerRecMsg, 0, length);


                    if (length == 0)
                        continue;

                    byte[] datas = new byte[length];
                    for (int i = 0; i < length; i++)
                        datas[i] = arrServerRecMsg[i];

                    ReceiveDatasEvent?.Invoke(datas);

                    PLCConnectFlag = 25;
                }
                catch (Exception ex)
                {
                    try
                    {
                        Console.WriteLine("客户端" + socketServer.RemoteEndPoint + "已经中断连接" + "\r\n" + ex.Message + "\r\n" + ex.StackTrace + "\r\n");
                        clients.Remove(socketServer);
                        socketServer.Close();
                    }
                    catch
                    {
                    }
                    break;
                }
            }
        }

        public void SendDatas(byte[] datas)
        {
            try
            {
                List<Socket> ErrorSockets = new List<Socket>();

                foreach (Socket client in clients)
                {
                    try
                    {
                        if (client != null && client.Connected)
                        {
                            int rlt = client.SendTo(datas, client.RemoteEndPoint);
                        }
                    }
                    catch (Exception e)
                    {
                        ErrorSockets.Add(client);
                        Console.WriteLine("SendMessageError " + e.Message);
                    }
                }

                if (ErrorSockets.Count > 0)
                {
                    foreach (Socket errorSocket in ErrorSockets)
                    {
                        clients.Remove(errorSocket);
                        errorSocket.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void SendMessage(string message)
        {
            try
            {
                List<Socket> ErrorSockets = new List<Socket>();

                foreach (Socket client in clients)
                {
                    try
                    {
                        if (client != null && client.Connected)
                        {
                            int rlt = client.SendTo(Encoding.UTF8.GetBytes(message), client.RemoteEndPoint);
                            Console.WriteLine(rlt + " Server -> " + client.RemoteEndPoint + ": " + message);
                        }
                    }
                    catch (Exception e)
                    {
                        ErrorSockets.Add(client);
                        Console.WriteLine("SendMessageError " + e.Message);
                    }
                }

                if (ErrorSockets.Count > 0)
                {
                    foreach (Socket errorSocket in ErrorSockets)
                    {
                        clients.Remove(errorSocket);
                        errorSocket.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private DateTime GetCurrentTime()
        {
            DateTime currentTime = new DateTime();
            currentTime = DateTime.Now;
            return currentTime;
        }

    }
}
