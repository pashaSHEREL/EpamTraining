using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    internal interface ICompanyOperator
    {
        string Name { get; set; }
        IATS ATS { get; set; }
        void AddTerminal(ITerminal terminal);
        void ConcludeContract(ISubscriber subscriber, int phoneNumber);
        void TerminateContract(ISubscriber subscriber);
    }
}
