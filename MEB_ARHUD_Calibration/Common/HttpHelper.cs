using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEB_ARHUD_Calibration.Common
{
    public class HttpHelper
    {
        public static string httpGet(string url)
        {
            HttpWebRequest request = null;
            request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

            string retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            myStreamReader = null;
            myResponseStream.Close();
            myResponseStream.Dispose();
            myResponseStream = null;
            request.Abort();
            request = null;

            return retString;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受     
        }

        public static string httpPost(string Url, string postDataStr)
        {
            return httpPost(Url, postDataStr, "txt", "", "utf-8");
        }
        public static string httpPost(string Url)
        {
            return httpPost(Url, "", "txt", "", "utf-8");
        }

        public static string httpPost1(string Url)
        {
            return httpPost(Url, "", "html", "", "utf-8");
        }

        public static string httpPost2(string Url, string postDataStr)
        {
            return httpPost(Url, postDataStr, "html", "", "utf-8");
        }

        public static string httpPost3(string Url, string postDataStr)
        {
            return httpPost(Url, postDataStr, "textjson", "", "utf-8");
        }

        public static string httpPostByJson(string Url, string postDataStr)
        {
            return httpPost(Url, postDataStr, "json", "", "utf-8");
        }



        public static string httpPost(string Url, string postDataStr, string postType, string cacert, string chartset)  //post读取
        {
            //发送
            System.GC.Collect();//系统回收垃圾
            if (Url.Contains("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Timeout = 1000 * 20;
            request.Method = "POST";

            //以下加速请求
            //request.KeepAlive = false;//请求完关闭
            //request.ServicePoint.Expect100Continue = false;
            //request.ServicePoint.UseNagleAlgorithm = false;
            //request.ServicePoint.ConnectionLimit = 65500;
            //request.AllowWriteStreamBuffering = false; 
            request.Proxy = null;
            request.AllowAutoRedirect = true;
            //以上加速请求
            if (postType == "txt")
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }
            else if (postType == "json")
            {
                request.ContentType = "application/json";
            }
            else if (postType == "html")
            {
                request.ContentType = "text/html";
            }
            else if (postType == "textjson")
            {
                request.ContentType = "text/json";
            }

            if (cacert != "")
            {
                X509Certificate cert = new System.Security.Cryptography.X509Certificates.X509Certificate(cacert, "");
                request.ImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;//设定验证回调(总是同意)
                request.ClientCertificates.Add(cert);//把证书添加进http请求中
            }

            try
            {
                Console.WriteLine(postDataStr);

                byte[] payload = System.Text.Encoding.UTF8.GetBytes(postDataStr);
                request.ContentLength = payload.Length;
                request.ServicePoint.Expect100Continue = false;
                request.GetRequestStream().Write(payload, 0, payload.Length);

                //响应接收
                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                HttpWebResponse response;
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                {
                    response = (HttpWebResponse)ex.Response;
                }
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding(chartset));
                string retString = myStreamReader.ReadToEnd();
                response.Close();
                myStreamReader.Close();
                myResponseStream.Close();
                myResponseStream.Dispose();
                response = null;
                myStreamReader = null;
                myResponseStream = null;
                request.Abort();
                request = null;

                //if(retString.Length < 100)
                //    Console.WriteLine(retString);

                return retString;
            }
            catch (Exception ex)
            {
                request.Abort();
                request = null;
                Console.WriteLine(ex.Message);
                //Message ms = new Message("网络超时!");
                return "";
            }
        }

        /// <summary>
        /// Post方式请求接口
        /// </summary>
        /// <param name="action">请求的方法名</param>
        /// <param name="dic">请求发送的数据</param>
        /// <returns></returns>
        public static string HttpPost(string action, Dictionary<string, string> dic)
        {
            //此处换为自己的请求url
            string url = action;
            string result = string.Empty;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            #region 添加Post 参数
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
            byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        public static string PostDataNew(string url, string infor)
        {
            string result = "";
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "POST";
                request.KeepAlive = true;
                request.AllowAutoRedirect = false;
                request.ContentType = "application/x-www-form-urlencoded";
                byte[] postdatabtyes = Encoding.UTF8.GetBytes(infor);
                request.ContentLength = postdatabtyes.Length;
                request.ServicePoint.Expect100Continue = false;//这个在Post的时候，一定要加上，如果服务器返回错误，他还会继续再去请求，不会使用之前的错误数据，做返回数据
                Stream requeststream = request.GetRequestStream();
                requeststream.Write(postdatabtyes, 0, postdatabtyes.Length);
                requeststream.Close();
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader sr2 = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    string respsr = sr2.ReadToEnd();
                    result = respsr;
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public static string RequestData(string POSTURL, string PostData)
        {
            //发送请求的数据
            WebRequest myHttpWebRequest = WebRequest.Create(POSTURL);
            myHttpWebRequest.Method = "POST";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] byte1 = encoding.GetBytes(PostData);
            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            myHttpWebRequest.ContentLength = byte1.Length;

            Stream newStream = myHttpWebRequest.GetRequestStream();
            newStream.Write(byte1, 0, byte1.Length);
            newStream.Close();

            //发送成功后接收返回的XML信息
            HttpWebResponse response = (HttpWebResponse)myHttpWebRequest.GetResponse();
            string lcHtml = string.Empty;
            Encoding enc = Encoding.GetEncoding("UTF-8");
            Stream stream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream, enc);
            lcHtml = streamReader.ReadToEnd();
            return lcHtml;
        }

    }
}
