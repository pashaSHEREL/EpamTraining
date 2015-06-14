using System;

namespace CheckPoint3ATS
{
    public interface ICallInfoForBilling : ICallInfo
    {
        TimeSpan DurationCall { get; set; }
        int CostCall { get; set; }
        int PhoneNumber { get; set; }
        bool OutgoingCall { get; set; }
    }
}