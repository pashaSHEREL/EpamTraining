using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CheckPoint3ATS
{
    interface ICallInfo
    {
        bool OutgoingCall { get; set; }
        TimeSpan StartCall { get; set; }
        TimeSpan DurationCall { get; set; }
        DateTime DayCall { get; set; }
        int NumberOfSubscriber { get; set; }
    }
}
