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
    public class T808_0x0700_Process<T> : IProcess<T> where T : IMessageBody
    {
        public byte[] PackData(CommonMessage<T> cm, string key)
        {
            var stream = new MemoryStream();
            try
            {
                stream.WriteByte(0);
                var headByte = ProtocolHelper.getHeaderBytes(cm);
                var getByteNumBt = ToolHelper.Int2Bytes2(11);
                headByte[2] = getByteNumBt[0];
                headByte[3] = getByteNumBt[1];
                stream.Write(headByte, 0, headByte.Length);
                var keyBt = ToolHelper.HexStringToByteArray(ToolHelper.StringToByHexString(key));
                stream.Write(keyBt, 0, keyBt.Length);
                stream.WriteByte(0);//结果 0为成功
                //状态word 2字节,这16位分别代表的参数参考PDF文档
                var statusBinary = "0000000000000000";//16位
                var statusBt = ToolHelper.BinaryToBytes(statusBinary);
                stream.Write(statusBt, 0, statusBt.Length);
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

        public T808_0x0700_Process()
        {
        }
    }
}
