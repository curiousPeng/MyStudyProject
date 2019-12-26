using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTestApp.Protocol.Protocol808.MessageBody
{
    public class T808_0x8701 : T808_MessageBody
    {
        private int ALLATORIxDEMO;

        public void setResult(int a)
        {
            this.ALLATORIxDEMO = a;
        }

        public T808_0x8701()
        {
            this.messageID = 34561;
        }

        public int getResult()
        {
            return this.ALLATORIxDEMO;
        }
    }

}
