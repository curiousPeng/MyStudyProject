using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTestApp.Protocol.Protocol808.MessageBody
{
    public class T808_0x0600 : T808_MessageBody
    {
        private int ALLATORIxDEMO;

        public string getResultDesc()
        {
            return "";
        }

        public T808_0x0600()
        {
            this.messageID = 1536;
        }

        public int getResult()
        {
            return this.ALLATORIxDEMO;
        }

        public void setResult(int a)
        {
            this.ALLATORIxDEMO = a;
        }
    }
}
