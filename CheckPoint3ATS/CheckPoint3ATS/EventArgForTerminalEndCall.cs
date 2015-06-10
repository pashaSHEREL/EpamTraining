using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    class EventArgForTerminalEndCall:EventArgs
    {
        public EventArgForTerminalEndCall()
        { 
        }

        public EventArgForTerminalEndCall(int numberPhone)
        {
            NumberPhone = numberPhone;
        }

        public int NumberPhone
        {
            get;
            set;
        }
    }
}
