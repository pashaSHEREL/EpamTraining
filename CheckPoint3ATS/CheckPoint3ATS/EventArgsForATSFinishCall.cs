using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    class EventArgsForATSFinishCall:EventArgs
    {
        public EventArgsForATSFinishCall()
        {
        }

        public EventArgsForATSFinishCall(ICallInfo callInfo)
        {
            CallInfo = callInfo;
        }

        public ICallInfo CallInfo
        {
            get;
            set;
        }
    }
}
