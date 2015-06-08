using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    enum PortStatus
    {
        Free,
        Busy,
        Reserved,
        Beaten
    }

    enum PortMode
    {
        Tone,
        ShortBeeps,
        LongBeeps
    }

    interface IPort
    {
        int Number { get; set; }
        int PhoneNumber { get; set; }
        PortStatus Status { get; set; }
        PortMode Mode { get; set; }
        bool ConnectStatus { get; set; }
        bool Off { get; set; }
    }
}
