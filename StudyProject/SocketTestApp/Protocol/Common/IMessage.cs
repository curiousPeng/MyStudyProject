using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTestApp.Protocol.Common
{
    public interface IMessage<T> where T : IMessageBody
    {
        T getMessageBody();

        IMessageHeader getMessageHeader();

        string entityString();

        object getMessageID();
    }
}
