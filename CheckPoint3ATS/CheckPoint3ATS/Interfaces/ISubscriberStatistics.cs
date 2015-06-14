using System;
using System.Collections.ObjectModel;


namespace CheckPoint3ATS
{
    public interface ISubscriberStatistics
    {
        int AccountNumber { get; }
        int Balance { get; set; }
        int PhoneNumber { get; }
        ITariffPlan TariffPlan { get; }
        ReadOnlyCollection<ICallInfo> CallsInfo { get; }
        ReadOnlyCollection<IPayment> Payments { get; }

        event EventHandler<EventArgs> PaymentIsMade;

        void AddCallInfo(ICallInfo callInfo);
        void AddPayment(IPayment payment);
    }
}