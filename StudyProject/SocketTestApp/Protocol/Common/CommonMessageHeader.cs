namespace SocketTestApp.Protocol.Common
{
    public class CommonMessageHeader : IMessageHeader
    {
        protected object messageID;

        public void setMessageID(object a)
        {
            this.messageID = a;
        }

        public CommonMessageHeader()
        {
        }

        public string entityString()
        {
            return "";
            //return JsonConvert.SerializeObject(this);
        }

        public object getMessageID()
        {
            return this.messageID;
        }
    }
}
