using System;


namespace CheckPoint3ATS
{
    public class EventArgsForATSFinishCall : EventArgs
    {
        public EventArgsForATSFinishCall()
        {
        }

        public EventArgsForATSFinishCall(ICallInfo callInfo)
        {
            CallInfo = callInfo;
        }

        public ICallInfo CallInfo { get; set; }
    }
}