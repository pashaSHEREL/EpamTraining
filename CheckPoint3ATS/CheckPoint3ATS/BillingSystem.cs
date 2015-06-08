using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    interface BillingSystem
    {
        List<ISubscriber> Subscriber { get; }
        void CallCharges(TariffPlan tariffPlan, int time);
        void ChangeTheAccountStatus();
    }
}
