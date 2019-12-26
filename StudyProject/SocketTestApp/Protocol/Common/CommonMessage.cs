using System;

namespace SocketTestApp.Protocol.Common
{
    public class CommonMessage<T> : IMessage<T> where T : IMessageBody
    {
        private object b;
        private T m;
        private IMessageHeader ALLATORIxDEMO;

        public IMessageHeader getMessageHeader()
        {
            return this.ALLATORIxDEMO;
        }

        public T getMessageBody()
        {
            return this.m;
        }

        public Object getMessageID()
        {
            return this.b;
        }

        public CommonMessage(object a, IMessageHeader c, T d)
        {
            this.b = a;
            this.ALLATORIxDEMO = c;
            this.m = d;
        }

        public void setMessageID(Object a)
        {
            this.b = a;
        }

        public void setMessageBody(T a)
        {
            this.m = a;
        }

        public void setMessageHeader(IMessageHeader a)
        {
            this.ALLATORIxDEMO = a;
        }

        public string entityString()
        {
            return "";
            //return JsonConvert.SerializeObject(this);
        }
    }
}
