using System;
using System.Collections.ObjectModel;


namespace CheckPoint3ATS
{
    public interface ISubscriberStatistics
    {
        int AccountNumber { get; }
        int Balance { get; set; }
        int PhoneNumber { get; }
        IStandartTariffPlan TariffPlan { get; set; }
        ReadOnlyCollection<ICallInfo> CallsInfo { get; }
        ReadOnlyCollection<IPayment> Payments { get; }
        DateTime ChangeTariffPlanDay { get; set; }
        event EventHandler<EventArgs> PaymentIsMade;
        void AddCallInfo(ICallInfo callInfo);
        void AddPayment(IPayment payment);
    }
}