using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTestApp.Protocol.Common
{
    public interface IMessageBody
    {
        string entityString();

        object getMessageID();
    }
}
