using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace CheckPoint3ATS
{
    interface IBillingSystem
    {
        ReadOnlyCollection<ISubscriberStatistics> SubscribersStatistics { get; }
        event EventHandler<EventArgs> PayDateEvent;
        void RegistrationATS(IATS ats);
        void InstallTime(Time time);
        List<int> ListOfDebtors { get; }
        DateTime Date { get; set; }
        void AddSubscriber(ISubscriberStatistics subcriberStat);
        void DelSubscriber(int contractNumber); 
    }
}
