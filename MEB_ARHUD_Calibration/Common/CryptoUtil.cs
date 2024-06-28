using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MEB_ARHUD_Calibration.Common {
    static class CryptoUtil {
        static readonly string KEY = "mebarhud";

        public static string DesEncode(string plain) {
            byte[] bytesCipher;
            byte[] keyBytes = Encoding.UTF8.GetBytes(KEY);
            byte[] ivBytes = keyBytes;

            DESCryptoServiceProvider des = new();

            using (MemoryStream msEncrypt = new()) {
                CryptoStream csEncrypt = new(msEncrypt, des.CreateEncryptor(keyBytes, ivBytes),
                    CryptoStreamMode.Write);
                StreamWriter swEncrypt = new(csEncrypt);
                swEncrypt.WriteLine(plain);
                swEncrypt.Close();
                csEncrypt.Close();
                bytesCipher = msEncrypt.ToArray();
                msEncrypt.Close();
            }

            return Convert.ToBase64String(bytesCipher);
        }

        public static string DesDeCode(string cipher) {
            string strPlainText = "";
            byte[] cipherByte = Convert.FromBase64String(cipher);
            byte[] keyBytes = Encoding.UTF8.GetBytes(KEY);
            byte[] ivBytes = keyBytes;
            DESCryptoServiceProvider des = new();
            using (MemoryStream msDecrypt = new(cipherByte)) {
                CryptoStream csDecrypt = new(msDecrypt, des.CreateDecryptor(keyBytes, ivBytes), CryptoStreamMode.Read);
                StreamReader srDecrypt = new(csDecrypt);
                strPlainText = srDecrypt.ReadLine();
            }
            return strPlainText;
        }
    }
}
