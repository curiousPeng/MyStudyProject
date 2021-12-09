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
                MessageQueue.Create(queuePath, true);
            }
            MessageQueueTransaction myTransaction = new
              MessageQueueTransaction();
            MessageQueue queue = new MessageQueue(queuePath);

            try
            {
                myTransaction.Begin();
                queue.Send(data, myTransaction);
                myTransaction.Commit();
            }
            catch (Exception e)
            {
                myTransaction.Abort();
                throw e;
            }
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
            MessageQueueTransaction myTransaction = new
               MessageQueueTransaction();
            try
            {
                myTransaction.Begin();
                // Receive and format the message.
                Message myMessage = queue.Receive(myTransaction);
                Order myOrder = (Order)myMessage.Body;
                myTransaction.Commit();
                //throw new Exception("11");
                // Display message information.
                Console.WriteLine("收到消息Order Id: " +
                    myOrder.orderId.ToString());
            }
            // Handle invalid serialization format.
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                myTransaction.Abort();
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
