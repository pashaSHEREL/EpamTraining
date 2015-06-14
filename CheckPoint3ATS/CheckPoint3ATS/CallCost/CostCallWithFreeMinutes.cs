using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    internal class CostCallWithFreeMinutes : ICallCost
    {
        public int GetCostCall(int duration, ISubscriberStatistics item)
        {
            int costCall=0;

            IFreeMinutesTariffPlan tariffPlan = item.TariffPlan as IFreeMinutesTariffPlan;

            if (tariffPlan != null)
            {
                int sumMinutes =
                    item.CallsInfo.OfType<CallInfoForBilling>()
                        .Sum(callInfoBilling => (int) callInfoBilling.DurationCall.TotalMinutes);

                if (sumMinutes >= tariffPlan.FreeMinute)
                {
                    costCall = duration*item.TariffPlan.PricePerMinute;
                }
                else
                {
                    if ((sumMinutes + duration) <= tariffPlan.FreeMinute)
                    {
                        costCall = 0;
                    }
                    else
                    {
                        costCall = (sumMinutes + duration - tariffPlan.FreeMinute)*
                                   item.TariffPlan.PricePerMinute;
                    }
                }
            }

            return costCall;
        }
    }
}