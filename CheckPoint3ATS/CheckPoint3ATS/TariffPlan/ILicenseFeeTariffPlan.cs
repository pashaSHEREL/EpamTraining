using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    public interface ILicenseFeeTariffPlan:IStandartTariffPlan
    {
        int LicenseFee { get; set; }
    }
}
