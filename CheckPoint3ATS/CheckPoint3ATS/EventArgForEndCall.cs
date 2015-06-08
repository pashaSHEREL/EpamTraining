using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    class EventArgForEndCall:EventArgs
    {
        public EventArgForEndCall()
        { 
        }

        public EventArgForEndCall(int numberPhone)
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
