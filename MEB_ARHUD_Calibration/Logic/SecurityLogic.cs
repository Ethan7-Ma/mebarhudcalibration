using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Management;

namespace MEB_ARHUD_Calibration.Logic
{
    class SecurityLogic
    {

        private List<byte[]> registeredMacs = new List<byte[]>() { 
                                                                   new byte[] { 0x00, 0x2b, 0x67, 0xcb, 0x5d, 0xf0 },
                                                                 };

        private List<byte[]> GetLocalMac()
        {
            List<byte[]> macs = new List<byte[]>();

            try
            {
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface ni in interfaces)
                {
                    macs.Add(ni.GetPhysicalAddress().GetAddressBytes());
                }
            }
            catch (Exception)
            {

            }
            return macs;
        }

        public bool CheckRegistered()
        {
            List<byte[]> macs = GetLocalMac();

            foreach (byte[] currentMac in macs)
            {
                foreach (byte[] targetMac in registeredMacs)
                {
                    if (CheckMacEquals(currentMac, targetMac))
                        return true;
                }
            }

            return false;
        }

        private bool CheckMacEquals(byte[] bytes1, byte[] bytes2)
        {
            if (bytes1.Length != bytes2.Length)
                return false;

            for (int i = 0; i < bytes1.Length; i++)
            {
                if (bytes1[i] != bytes2[i])
                    return false;
            }
            return true;
        }
    }
}
