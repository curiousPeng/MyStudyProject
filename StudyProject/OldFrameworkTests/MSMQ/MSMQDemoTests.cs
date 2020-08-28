using Microsoft.VisualStudio.TestTools.UnitTesting;
using OldFramework.MSMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldFramework.MSMQ.Tests
{
    [TestClass()]
    public class MSMQDemoTests
    {
        [TestMethod()]
        public void SendMessageTest()
        {
            MSMQDemo.SendMessage(new Order { orderId = Guid.NewGuid(), orderTime = DateTime.Now });
        }

        [TestMethod()]
        public void ReceiveMessageTest()
        {
            MSMQDemo.ReceiveMessage();
        }
    }
}