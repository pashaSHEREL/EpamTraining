using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    interface ISubscriber
    {
        string NameL { get; set; }
        string NameF { get; set; }
        string Address { get; set; }
        int PhoneNumber { get; set; }
        ITerminal Terminal { get; set; }
        IContract Contract { get; set; }
        void Call(int number);
        void Answer();
    }
}
