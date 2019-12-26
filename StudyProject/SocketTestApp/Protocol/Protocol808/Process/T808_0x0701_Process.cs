using SocketTestApp.Common;
using SocketTestApp.Protocol.Common;
using SocketTestApp.Protocol.Protocol808.MessageBody;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTestApp.Protocol.Protocol808.Process
{
    public class T808_0x0701_Process<T> : IProcess<T> where T : IMessageBody
    {
        public byte[] PackData(CommonMessage<T> cm, string key, string pinNum, string password, string lockNum, string jzdw, int altitude, int speed, int direction)
        {
            var stream = new MemoryStream();
            try
            {
                stream.WriteByte(0);
                var headByte = ProtocolHelper.getHeaderBytes(cm);
                var getByteNumBt = ToolHelper.Int2Bytes2(55);
                headByte[2] = getByteNumBt[0];
                headByte[3] = getByteNumBt[1];
                stream.Write(headByte, 0, headByte.Length);
                //key 0-7
                var keyBt = ToolHelper.HexStringToByteArray(ToolHelper.StringToByHexString(key));
                stream.Write(keyBt, 0, keyBt.Length);
                //操作员PIN码 8-13
                //ping
                var pingBt = ASCIIEncoding.Default.GetBytes(pinNum);
                stream.Write(pingBt, 0, pingBt.Length);
                //password  //操作员密码 14-19
                var pwdBt = ASCIIEncoding.Default.GetBytes(password);
                stream.Write(pwdBt, 0, pwdBt.Length);
                //锁号/铅封号 20-23
                var lockBt = ASCIIEncoding.Default.GetBytes(lockNum);
                stream.Write(lockBt, 0, lockBt.Length);
                //状态标志 24-25 16位代表内容参考PDF文档
                var statusBinary = "0000000000000000";//16位
                var statusBt = ToolHelper.BinaryToBytes(statusBinary);
                stream.Write(statusBt, 0, statusBt.Length);
                //位置信息 26-end
                //报警标志，已废弃
                var WaringBt = ToolHelper.HexStringToByteArray("00000000");
                stream.Write(WaringBt, 0, WaringBt.Length);
                var jzBt = ToolHelper.HexStringToByteArray(jzdw);
                stream.Write(jzBt, 0, jzBt.Length);
                var LatitudeAndLongitude = ToolHelper.HexStringToByteArray("0000000000000000");
                stream.Write(LatitudeAndLongitude, 0, LatitudeAndLongitude.Length);
                var altBt = ToolHelper.HexStringToByteArray(altitude.ToString("x4"));
                stream.Write(altBt, 0, altBt.Length);
                var speedBt = ToolHelper.HexStringToByteArray(speed.ToString("x4"));
                stream.Write(speedBt, 0, speedBt.Length);
                var dirBt = ToolHelper.HexStringToByteArray(direction.ToString("x4"));
                stream.Write(dirBt, 0, dirBt.Length);
                var dateBt = ToolHelper.HexStringToByteArray(DateTime.Now.ToString("yyMMddHHmmss"));//代表时间
                stream.Write(dateBt, 0, dateBt.Length);

                var end2 = ProtocolHelper.Check(ToolHelper.StreamToBytes(stream));//取得倒数第二位的校验位
                stream.WriteByte(end2);
                stream.WriteByte(0);
                var sendByte = ToolHelper.StreamToBytes(stream);
                sendByte = ProtocolHelper.escapeData(sendByte);
                sendByte[0] = 126;
                sendByte[sendByte.Length - 1] = 126;
                stream.Dispose();
                return sendByte;
            }
            catch
            {
                stream.Dispose();
                throw;
            }
        }

        public CommonMessageBody getBody(CommonMessageHeader a1, byte[] a2)
        {
            return null;
        }

        public T808_0x0701_Process()
        {
        }
    }
}
