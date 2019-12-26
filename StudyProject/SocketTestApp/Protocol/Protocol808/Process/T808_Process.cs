using SocketTestApp.Protocol.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTestApp.Protocol.Protocol808.Process
{
    public abstract class T808_Process<T> : IProcess<T> where T : IMessageBody
    {
        public T808_Process()
        {
        }

        public abstract byte[] packData(CommonMessage<T> var1);

        public abstract CommonMessageBody getBody(CommonMessageHeader var1, byte[] var2);
    }

}
