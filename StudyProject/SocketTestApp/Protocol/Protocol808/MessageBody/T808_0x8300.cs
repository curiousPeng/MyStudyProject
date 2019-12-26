using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTestApp.Protocol.Protocol808.MessageBody
{
    public class T808_0x8300 : T808_MessageBody
    {
    private int b;
    private int m;
    private int ALLATORIxDEMO;

    public void setLongitude(int a)
    {
        this.m = a;
    }

    public int getLatitude()
    {
        return this.b;
    }

    public T808_0x8300()
    {
        this.messageID = 33536;
    }

    public void setLatitude(int a)
    {
        this.b = a;
    }

    public int getRadius()
    {
        return this.ALLATORIxDEMO;
    }

    public void setRadius(int a)
    {
        this.ALLATORIxDEMO = a;
    }

    public int getLongitude()
    {
        return this.m;
    }
}
}
