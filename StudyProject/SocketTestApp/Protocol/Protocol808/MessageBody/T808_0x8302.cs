using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTestApp.Protocol.Protocol808.MessageBody
{
    public class T808_0x8302 : T808_MessageBody
    {
        private int b;
        private int m;
        private DateTime ALLATORIxDEMO;

        public int getIsControlled()
        {
            return this.m;
        }

        public T808_0x8302()
        {
            this.messageID = 33538;
        }

        public DateTime getDate()
        {
            return this.ALLATORIxDEMO;
        }

        public void setIsControlled(int a)
        {
            this.m = a;
        }

        public int getControlledTime()
        {
            return this.b;
        }

        public void setDate(DateTime a)
        {
            this.ALLATORIxDEMO = a;
        }

        public void setControlledTime(int a)
        {
            this.b = a;
        }
    }
}
