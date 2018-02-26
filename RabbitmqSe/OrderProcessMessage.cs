using RabbitmqCom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitmqSe
{
    public class OrderProcessMessage : IProcessMessage
    {
        public void ProcessMsg(Message msg)
        {
            Console.WriteLine(msg.MessageBody + msg.MessageTitle);
        }
    }
}
