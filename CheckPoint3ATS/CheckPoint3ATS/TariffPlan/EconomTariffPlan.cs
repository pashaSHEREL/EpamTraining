using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    public class EconomTariffPlan : ILicenseFeeTariffPlan
    {
        public int LicenseFee { get; set; }

        public int PricePerMinute { get; set; }
    }
}