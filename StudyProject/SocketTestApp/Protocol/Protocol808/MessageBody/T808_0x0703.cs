using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTestApp.Protocol.Protocol808.MessageBody
{
    public class T808_0x0703 : T808_MessageBody
    {
        private string b;
        private string m;
        private string ALLATORIxDEMO;

        public void setpTime(string a)
        {
            this.m = a;
        }

        public string getLockNo()
        {
            return this.ALLATORIxDEMO;
        }

        public void setPwd(string a)
        {
            this.b = a;
        }

        public void setLockNo(string a)
        {
            this.ALLATORIxDEMO = a;
        }

        public string getpTime()
        {
            return this.m;
        }

        public string getPwd()
        {
            return this.b;
        }

        public T808_0x0703()
        {
            this.messageID = 1795;
        }
    }

}
