using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    internal class TariffPlan : ITariffPlan
    {
        public string Name
        {
            get;
            set;
        }

        public int LicenseFee
        {
            get;
            set;
        }

        public int PricePerMinute
        {
            get;
            set;
        }

        public int FreeMinute
        {
            get;
            set;
        }
    }
}