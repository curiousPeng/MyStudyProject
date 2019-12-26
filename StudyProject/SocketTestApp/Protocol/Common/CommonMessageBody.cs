using System;

namespace SocketTestApp.Protocol.Common
{
    public class CommonMessageBody : IMessageBody
    {
        protected Object messageID;

        public String entityString()
        {
            return "";
            //return JsonConvert.SerializeObject(this);
        }

        public CommonMessageBody()
        {
        }

        public object getMessageID()
        {
            return this.messageID;
        }

        public void setMessageID(object a)
        {
            this.messageID = a;
        }
    }

}
