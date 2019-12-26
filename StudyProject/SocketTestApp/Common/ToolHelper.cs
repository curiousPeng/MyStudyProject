using SocketTestApp.Protocol.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SocketTestApp.Common
{
    public static class ToolHelper
    {

        /// <summary>
        /// 将字符串的每一个字转化为16进制
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string StringToByHexString(string input)
        {
            char[] values = input.ToCharArray();
            string result = string.Empty;
            foreach (char letter in values)
            {
                // Get the integral value of the character.
                int value = Convert.ToInt32(letter);
                // Convert the decimal value to a hexadecimal value in string form.
                string hexOutput = String.Format("{0:X}", value);
                result += hexOutput;
            }
            return result;
        }

        /// <summary>
        /// string 转16进制byte[]
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString = hexString.Insert(hexString.Length - 1, 0.ToString());
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary>
        /// byte[]转16进制
        /// </summary>
        /// <param name="bt"></param>
        /// <returns></returns>
        public static string ByteConvertToHex(byte[] bt)
        {
            var encode = Encoding.UTF8;
            return BitConverter.ToString(bt, 0).Replace("-", string.Empty).ToLower();
        }

        /// <summary>
        /// 将一条十六进制字符串转换为ASCII
        /// </summary>
        /// <param name="hexstring">一条十六进制字符串</param>
        /// <returns>返回一条ASCII码</returns>
        public static string HexStringToASCII(string hexstring)
        {
            byte[] bt = HexStringToBinary(hexstring);
            string lin = "";
            for (int i = 0; i < bt.Length; i++)
            {
                lin = lin + bt[i] + " ";
            }


            string[] ss = lin.Trim().Split(new char[] { ' ' });
            char[] c = new char[ss.Length];
            int a;
            for (int i = 0; i < c.Length; i++)
            {
                a = Convert.ToInt32(ss[i]);
                c[i] = Convert.ToChar(a);
            }

            string b = new string(c);
            return b;
        }

        /// <summary>
        /// 16进制字符串转换为二进制数组
        /// </summary>
        /// <param name="hexstring">用空格切割字符串</param>
        /// <returns>返回一个二进制字符串</returns>
        public static byte[] HexStringToBinary(string hexstring)
        {

            string[] tmpary = hexstring.Trim().Split(' ');
            byte[] buff = new byte[tmpary.Length];
            for (int i = 0; i < buff.Length; i++)
            {
                buff[i] = Convert.ToByte(tmpary[i], 16);
            }
            return buff;
        }

        private static Byte[] ConvertFrom(string strTemp)
        {
            try
            {
                if (Convert.ToBoolean(strTemp.Length & 1))//数字的二进制码最后1位是1则为奇数
                {
                    strTemp = "0" + strTemp;//数位为奇数时前面补0
                }
                Byte[] aryTemp = new Byte[strTemp.Length / 2];
                for (int i = 0; i < (strTemp.Length / 2); i++)
                {
                    aryTemp[i] = (Byte)(((strTemp[i * 2] - '0') << 4) | (strTemp[i * 2 + 1] - '0'));
                }
                return aryTemp;//高位在前
            }
            catch
            { return null; }
        }

        /// <summary>
        /// BCD码转换16进制(压缩BCD)
        /// </summary>
        /// <param name="strTemp"></param>
        /// <returns></returns>
        public static Byte[] ConvertFrom(string strTemp, int IntLen)
        {
            try
            {
                Byte[] Temp = ConvertFrom(strTemp.Trim());
                Byte[] return_Byte = new Byte[IntLen];
                if (IntLen != 0)
                {
                    if (Temp.Length < IntLen)
                    {
                        for (int i = 0; i < IntLen - Temp.Length; i++)
                        {
                            return_Byte[i] = 0x00;
                        }
                    }
                    Array.Copy(Temp, 0, return_Byte, IntLen - Temp.Length, Temp.Length);
                    return return_Byte;
                }
                else
                {
                    return Temp;
                }
            }
            catch
            { return null; }
        }

        /// <summary>
        /// 16进制转换BCD（解压BCD）
        /// </summary>
        /// <param name="AData"></param>
        /// <returns></returns>
        public static string ConvertTo(Byte[] AData)
        {
            try
            {
                StringBuilder sb = new StringBuilder(AData.Length * 2);
                foreach (Byte b in AData)
                {
                    sb.Append(b >> 4);
                    sb.Append(b & 0x0f);
                }
                return sb.ToString();
            }
            catch { return null; }
        }

        /// <summary>
        /// stream 转 byte[] ,请用完后手动释放Stream。
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] StreamToBytes(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // stream.Dispose();
            return bytes;

        }

        /// <summary>
        /// int 转byte[2]
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static byte[] Int2Bytes2(int a)
        {
            byte[] result = new byte[2];
            result[1] = (byte)(a & 255);
            a >>= 8;
            result[0] = (byte)(a & 255);
            return result;
        }

        /// <summary>
        /// int转BYTE[]
        /// </summary>
        /// <param name="a">要转的值</param>
        /// <param name="b">要转数组的长度</param>
        /// <returns></returns>
        public static byte[] Int2Bytes4(int a)
        {

            byte[] result = new byte[4];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = (byte)(a & 255);
                a >>= 8;
            }

            return result;
        }


        /// <summary>
        /// byte[]转int
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static int Bytes2Int(byte[] a)
        {
            int var1 = a.Length;
            int var2 = 0;

            int var3;
            for (int var10000 = var3 = 0; var10000 < var1; var10000 = var3)
            {
                var10000 = var2 <<= 8;
                byte var10001 = a[var3];
                ++var3;
                var2 = var10000 | var10001 & 255;
            }

            return var2;
        }

        public static byte[] BinaryToBytes(string orgStr)
        {
            byte[] result = null;
            if (orgStr.Length > 8)
            {
                ///get the lenght of byte array
                int len = orgStr.Length % 8 == 0 ? orgStr.Length / 8 : (orgStr.Length / 8) + 1;
                ///initial the result with the length calculated previously
                result = new byte[len];
                /// define a varibale which will be used to split the string
                ///complement the length of the orgianl string, which can be dividened by 8

                /// Assign the original string to another temp variable in case of confused with the original one
                /// This temporary string will be renewed every time after getting the result of a subString
                string tempStr = orgStr.PadLeft((8 - orgStr.Length % 8) + orgStr.Length, '0');
                for (int i = 0; i < len; i++)
                {
                    string binStr;

                    binStr = tempStr.Substring(i * 0, 8);
                    tempStr = tempStr.Substring(8, tempStr.Length - 8);

                    result[i] = Convert.ToByte(binStr, 2);
                }
            }
            else
            {
                result = new byte[1];
                result[0] = Convert.ToByte(orgStr, 2);
            }

            return result;
        }
    }
}
