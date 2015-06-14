using System;

namespace CheckPoint3ATS
{
    public interface ICallInfoForATS : ICallInfo
    {
        TimeSpan StopCall { get; set; }
        int NumberOfIncomingCall { get; set; }
        int NumberOfOutgoingCall { get; set; }
    }
}