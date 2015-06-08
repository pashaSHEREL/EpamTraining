using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    interface ITariffPlan
    {
        string Name { get; set; }
        int LicenseFee { get; set; }
        int PricePerMinute { get; set; }
        int FreeMinute { get; set; }
    }
}
