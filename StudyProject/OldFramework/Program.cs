using OldFramework.MSMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OldFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(() =>
            {
                //while (true)
                //{
                    
                //}
                Order data = new Order { orderId = Guid.NewGuid(), orderTime = DateTime.Now };
                MSMQDemo.SendMessage(data);
                Console.WriteLine($"发出消息Order Id:{data.orderId}");
                //Thread.Sleep(3000);
            });

            Task.Run(() =>
            {
                //while (true)
                //{
                    
                //}
                MSMQDemo.ReceiveMessage();
                Thread.Sleep(3000);
            });


            Console.Read();
        }
    }
}
