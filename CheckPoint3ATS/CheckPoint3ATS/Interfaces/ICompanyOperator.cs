using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    public interface ICompanyOperator
    {
        string Name { get; }
        void AddTerminal(ITerminal terminal);
        void ConcludeContract(ISubscriber subscriber, int phoneNumber, ITariffPlan tariffPlan);
        void TerminateContract(ISubscriber subscriber);
    }
}