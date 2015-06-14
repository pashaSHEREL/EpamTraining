using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    public class EventArgsForSubscriberPay : EventArgs
    {
        

        public EventArgsForSubscriberPay()
        {
        }

        public EventArgsForSubscriberPay(int amountOfPay, int contractId)
        {
            AmountOfPay = amountOfPay;
            ContractId = contractId;
        }

        public int ContractId { get; private set; }

        public int AmountOfPay { get; private set; }
    }
}