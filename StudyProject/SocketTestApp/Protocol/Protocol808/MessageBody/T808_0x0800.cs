using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTestApp.Protocol.Protocol808.MessageBody
{
    public class T808_0x0800 : T808_MessageBody
    {
        private int ALLATORIxDEMO;

        public String getResultDesc()
        {
            return "";
        }

        public T808_0x0800()
        {
            this.messageID = 2048;
        }

        public void setResult(int a)
        {
            this.ALLATORIxDEMO = a;
        }

        public int getResult()
        {
            return this.ALLATORIxDEMO;
        }
    }

}
