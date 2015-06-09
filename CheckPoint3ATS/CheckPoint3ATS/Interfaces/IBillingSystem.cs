using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace CheckPoint3ATS
{
    interface IBillingSystem
    {
        ReadOnlyCollection<ICallInfo> CallsInfo { get; }
    }
}
