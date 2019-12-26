using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTestApp.Protocol.Protocol808.MessageBody
{
    public class T808_0x8800 : T808_MessageBody
    {
        private string ALLATORIxDEMO;

        public string getTtscontent()
        {
            return this.ALLATORIxDEMO;
        }

        public void setTtscontent(string a)
        {
            this.ALLATORIxDEMO = a;
        }

        public T808_0x8800()
        {
            this.messageID = 34816;
        }
    }

}
