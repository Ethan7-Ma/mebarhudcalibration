using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace MEB_ARHUD_Calibration.Common
{
    class CryptoUtil
    {
        static string KEY = "mebarhud";

        public static string DesEncode(string plain)
        {
            byte[] bytesCipher;
            byte[] keyBytes = Encoding.UTF8.GetBytes(KEY);
            byte[] ivBytes = keyBytes;

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                CryptoStream csEncrypt = new CryptoStream(msEncrypt, des.CreateEncryptor(keyBytes, ivBytes),
                    CryptoStreamMode.Write);
                StreamWriter swEncrypt = new StreamWriter(csEncrypt);
                swEncrypt.WriteLine(plain);
                swEncrypt.Close();
                csEncrypt.Close();
                bytesCipher = msEncrypt.ToArray();
                msEncrypt.Close();
            }

            return Convert.ToBase64String(bytesCipher);
        }

        public static string DesDeCode(string cipher)
        {
            string strPlainText = "";
            byte[] cipherByte = new byte[0];
            try
            {
                cipherByte = Convert.FromBase64String(cipher);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            byte[] keyBytes = Encoding.UTF8.GetBytes(KEY);
            byte[] ivBytes = keyBytes;
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            using (MemoryStream msDecrypt = new MemoryStream(cipherByte))
            {
                CryptoStream csDecrypt = null;
                StreamReader srDecrypt = null;
                try
                {
                    csDecrypt = new CryptoStream(msDecrypt, des.CreateDecryptor(keyBytes, ivBytes), CryptoStreamMode.Read);
                    srDecrypt = new StreamReader(csDecrypt);
                    strPlainText = srDecrypt.ReadLine();
                }
                finally
                {
                    srDecrypt.Close();
                    csDecrypt.Close();
                    msDecrypt.Close();
                }
            }

            return strPlainText;
        }
    }
}
