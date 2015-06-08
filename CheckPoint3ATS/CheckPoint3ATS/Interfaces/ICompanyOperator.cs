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
        List<IContract> Contracts { get; set; }
        IBillingSystem BillingSystem { get; set; }
        void ConcludeContract(Subscriber subscriber, int phoneNumber);
        void TerminateContract();
    }
}
