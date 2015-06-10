using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    interface ICallInfoForATS:ICallInfo
    {
        TimeSpan StopCall { get; set; }
        int NumberOfIncomingCall { get; set; }
        int NumberOfOutgoingCall { get; set; }
    }
}
