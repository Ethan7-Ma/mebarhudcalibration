using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MEB_ARHUD_Calibration.Common
{
    class CommonUtil
    {
        public static string ByteToHexString(byte value)
        {
            string valueStr = value.ToString("x8");
            return valueStr.Substring(valueStr.Length - 2, 2);
        }

        public static string ByteArrToHexString(byte[] values)
        {
            string rlt = "";
            for (int i = 0; i < values.Length; i++)
            {
                rlt += ByteToHexString(values[i]) + " ";
            }
            return rlt;
        }

        public static byte[] StringToByteArray(string str)
        {
            char[] charArr = str.ToCharArray(0, str.Length);
            byte[] rlt = new byte[charArr.Length];
            for (int i = 0; i < charArr.Length; i++)
            {
                char one = charArr[i];
                rlt[i] = (byte)one;
            }
            return rlt;
        }

        public static byte[] HexStringToByteArray(string str)
        {
            string[] strArr = str.Split(' ');
            byte[] rlt = new byte[strArr.Length];

            for (int i = 0; i < strArr.Length; i++)
            {
                string oneHexData = strArr[i];
                rlt[i] = Convert.ToByte(oneHexData, 16);
            }
            return rlt;
        }

        public static int ChangeByteToInt(byte data)
        {
            return data < 128 ? data : data - 256;
        }


    }
}
