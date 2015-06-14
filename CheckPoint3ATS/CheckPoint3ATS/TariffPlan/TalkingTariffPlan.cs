using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    public class TalkingTariffPlan : IFreeMinutesTariffPlan
    {
        public int FreeMinute { get; set; }

        public int PricePerMinute{ get; set; } 
    }
}