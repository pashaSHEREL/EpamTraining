using System;
using System.Collections.ObjectModel;

namespace CheckPoint3ATS
{
    public interface IBillingSystem
    {
        ReadOnlyCollection<ISubscriberStatistics> SubscribersStatistics { get; }
        ReadOnlyCollection<int> ListOfDebtors { get; }
        DateTime Date { get; set; }

        event EventHandler<EventArgs> PayDateEvent;
        event EventHandler<EventArgs> PaymentIsMade;

        void RegistrationATS(IATS ats);
        void InstallTime(Time time);
        void AddSubscriber(ISubscriberStatistics subcriberStat);
        void DelSubscriber(int contractNumber);
    }
}