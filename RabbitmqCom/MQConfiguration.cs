using EasyNetQ.FluentConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitmqCom
{
    public class MQConfiguration : IPublishConfiguration
    {
        public IPublishConfiguration WithExpires(int expires)
        {
            throw new NotImplementedException();
        }

        public IPublishConfiguration WithPriority(byte priority)
        {
            throw new NotImplementedException();
        }

        public IPublishConfiguration WithTopic(string topic)
        {
            throw new NotImplementedException();
        }
    }

}
