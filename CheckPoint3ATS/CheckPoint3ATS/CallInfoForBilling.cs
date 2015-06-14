using System;

namespace CheckPoint3ATS
{
    public class CallInfoForBilling : ICallInfoForBilling
    {
        public TimeSpan DurationCall { get; set; }

        public int CostCall { get; set; }

        public int PhoneNumber { get; set; }

        public bool OutgoingCall { get; set; }

        public TimeSpan StartCall { get; set; }

        public DateTime DayCall { get; set; }
    }
}