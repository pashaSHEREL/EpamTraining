using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    interface ICallInfoForBilling:ICallInfo
    {
        TimeSpan DurationCall { get; set; }
        int CallCharges { get; set; }
        int PhoneNumber { get; set; }
        bool OutgoingCall { get; set; }
    }
}
