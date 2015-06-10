using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    class CallInfoForATS:ICallInfoForATS
    {
        public TimeSpan StopCall
        {
            get;
            set;
        }

        public int NumberOfIncomingCall
        {
            get;
            set;
        }

        public int NumberOfOutgoingCall
        {
            get;
            set;
        }

        public TimeSpan StartCall
        {
            get;
            set;
        }

        public DateTime DayCall
        {
            get;
            set;
        }
    }
}
