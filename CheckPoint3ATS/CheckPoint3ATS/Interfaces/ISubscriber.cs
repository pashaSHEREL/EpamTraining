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
        void Call(int number);
        void Answer();
        void EndCall();
    }
}
