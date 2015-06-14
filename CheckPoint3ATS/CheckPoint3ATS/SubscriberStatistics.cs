using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace CheckPoint3ATS
{
    public class SubscriberStatistics : ISubscriberStatistics
    {
        private int _balance;
        private List<IPayment> _payments = new List<IPayment>();
        private readonly int _accountNumber;
        private readonly List<ICallInfo> _callsInfo = new List<ICallInfo>();
        private readonly int _phoneNumber;
        private IStandartTariffPlan _tariffPlan;
        private DateTime _changeTariffPlanDay;

        public SubscriberStatistics()
        {
            _tariffPlan = new StandartTariffPlan();
        }

        public SubscriberStatistics(int accountNumber, int phoneNumber, IStandartTariffPlan tariffPlan)
        {
            _accountNumber = accountNumber;
            _phoneNumber = phoneNumber;
            _tariffPlan = tariffPlan;
        }

        public event EventHandler<EventArgs> PaymentIsMade;

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

        public IStandartTariffPlan TariffPlan
        {
            get { return _tariffPlan; }
            set { _tariffPlan = value; }
        }

        public ReadOnlyCollection<ICallInfo> CallsInfo
        {
            get { return new ReadOnlyCollection<ICallInfo>(_callsInfo.ToList()); }
        }

        public ReadOnlyCollection<IPayment> Payments
        {
            get { return new ReadOnlyCollection<IPayment>(_payments); }
        }

        public DateTime ChangeTariffPlanDay
        {
            get { return _changeTariffPlanDay; }
            set { _changeTariffPlanDay = value; }
        }

        public void AddCallInfo(ICallInfo callInfo)
        {
            _callsInfo.Add(callInfo);
        }

        public void AddPayment(IPayment payment)
        {
            _payments.Add(payment);
        }

        protected void OnPaymetIsMade()
        {
            if (PaymentIsMade != null)
            {
                PaymentIsMade(this, null);
            }
        }
    }
}