using System;

namespace CheckPoint3ATS
{
    public class EventArgForTerminalEndCall : EventArgs
    {
        public EventArgForTerminalEndCall()
        {
        }

        public EventArgForTerminalEndCall(int numberPhone)
        {
            NumberPhone = numberPhone;
        }

        public int NumberPhone { get; set; }
    }
}