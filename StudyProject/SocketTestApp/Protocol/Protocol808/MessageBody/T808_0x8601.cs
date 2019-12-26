using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTestApp.Protocol.Protocol808.MessageBody
{
    public class T808_0x8601 : T808_MessageBody
    {
        private List<string> ALLATORIxDEMO = new List<string>();

        public void addElockPwdList(string a)
        {
            if (a != null && a.Trim().Length == 6)
            {
                if (this.ALLATORIxDEMO.Count() > 20)
                {
                    throw new System.Exception("未知错误");
                }
                else
                {
                    this.ALLATORIxDEMO.Add(a);
                }
            }
            else
            {
                throw new System.Exception("未知错误");
            }
        }

        public T808_0x8601()
        {
            this.messageID = 34305;
        }

        public List<String> getElockPwdList()
        {
            return this.ALLATORIxDEMO;
        }
    }
}
