using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

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
        Off,
        On,
        Tone,
        ShortBeeps,
        LongBeeps,
        NoPort,
        NotAvailable,
        Connected,
        Pause
    }

    interface IPort
    {
        int Number { get; }
        int PhoneNumber { get; set; }
        PortStatus Status { get; set; }
        PortMode Mode { get; set; }
        void StartTimer();
        void StopTimer();
        event EventHandler<EventArgs> FinishTimerEvent;
    }
}
