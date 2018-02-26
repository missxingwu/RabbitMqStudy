using EasyNetQ;
using EasyNetQ.Topology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitmqCom
{
    public class MQHelper
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        public static void Publish(Message msg)
        {

            //// 创建消息bus
            using (IBus bus = BusBuilder.CreateMessageBus())
            {
                try
                {
                    bus.Publish(msg, x => x.WithTopic(msg.MessageRouter));
                }
                catch (EasyNetQException ex)
                {
                    //处理连接消息服务器异常 
                }
                

            }



        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="msg"></param>
        public static void Subscribe(Message msg, IProcessMessage ipro)
        {

            //// 创建消息bus
            //using (IBus bus = BusBuilder.CreateMessageBus())
            //{
            //    try
            //    {
            //        bus.Subscribe<Message>(msg.MessageRouter, message => ipro.ProcessMsg(message), x => x.WithTopic(msg.MessageRouter));
            //    }
            //    catch (EasyNetQException ex)
            //    {
            //        //处理连接消息服务器异常 
            //    }

            //}

            //// 创建消息bus
            IBus bus = BusBuilder.CreateMessageBus();

            try
            {
                bus.Subscribe<Message>(msg.MessageRouter, message => ipro.ProcessMsg(message), x => x.WithTopic(msg.MessageRouter));
             
            }
            catch (EasyNetQException ex)
            {
                //处理连接消息服务器异常 
            }

        }
    }
}
