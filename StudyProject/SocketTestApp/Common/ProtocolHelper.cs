using SocketTestApp.Protocol.Common;
using SocketTestApp.Protocol.Protocol808.MessageBody;
using SocketTestApp.Protocol.Protocol808.Process;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace SocketTestApp.Common
{
    public static class ProtocolHelper//<T> where T : IMessageBody
    {
        private static int index = 1;
        /// <summary>
        /// 数组倒数第二位是校验位，算出校验位
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte Check(byte[] data)
        {
            byte tmp = (byte)(data[1] ^ data[2]);

            for (int i = 3; i < data.Length; i++)
            {
                tmp ^= data[i];
            }
           
            return tmp;
        }

        public static byte[] reverseEscapeData(byte[] data)
        {
            byte[] result;
            try
            {
                int i = 0;
                Stream stream = new MemoryStream();
                while (true)
                {

                    if (i >= data.Length - 1)
                    {
                        if (data[data.Length - 2] != 125)
                        {
                            if (data[data.Length - 1] == 125)
                            {
                                throw new Exception("错误的协议无法转义！");
                            }

                            stream.WriteByte(data[data.Length - 1]);
                        }

                        result = ToolHelper.StreamToBytes(stream);
                        break;
                    }
                    if (data[i] == 125 && data[i + 1] == 2)
                    {
                        ++i;
                        stream.WriteByte(126);
                    }
                    else if (data[i] == 125 && data[i + 1] == 1)
                    {
                        ++i;
                        stream.WriteByte(125);
                    }
                    else
                    {
                        if (data[i] == 125 && data[i + 1] != 2 || data[i] == 125 && data[i + 1] != 1)
                        {
                            throw new Exception("错误的协议无法转义！");
                        }

                        stream.WriteByte(data[i]);
                    }

                    ++i;
                }
            }
            catch
            {
                throw;
            }

            return result;
        }

        public static byte[] escapeData(byte[] data)
        {
            Stream stream = new MemoryStream();

            byte[] result;
            try
            {
                int i = 0;

                while (true)
                {
                    if (i >= data.Length)
                    {
                        result = ToolHelper.StreamToBytes(stream);
                        break;
                    }

                    if (data[i] == 126)
                    {
                        stream.WriteByte(125);
                        stream.WriteByte(2);
                    }
                    else if (data[i] == 125)
                    {
                        stream.WriteByte(125);
                        stream.WriteByte(1);
                    }
                    else
                    {
                        stream.WriteByte(data[i]);
                    }

                    ++i;

                }
            }
            catch
            {
                throw;
            }

            return result;
        }
        
        public static byte[] getHeaderBytes<T>(CommonMessage<T> a) where T: IMessageBody
        {
            byte[] var1 = getInitNoPackHeadr();
            byte[] var2 = ToolHelper.Int2Bytes2((int)a.getMessageID());
            var1[0] = var2[0];
            var1[1] = var2[1];
        
            T808_MessageHeader head= (T808_MessageHeader)a.getMessageHeader();
            if (head.getSimNum() != null && !head.getSimNum().Equals(""))
            {
                if (head.getSimNum().Length > 12)
                {
                    head.setSimNum(head.getSimNum().Substring(0, 12));
                }
                else
                {
                    if (head.getSimNum().Length < 12)
                    {
                        head.setSimNum(formatString(head.getSimNum()));
                    }
                }
            }
            else
            {
                head.setSimNum("000000000000");
            }

            var2 = strToBcd(head.getSimNum());
            byte[] var10001 = var1;
            byte[] var4 = var1;
            byte[] var10002 = var1;
            var1[4] = var2[0];
            var1[5] = var2[1];
            var1[6] = var2[2];
            var1[7] = var2[3];
            var1[8] = var2[4];
            var1[9] = var2[5];
            var1 = ToolHelper.Int2Bytes2(getRunningNum());
            var10002[10] = var1[0];
            var10001[11] = var1[1];
            return var4;
        }
        public static byte[] getInitNoPackHeadr()
        {
            byte[] head = new byte[12];
            return head;
        }
        public static string formatString(string a)
        {
            long var1 = long.Parse(a);

            return var1.ToString("000000000000");
        }

        public static byte[] strToBcd(string a)
        {
            int var1;
            if ((var1 = a.Length) % 2 != 0)
            {
                var1 = (a = ProtocolExceptionALLATORIxDEMO("8") + a).Length;
            }

            byte[] var10000 = new byte[var1];
            if (var1 >= 2)
            {
                var1 /= 2;
            }

            var10000 = new byte[var1];
            byte[] var6 = var10000;
            byte[] var2 = Encoding.Default.GetBytes(a);

            for (int i = 0; i < a.Length / 2;i++)
            {
                int var3;
                if (var2[2 * i] >= 48 && var2[2 * i] <= 57)
                {
                    var10000 = var2;
                    var3 = var2[2 * i] - 48;
                }
                else if (var2[2 * i] >= 97 && var2[2 * i] <= 122)
                {
                    var10000 = var2;
                    var3 = var2[2 * i] - 97 + 10;
                }
                else
                {
                    var10000 = var2;
                    var3 = var2[2 * i] - 65 + 10;
                }

                int var4;
                if (var10000[2 * i + 1] >= 48 && var2[2 * i + 1] <= 57)
                {
                    var4 = var2[2 * i + 1] - 48;
                  
                }
                else if (var2[2 * i + 1] >= 97 && var2[2 * i + 1] <= 122)
                {
                    var4 = var2[2 * i + 1] - 97 + 10;
                }
                else
                {
                    var4 = var2[2 * i + 1] - 65 + 10;
                }

                byte var7 = (byte)((var3 << 4) + var4);
                var6[i++] = var7;
            }

            return var6;
        }
        public static int getRunningNum()
        {
            ++index;
            if (index == 65534)
            {
                index = 1;
            }

            return index;
        }

        public static string ProtocolExceptionALLATORIxDEMO(string a)
        {
            int var10000 = (2 ^ 5) << 4 ^ 1 << 1;
            int var10001 = 1 << 3;
            int var10002 = 4 << 4 ^ 3 << 2 ^ 1;
            int var10003 = a.Length;
            char[] var10004 = new char[var10003];
            int var5 = var10003 - 1;
            var10003 = var10002;
            int var3;
            var10002 = var3 = var5;
            char[] var1 = var10004;
            int var4 = var10003;
            var10000 = var10002;

            for (int var2 = var10001; var10000 >= 0; var10000 = var3)
            {
                var10001 = var3;
                char var6 = a[var3];
                --var3;
                var1[var10001] = (char)(var6 ^ var2);
                if (var3 < 0)
                {
                    break;
                }

                var10002 = var3--;
                var1[var10002] = (char)(a[var10002] ^ var4);
            }

            return new String(var1);
        }

        //public static CommonMessage<IMessageBody> unpack(byte[] a)
        //{
        //    if (a[0] == 126 && a[a.Length - 1] == 126)
        //    {
        //        bool var2 = Check(a = reverseEscapeData(a)) == a[a.Length - 2];
        //        if (!var2)
        //        {
        //            throw new Exception("校验位出错");
        //        }
        //        else
        //        {
        //            T808_MessageHeader var5 = getHeader(a);
        //            CommonMessageBody var4 = var5.getSubpackage() ? var3.getBody(var5, BytesUtil.cutBytes(17, a.length - 19, a)): (var4 = var3.getBody(var5, BytesUtil.cutBytes(13, a.length - 15, a)));

        //        }
        //    }
        //    else
        //    {
        //        throw new Exception("不是7E开头7E结尾");
        //    }
        //}

        public static T808_MessageHeader getHeader(byte[] a)
        {
            T808_MessageHeader var1 = new T808_MessageHeader();
            byte[] var2 = cutBytes(1, 12, a);//截取出消息头
            var msgid = getWord(0, var2);
            int var3 = parseBytesToInt(msgid);//messageid
            byte[] var4 = getBigWord(2, var2);
            int var5 = ToolHelper.Bytes2Int(var4); //getBitsValue(0, 9, var4);//
            int var6 = getBitsValue(10, 3, var4);
            bool var12 = getBooleanValue(1, 2, var4);
            String var7 = bcdToStr(cutBytes(4, 6, var2));
            int var11 = parseBytesToInt(getWord(10, var2));
            int var8 = 0;
            int var9 = 0;
            if (var12)
            {
                byte[] var10 = getWord(13, a);
                byte[] var10000 = getWord(15, a);
                var8 = parseBytesToInt(var10);
                var9 = parseBytesToInt(var10000);
            }

            var1.setBodyLength(var5);
            var1.setEncrypt(var6);
            var1.setMessageID(var3);
            var1.setPackageCounts(var8);
            var1.setPackageNum(var9);
            var1.setRunningNum(var11);
            var1.setSimNum(var7);
            var1.setSubpackage(var12);
            return var1;
        }
        public static byte[] cutBytes(int index, int lenght, byte[] data)
        {
            byte[] result = new byte[lenght];

            result = data.Skip(index).Take(lenght).ToArray();
            return result;
        }
        public static int parseBytesToInt(byte[] data)
        {
            int length = data.Length;
            int result = 0;

            int j;
            for (int i = j = 0; i < length; i = j)
            {
                i = result <<= 8;
                byte var10001 = data[j];
                ++j;
                result = i | var10001 & 255;
            }
            return result;
        }
        /// <summary>
        /// 一个 word的长度
        /// </summary>
        /// <param name="index"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] getWord(int index, byte[] data)
        {
            return cutBytes(index, 2, data);
        }
        public static byte[] getBigWord(int a, byte[] data)
        {
            byte[] result = new byte[2];
            result[0] = data[a];
            result[1] = data[a + 1];
            return result;
        }

        public static int getBitsValue(int a, int lenght, byte[] bt)
        {
            int var3 = parseBytesToInt(bt);
            int var4 = 0;
            
            for (int i = 0; i < lenght; ++var4)
            {
                var4 <<= 1;
                ++i;
            }

            return var3 >> bt.Length * 8 - (a + a - 1) & var4;
        }

        public static bool getBooleanValue(int index, int a, byte[] data)
        {
            return (data[index] >> 7 - a & 1) == 1;
        }
        public static string bcdToStr(byte[] a)
        {
            StringBuilder var1 = new StringBuilder(a.Length * 2);

            int var2;
            for (int var10000 = var2 = 0; var10000 < a.Length; var10000 = var2)
            {
                var1.Append((byte)((a[var2] & 240) >> 4));
                byte var10002 = a[var2];
                ++var2;
                var1.Append((byte)(var10002 & 15));
            }

            return var1.ToString().Substring(0, 1).Equals(ProtocolExceptionALLATORIxDEMO("8")) ? var1.ToString().Substring(1) : var1.ToString();
        }
    }
}
