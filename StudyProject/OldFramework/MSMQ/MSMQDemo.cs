using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace OldFramework.MSMQ
{
    public class MSMQDemo
    {
        public static void SendMessage(Order data)
        {
            string queuePath = @".\private$\myqueue";//位置
            //远程无法使用Exists(),Create()
            //string queuePath = @"FormatName:DIRECT=TCP:192.168.1.1\private$\myqueue";// 使用遠程IP指定訊息佇列位置

            if (!MessageQueue.Exists(queuePath))//是否存在
            {
                MessageQueue.Create(queuePath);
            }
            MessageQueue queue = new MessageQueue(queuePath);
            queue.Send(data);

        }
        public static void ReceiveMessage()
        {
            string queuePath = @".\private$\myqueue";
            if (!MessageQueue.Exists(queuePath))//是否存在
            {
                return;
            }
            MessageQueue queue = new MessageQueue(queuePath);
            queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(Order) });
            try
            {
                // Receive and format the message.
                Message myMessage = queue.Receive();
                Order myOrder = (Order)myMessage.Body;

                // Display message information.
                Console.WriteLine("收到消息Order ID: " +
                    myOrder.orderId.ToString());
            }
            // Handle invalid serialization format.
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }

            // Catch other exceptions as necessary.

            return;
        }
    }

    public class Order
    {
        public Guid orderId;
        public DateTime orderTime;
    };
}
