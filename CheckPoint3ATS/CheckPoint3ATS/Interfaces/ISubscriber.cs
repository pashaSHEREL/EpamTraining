using System;
using System.Collections.Generic;


namespace CheckPoint3ATS
{
    public interface ISubscriber
    {
        string NameL { get; }
        string NameF { get; }
        string Address { get; }
        int PhoneNumber { get; set; }
        ITerminal Terminal { get; set; }
        IContract Contract { get; set; }
        event EventHandler<EventArgsForSubscriberChangeTariffPlan> ChangeTariffPlanEvent;
        event Func<ISubscriber, List<ICallInfo>> GetStatisticEvent;
        event EventHandler<EventArgsForSubscriberPay> PayEvent;
        PortMode Call(int number);
        PortMode Answer();
        void HangUpPhone();
        void ViewStatisticFilterByDate(DateTime firstDay, DateTime lastDay);
        void ViewStatisticFilterByCostCall(DateTime startDate, DateTime endDate);
        void ViewStatisticFilterBySubscriber(DateTime startDate, DateTime endDate);
        void ChangeTariffPlan(IStandartTariffPlan tariffPlan);
        void Pay(int amountOfMoney);
    }
}