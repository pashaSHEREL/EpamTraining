using System;

namespace CheckPoint3ATS
{
    public class EventArgsForTerminalEndCall : EventArgs
    {
        public EventArgsForTerminalEndCall()
        {
        }

        public EventArgsForTerminalEndCall(int numberPhone)
        {
            NumberPhone = numberPhone;
        }

        public int NumberPhone { get; set; }
    }
}