using SocketTestApp.Protocol.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTestApp.Protocol.Protocol808.MessageBody
{
    public class T808_MessageBody : CommonMessageBody
    {
    private string terminalID;

    public string getTerminalID()
    {
        return this.terminalID;
    }

    public T808_MessageBody()
    {
    }

    public void setTerminalID(string a)
    {
        this.terminalID = a;
    }
}
}
