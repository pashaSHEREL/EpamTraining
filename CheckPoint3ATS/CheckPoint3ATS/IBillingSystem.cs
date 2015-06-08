using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    interface IBillingSystem
    {
        List<ISubscriber> Subscriber { get; set; }
        void CallCharges(ITariffPlan tariffPlan, int time);
        void ChangeTheAccountStatus();
    }
}
