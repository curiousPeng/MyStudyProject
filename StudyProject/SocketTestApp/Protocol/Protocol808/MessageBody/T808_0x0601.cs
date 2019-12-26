using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTestApp.Protocol.Protocol808.MessageBody
{
    public class T808_0x0601 : T808_MessageBody
    {
        private int ALLATORIxDEMO;

        public int getResult()
        {
            return this.ALLATORIxDEMO;
        }

        public T808_0x0601()
        {
            this.messageID = 1537;
        }

        public void setResult(int a)
        {
            this.ALLATORIxDEMO = a;
        }
    }

}
