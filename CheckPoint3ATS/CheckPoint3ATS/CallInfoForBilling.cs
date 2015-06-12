using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    class CallInfoForBilling:ICallInfoForBilling
    {

        public TimeSpan DurationCall
        {
            get;
            set;
        }

        public int CostCall
        {
            get;
            set;
        }

        public int PhoneNumber
        {
            get;
            set;
        }

        public bool OutgoingCall
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
