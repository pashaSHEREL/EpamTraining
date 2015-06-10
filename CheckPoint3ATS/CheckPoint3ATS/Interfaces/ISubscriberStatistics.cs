using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    interface ISubscriberStatistics
    {
        int AccountNumber { get; }
        int Balance{get;set;}
        int PhoneNumber { get; }
        ITariffPlan TariffPlan { get; }
        List<ICallInfo> CallsInfo { get; }
        List<IPayment> Payments { get; }
        void AddCallInfo(ICallInfo callInfo);
    }
}
