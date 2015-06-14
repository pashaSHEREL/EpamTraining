using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    class CostCallWithoutFreeMinutes:ICallCost
    {
        public int GetCostCall(int duration, ISubscriberStatistics item)
        {
            return duration*item.TariffPlan.PricePerMinute;
        }
    }
}
