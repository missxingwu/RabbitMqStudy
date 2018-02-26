using EasyNetQ;
using RabbitMQ.Client;
using RabbitmqCom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitmqCl
{
    class Send
    {
        static void Main(string[] args)
        {
            #region 0.0.0 EasyNetQ Publish

            //using (var bus = RabbitHutch.CreateBus("host=localhost"))
            //{
            //    var input = "";
            //    Console.WriteLine("Enter a message. 'Quit' to quit.");
            //    while ((input = Console.ReadLine()) != "Quit")
            //    {
            //        bus.Publish(new Message
            //        {
            //            MessageBody = DateTime.Now.ToString(),
            //            MessageTitle = "ceshishisi"
            //        });
            //    }
            //} 
            #endregion

            #region 0.0.1 EasyNetQ Publish

            //using (var bus = RabbitHutch.CreateBus("host=localhost"))
            //{

            //    Console.WriteLine("Enter a message. 'Quit' to quit.");
            //    bus.Publish(new Message
            //    {
            //        MessageBody = DateTime.Now.ToString(),
            //        MessageTitle = "ceshishisi"
            //    });

            //}
            #endregion

            #region 0.1 EasyNetQ Send
            //using (var bus = RabbitHutch.CreateBus("host=localhost"))
            //{

            //    Console.WriteLine("Enter a message. 'Quit' to quit.");
            //    bus.Send("first", new Message
            //    {
            //        MessageBody = DateTime.Now.ToString(),
            //        MessageTitle = "ceshishisi"
            //    });
            //    bus.Send("sencod", new Message
            //    {
            //        MessageBody = DateTime.Now.ToString(),
            //        MessageTitle = "这是第二个"
            //    });

            //}
            #endregion

            #region 0.2 EasyNetQ Topic Based Routing
            //using (var bus = RabbitHutch.CreateBus("host=localhost"))
            //{

            //    bus.Publish(new Message
            //    {
            //        MessageBody = DateTime.Now.ToString(),
            //        MessageTitle = "ceshishisi"
            //    }, "z.A");

            //    bus.Publish(new Message
            //    {
            //        MessageBody = DateTime.Now.ToString(),
            //        MessageTitle = "这是第2个"
            //    }, "A.b");

            //    bus.Publish(new Message
            //    {
            //        MessageBody = DateTime.Now.ToString(),
            //        MessageTitle = "这是第3个"
            //    }, "z.b");
            //    bus.Publish(new Message
            //    {
            //        MessageBody = DateTime.Now.ToString(),
            //        MessageTitle = "这是第4个"
            //    }, "z.c");
            //}
            #endregion

            #region 0.3 Request && RequestAsync
            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                Console.Write("Request发送前时间" + DateTime.Now.ToString());
                var sssddd = bus.Request<Message, Message>(new Message { MessageTitle = "这是Request" });
                Console.Write("\n\r\n\r Request发送接收返回数据" + sssddd.MessageTitle + "---------" + DateTime.Now.ToString());

                bus.Publish(new Message
                {
                    MessageBody = DateTime.Now.ToString(),
                    MessageTitle = "ceshishisi"
                }, "z.A");

                bus.Publish(new Message
                {
                    MessageBody = DateTime.Now.ToString(),
                    MessageTitle = "这是第2个"
                }, "A.b");

                bus.Publish(new Message
                {
                    MessageBody = DateTime.Now.ToString(),
                    MessageTitle = "这是第3个"
                }, "z.b");
                bus.Publish(new Message
                {
                    MessageBody = DateTime.Now.ToString(),
                    MessageTitle = "这是第4个"
                }, "z.c");

                bus.Publish(new Message
                {
                    MessageBody = DateTime.Now.ToString(),
                    MessageTitle = "这是第5个"
                }, x => x.WithPriority(5));

                bus.Publish(new Message
                {
                    MessageBody = DateTime.Now.ToString(),
                    MessageTitle = "这是第6个"
                }, x => x.WithExpires(100));
                bus.Publish(new Message
                {
                    MessageBody = DateTime.Now.ToString(),
                    MessageTitle = "这是第7个"
                }, x => x.WithTopic("X.c"));

                //var sssddd = bus.Request<Message, Message>(new Message { MessageTitle = "这是Request" });
                //Console.Write(sssddd.MessageTitle + "---------" + sssddd.MessageBody);

                // var dataAsync= tsk3.

                Console.Write("\n\r\n\r异步前时间" + DateTime.Now.ToString());
                var dataAsync = Task.Run(() =>
               {
                   return RequsetAsyncData(new Message { MessageTitle = "异步MessageTitle" });

                   //  Console.WriteLine("Task启动执行匿名方法");
               }).Result;
                Console.Write("\n\r\n\rRequsetAsync 异步发送数据" + dataAsync.MessageTitle + "---------" + dataAsync.MessageBody);
            }
            #endregion
            //var bus = RabbitHutch.CreateBus("host=localhost;virtualHost=default;username=guest;password=guest");


            //Message msg = new Message();
            //msg.MessageID = "454";
            //msg.MessageBody = DateTime.Now.ToString();
            //msg.MessageTitle = "ceshishisi";
            //msg.MessageRouter = "sadfsadfasdf";// "pcm.notice.zhangsan";
            //bus.Publish<Message>(msg);


            //Message msg = new Message();
            //msg.MessageID = "454";
            //msg.MessageBody = DateTime.Now.ToString();
            //msg.MessageTitle = "ceshishisi";
            //msg.MessageRouter = "sadfsadfasdf";// "pcm.notice.zhangsan";

            //MQHelper.Publish(msg);




            //var factory = new ConnectionFactory() { HostName = "localhost" };
            //using (var connection = factory.CreateConnection())
            //using (var channel = connection.CreateModel())
            //{
            //    channel.QueueDeclare(queue: "hello",
            //                         durable: false,
            //                         exclusive: false,
            //                         autoDelete: false,
            //                         arguments: null);

            //    string message = "Hello World! sadf asd fasd fsadf ";
            //    var body = Encoding.UTF8.GetBytes(message);

            //    channel.BasicPublish(exchange: "",
            //                         routingKey: "hello",
            //                         basicProperties: null,
            //                         body: body);
            //    Console.WriteLine(" [x] Sent {0}", message);
            //}

            //Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        static async Task<Message> RequsetAsyncData(Message msg)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                return await bus.RequestAsync<Message, Message>(msg);
            }
        }
    }
}
