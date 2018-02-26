using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitmqCom
{
    /// <summary>
    /// 消息服务器连接器
    /// </summary>
    public class BusBuilder
    {
        public static IBus CreateMessageBus()
        {
            // 消息服务器连接字符串
            // var connectionString = ConfigurationManager.ConnectionStrings["RabbitMQ"]; 192.168.2.125:5672
            string connString = "host=localhost;virtualHost=default;username=guest;password=guest";// "host=192.168.2.125:5672;virtualHost=OrderQueue;username=guest;password=guest";//  "host=localhost";//
            if (connString == null || connString == string.Empty)
            {
                throw new Exception("messageserver connection string is missing or empty");
            }

            return RabbitHutch.CreateBus(connString);
        }
    }
}
