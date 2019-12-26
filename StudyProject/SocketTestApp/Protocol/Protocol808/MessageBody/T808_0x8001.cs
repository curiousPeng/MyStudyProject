using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTestApp.Protocol.Protocol808.MessageBody
{
    public class T808_0x8001 : T808_MessageBody
    {
    private DateTime ALLATORIxDEMO;

    public void setDateTime(DateTime a)
    {
        this.ALLATORIxDEMO = a;
    }

    public DateTime getDateTime()
    {
        return this.ALLATORIxDEMO;
    }

    public T808_0x8001()
    {
        this.messageID = 32769;
    }
}
}
