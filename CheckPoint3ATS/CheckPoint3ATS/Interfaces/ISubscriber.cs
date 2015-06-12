using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    interface ISubscriber
    {
        string NameL { get;}
        string NameF { get;}
        string Address { get;}
        int PhoneNumber { get; set; }
        ITerminal Terminal { get; set; }
        IContract Contract { get; set; }
        PortMode Call(int number);
        PortMode Answer();
        void HangUpPhone();
        List<ICallInfo> GetStatistic(DateTime firstDay, DateTime lastDay);
        event Func<ISubscriber, List<ICallInfo>> GetStatisticEvent; 
    }
}
