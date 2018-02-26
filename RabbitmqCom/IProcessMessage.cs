using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitmqCom
{
    public interface IProcessMessage
    {
        void ProcessMsg(Message msg);
    }
}
