using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace CheckPoint3ATS
{
    public class SubscriberStatistics : ISubscriberStatistics
    {
        private readonly List<ICallInfo> _callsInfo = new List<ICallInfo>();
        private List<IPayment> _payments = new List<IPayment>();
        private readonly int _accountNumber;
        private readonly int _phoneNumber;
        private readonly ITariffPlan _tariffPlan;
        private int _balance;

        public event EventHandler<EventArgs> PaymentIsMade;

        protected void OnPaymetIsMade()
        {
            if (PaymentIsMade!=null)
            {
                PaymentIsMade(this, null);
            }
        }

        public SubscriberStatistics()
        {
            _tariffPlan = new TariffPlan();
        }

        public SubscriberStatistics(int accountNumber, int phoneNumber, ITariffPlan tariffPlan)
        {
            _accountNumber = accountNumber;
            _phoneNumber = phoneNumber;
            _tariffPlan = tariffPlan;
        }

        public int AccountNumber
        {
            get { return _accountNumber; }
        }

        public int Balance
        {
            get { return _balance; }
            set
            {
                _balance = value;
                OnPaymetIsMade();
            }
        }

        public int PhoneNumber
        {
            get { return _phoneNumber; }
        }

        public ITariffPlan TariffPlan
        {
            get { return _tariffPlan; }
        }

        public ReadOnlyCollection<ICallInfo> CallsInfo
        {
            get { return new ReadOnlyCollection<ICallInfo>(_callsInfo.ToList()); }
        }

        public ReadOnlyCollection<IPayment> Payments
        {
            get { return new ReadOnlyCollection<IPayment>(_payments); }
        }

        public void AddCallInfo(ICallInfo callInfo)
        {
            _callsInfo.Add(callInfo);
        }

        public void AddPayment(IPayment payment)
        {
            _payments.Add(payment);
        }
    }
}