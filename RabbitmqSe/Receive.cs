using EasyNetQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitmqCom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RabbitmqSe
{
    class Receive
    {
        public static void Main()
        {

            #region 0.0 EasyNetQ Subscribe
            //using (var bus = RabbitHutch.CreateBus("host=localhost"))
            //{
            //    // 0.0.0
            //    // bus.Subscribe<Message>("test", HandleTextMessage);
            //    // 0.0.1
            //    bus.Subscribe<Message>("my_subscription_id", msg => Console.WriteLine(msg.MessageTitle + "--" + msg.MessageBody));
            //    Console.WriteLine("Listening for messages. Hit <return> to quit.");
            //    Console.ReadLine();
            //}
            #endregion

            #region 0.1 EasyNetQ Receive
            //using (var bus = RabbitHutch.CreateBus("host=localhost"))
            //{

            //    bus.Receive<Message>("first", msg => Console.WriteLine(msg.MessageTitle + "--" + msg.MessageBody));
            //    Console.WriteLine("Listening for messages. Hit <return> to quit.");

            //    Console.ReadKey();

            //    bus.Receive<Message>("sencod", msg => Console.WriteLine(msg.MessageTitle + "--" + msg.MessageBody));
            //    // bus.Receive<MyMessage>("my.queue", message => Console.WriteLine("MyMessage: {0}", message.Text));
            //    //  Console.WriteLine("Listening for messages. Hit <return> to quit.");
            //    Console.ReadLine();
            //}
            #endregion

            //#region 0.2 EasyNetQ Topic Based Routing
            //using (var bus = RabbitHutch.CreateBus("host=localhost"))
            //{

            //    //  消息订阅方可以通过路由来过滤相应的消息。
            //    // * 匹配一个字符
            //    // # 匹配0个或者多个字符
            //    bus.Subscribe<Message>("my_id", HandleTextMessage, x => x.WithTopic("z.*"));
            //    bus.Subscribe<Message>("my_id", HandleTextMessage, x => x.WithTopic("A.*"));
            //    // bus.Receive<Message>("sencod", msg => Console.WriteLine(msg.MessageTitle + "--" + msg.MessageBody));
            //    // bus.Receive<MyMessage>("my.queue", message => Console.WriteLine("MyMessage: {0}", message.Text));
            //    //  Console.WriteLine("Listening for messages. Hit <return> to quit.");
            //    Console.ReadKey();
            //    //Console.WriteLine("测试读取已经处理过的数据");
            //    //bus.Subscribe<Message>("my_id", HandleTextMessage, x => x.WithTopic("z.*"));
            //    //bus.Subscribe<Message>("my_id", HandleTextMessage, x => x.WithTopic("A.*"));             



            //    Console.ReadLine();
            //}
            //#endregion

            #region 0.3 EasyNetQ Respond Routing
            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {

                //  消息订阅方可以通过路由来过滤相应的消息。
                // * 匹配一个字符
                // # 匹配0个或者多个字符
                bus.Subscribe<Message>("my_id", HandleTextMessage, x => x.WithTopic("z.*"));
                bus.Subscribe<Message>("my_id", HandleTextMessage, x => x.WithTopic("A.*"));

                //  Console.ReadKey();


                bus.Respond<Message, Message>(ms => HandMessage(ms));

                bus.RespondAsync<Message, Message>(ms => HandMessageAsync(ms));
                Console.ReadLine();
            }
            #endregion


            //  var bus = RabbitHutch.CreateBus("host=localhost;virtualHost=default;username=guest;password=guest");
            //  bus.Subscribe<Message>("my_subscription_id", msg => Console.WriteLine(msg.MessageBody + "--" + msg.MessageBody));

            //OrderProcessMessage order = new OrderProcessMessage();
            //Message msg = new Message();
            //msg.MessageID = "2";
            //msg.MessageRouter = "sadfsadfasdf";// "pcm.notice.zhangsan";

            //MQHelper.Subscribe(msg, order);



            //var factory = new ConnectionFactory() { HostName = "localhost" };
            //using (var connection = factory.CreateConnection())
            //using (var channel = connection.CreateModel())
            //{
            //    channel.QueueDeclare(queue: "hello",
            //                         durable: false,
            //                         exclusive: false,
            //                         autoDelete: false,
            //                         arguments: null);

            //    var consumer = new EventingBasicConsumer(channel);
            //    consumer.Received += (model, ea) =>
            //    {
            //        var body = ea.Body;
            //        var message = Encoding.UTF8.GetString(body);
            //        Console.WriteLine(" [x] Received {0}", message);
            //    };
            //    channel.BasicConsume(queue: "hello",
            //                         noAck: true,
            //                         consumer: consumer);

            //    Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        static void HandleTextMessage(Message textMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Got message: {0}", textMessage.MessageTitle);
            Console.ResetColor();
        }

        static Message HandMessage(Message textMessage)
        {
            Console.WriteLine("这是接收Request来的数据：" + textMessage.MessageTitle);
            return new Message { MessageTitle = "server", MessageBody = DateTime.Now.ToString() };
        }

        static async Task<Message> HandMessageAsync(Message textMessage)
        {
            await Task.Delay(1);
            Console.WriteLine("异步接收数据：" + textMessage.MessageTitle);
            return new Message { MessageTitle = "server", MessageBody = DateTime.Now.ToString() };
        }
    }
}

