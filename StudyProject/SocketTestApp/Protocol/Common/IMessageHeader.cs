using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTestApp.Protocol.Common
{
    public interface IMessageHeader
    {
        void setMessageID(object var1);

        string entityString();

        object getMessageID();
    }
}
