using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace CheckPoint3ATS
{
    class BillingSystem:IBillingSystem
    {
        private List<ISubscriberStatistics> subscribersStatistics = new List<ISubscriberStatistics>();
        private DateTime time = DateTime.Now;
        public ReadOnlyCollection<ISubscriberStatistics> SubscribersStatistics
        {
            get 
            {
                ReadOnlyCollection<ISubscriberStatistics> rColl=new ReadOnlyCollection<ISubscriberStatistics>(subscribersStatistics);
                return rColl;
            }
        }

        public void RegistrationATS(IATS ats)
        {
            ats.FinishCallEvent += this.EventHadlerEndCallOnATS;
        }

        protected void EventHadlerEndCallOnATS(object obj, EventArgsForATSFinishCall args)
        {
            //будет дозаполняться subscribersStatistics, смоделировать течение времени????
        }

        public void AddSubscriber(ISubscriberStatistics subcriberStat)
        {
            subscribersStatistics.Add(subcriberStat);
        }

        public void DelSubscriber(int contractNumber)
        {
            for (int i = 0; i < subscribersStatistics.Count; i++)
            {
                if (subscribersStatistics[i].AccountNumber==contractNumber)
                {
                    subscribersStatistics.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
