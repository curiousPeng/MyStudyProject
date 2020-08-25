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
            MessageQueue queue = new MessageQueue(".\\private$\\myQueue");
            queue.Send(data);

        }
        public static void ReceiveMessage()
        {
            MessageQueue queue = new MessageQueue(".\\private$\\myQueue");
            queue.Formatter = new XmlMessageFormatter(new Type[] {typeof(Order)});
            try
            {
                // Receive and format the message.
                Message myMessage = queue.Receive();
                Order myOrder = (Order)myMessage.Body;

                // Display message information.
                Console.WriteLine("收到消息Order ID: " +
                    myOrder.orderId.ToString());
            }

            catch (MessageQueueException)
            {
                // Handle Message Queuing exceptions.
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
