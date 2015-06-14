﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace CheckPoint3ATS
{
    public class BillingSystem : IBillingSystem
    {
        private List<ISubscriberStatistics> _subscribersStatistics = new List<ISubscriberStatistics>();
        private DateTime _date;
        private const int PayDay = 25;
        private List<int> _listOfDebtors = new List<int>();

        public event EventHandler<EventArgs> PayDateEvent;
        public event EventHandler<EventArgs> PaymentIsMade; 

        public ReadOnlyCollection<ISubscriberStatistics> SubscribersStatistics
        {
            get { return new ReadOnlyCollection<ISubscriberStatistics>(_subscribersStatistics); }
        }

        public ReadOnlyCollection<int> ListOfDebtors
        {
            get { return new ReadOnlyCollection<int>(_listOfDebtors); }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public void RegistrationATS(IATS ats)
        {
            ats.FinishCallEvent += FinishCallOnAtsEventHadler;
        }

        public void InstallTime(Time time)
        {
            time.ChangeTimeEvent += ChangeTimeEventHandler;
        }

        public void AddSubscriber(ISubscriberStatistics subcriberStat)
        {
            _subscribersStatistics.Add(subcriberStat);
            _subscribersStatistics.Last().PaymentIsMade += PaymentIsMadeEventHandler;
        }

        public void DelSubscriber(int contractNumber)
        {
            for (int i = 0; i < _subscribersStatistics.Count; i++)
            {
                if (_subscribersStatistics[i].AccountNumber == contractNumber)
                {
                    _subscribersStatistics.RemoveAt(i);
                    _subscribersStatistics[i].PaymentIsMade -= PaymentIsMadeEventHandler;
                    break;
                }
            }
        }

        protected void OnPaymentIsMade(object obj, EventArgs e)
        {
            if (PaymentIsMade!=null)
            {
                PaymentIsMade(obj, e);
            }
        }

        protected void PaymentIsMadeEventHandler(object obj, EventArgs e)
        {
            OnPaymentIsMade(obj,e);
        }

        protected void FinishCallOnAtsEventHadler(object obj, EventArgsForATSFinishCall args)
        {
            foreach (var item in _subscribersStatistics)
            {
                if (item.PhoneNumber == ((CallInfoForATS) args.CallInfo).NumberOfOutgoingCall)
                {
                    CreateCallInfo(args, item, true);
                    item.Balance -= item.CallsInfo.OfType<CallInfoForBilling>().Last().CostCall;
                }
                else
                {
                    if (item.PhoneNumber == ((CallInfoForATS) args.CallInfo).NumberOfIncomingCall)
                    {
                        CreateCallInfo(args, item, false);
                    }
                }
            }
        }

        protected void CreateCallInfo(EventArgsForATSFinishCall args, ISubscriberStatistics item, bool outgoingCall)
        {
            TimeSpan durationCall = ((CallInfoForATS) args.CallInfo).StopCall -
                                    ((CallInfoForATS) args.CallInfo).StartCall;

            int phoneNumber = outgoingCall
                ? ((CallInfoForATS)args.CallInfo).NumberOfIncomingCall
                : ((CallInfoForATS) args.CallInfo).NumberOfOutgoingCall;

            int costCall = outgoingCall
                ? GetCallCost(item, durationCall)
                : 0;

            item.AddCallInfo(new CallInfoForBilling()
            {
                DayCall = args.CallInfo.DayCall,
                StartCall = args.CallInfo.StartCall,
                DurationCall = durationCall,
                OutgoingCall = outgoingCall,
                PhoneNumber = phoneNumber,
                CostCall = costCall
            });
        }

        protected int GetCallCost(ISubscriberStatistics item, TimeSpan durationCall)
        {
            int costCall;

            int sumMinutes =
                item.CallsInfo.OfType<CallInfoForBilling>()
                    .Sum(callInfoBilling => (int) callInfoBilling.DurationCall.TotalMinutes);

            if (sumMinutes >= item.TariffPlan.FreeMinute)
            {
                costCall = (int) durationCall.TotalMinutes*item.TariffPlan.PricePerMinute;
            }
            else
            {
                if ((sumMinutes + durationCall.TotalMinutes) <= item.TariffPlan.FreeMinute)
                {
                    costCall = 0;
                }
                else
                {
                    costCall = (sumMinutes + (int) durationCall.TotalMinutes - item.TariffPlan.FreeMinute)*
                               item.TariffPlan.PricePerMinute;
                }
            }

            return costCall;
        }


        protected void OnPayDateEvent()
        {
            if (PayDateEvent != null)
            {
                PayDateEvent(this, null);
            }
        }

        protected void ChangeTimeEventHandler(object obj, EventArgs e)
        {
            Time time = obj as Time;
            if (time != null)
            {
                Date = time.Days;
            }
            if (_date.Day >= PayDay)
            {
                _listOfDebtors = _subscribersStatistics.Where(x => x.Balance < 0).Select(x => x.PhoneNumber).ToList();
                OnPayDateEvent();
            }
        }
    }
}