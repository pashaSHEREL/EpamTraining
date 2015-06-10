using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    class SubscriberStatistics:ISubscriberStatistics
    {
        List<ICallInfo> callsInfo = new List<ICallInfo>();
        List<IPayment> payments = new List<IPayment>();
        int accountNumber;
        int phoneNumber;
        ITariffPlan tariffPlan;

        public SubscriberStatistics()
        {
        }

        public SubscriberStatistics(int accountNumber, int phoneNumber, ITariffPlan tariffPlan)
        {
           this.accountNumber=accountNumber;
           this.phoneNumber=phoneNumber;
           this.tariffPlan=tariffPlan;
        }

        public int AccountNumber
        {
            get { return accountNumber;}
        }

        public int Balance
        {
            get;
            set;
        }

        public int PhoneNumber
        {
            get { return phoneNumber;}
        }

        public ITariffPlan TariffPlan
        {
            get { return tariffPlan;}
        }

        public List<ICallInfo> CallsInfo
        {
            get { return callsInfo.ToList();}
        }

        public List<IPayment> Payments
        {
            get { return payments.ToList(); }

        }

        public void AddCallInfo(ICallInfo callInfo)
        {
            callsInfo.Add(callInfo);
        }

    }
}
