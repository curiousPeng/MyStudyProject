using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTestApp.Protocol.Protocol808.MessageBody
{
    public class T808_0x0103 : T808_MessageBody
    {
        private int ALLATORIxDEMO;

        public string getResultDesc()
        {
            if (this.ALLATORIxDEMO == 0)
            {
                return "协议解析错误";
            }
            else if (this.ALLATORIxDEMO == 1)
            {
                return "未连接到终端";
            }
            else if (this.ALLATORIxDEMO == 2)
            {
                return "协议解析错误";
            }
            else
            {
                return this.ALLATORIxDEMO == 3 ? "终端未连接" : this.ALLATORIxDEMO.ToString();
            }
        }

        public void setResult(int a)
        {
            this.ALLATORIxDEMO = a;
        }

        public int getResult()
        {
            return this.ALLATORIxDEMO;
        }

        public T808_0x0103()
        {
            this.messageID = 259;
        }
    }
}
