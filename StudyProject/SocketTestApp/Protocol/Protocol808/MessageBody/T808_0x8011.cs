using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTestApp.Protocol.Protocol808.MessageBody
{
    public class T808_0x8011 : T808_MessageBody
    {
        private DateTime m;
        private int ALLATORIxDEMO;

        public DateTime getDateTime()
        {
            return this.m;
        }

        public void setDateTime(DateTime a)
        {
            this.m = a;
        }

        public void setIsEnable(int a)
        {
            this.ALLATORIxDEMO = a;
        }

        public T808_0x8011()
        {
            this.messageID = 32785;
        }

        public int getIsEnable()
        {
            return this.ALLATORIxDEMO;
        }
    }

}
