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
    public class T808_0x0200_Process<T> : IProcess<T> where T : IMessageBody
    {
        public byte[] PackData(CommonMessage<T> cm, string key, string voltage, string status, string version, string jzdw, int altitude, int speed, int direction)
        {
            var stream = new MemoryStream();
            try
            {
                stream.WriteByte(0);//头
                var headByte = ProtocolHelper.getHeaderBytes(cm);
                var getByteNumBt = ToolHelper.Int2Bytes2(29);
                headByte[2] = getByteNumBt[0];
                headByte[3] = getByteNumBt[1];
                stream.Write(headByte, 0, headByte.Length);//头部
                var keyBt = ToolHelper.HexStringToByteArray(ToolHelper.StringToByHexString(key));
                stream.Write(keyBt, 0, keyBt.Length);//key
                var volBt = ToolHelper.HexStringToByteArray(voltage);
                stream.Write(volBt, 0, volBt.Length);//电压
                var stateBt = ToolHelper.HexStringToByteArray(status);
                stream.Write(stateBt, 0, stateBt.Length);//状态
                var versionbt = ToolHelper.HexStringToByteArray(version);
                stream.Write(versionbt, 0, versionbt.Length);//版本
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

        public T808_0x0200_Process()
        {
        }
    }
}
