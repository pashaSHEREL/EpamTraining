using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    public class EventArgsForSubscriberChangeTariffPlan:EventArgs
    {
        private IStandartTariffPlan _tariffPlan;
        private IContract _contract=new Contract();

        public EventArgsForSubscriberChangeTariffPlan()
        {
        }

        public EventArgsForSubscriberChangeTariffPlan(IStandartTariffPlan tariffPlan, IContract contract)
        {
            _tariffPlan = tariffPlan;
            _contract = contract;
        }

        public IStandartTariffPlan TariffPlan
        {
            get { return _tariffPlan; }
        }

        public IContract GetContract
        {
            get { return _contract; }
        }
    }
}