using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace CheckPoint3ATS
{
    public enum PortStatus
    {
        Free,
        Busy,
        Reserved,
        Beaten
    }

    public enum PortMode
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

    public interface IPort
    {
        int Number { get; }
        int PhoneNumber { get; set; }
        PortStatus Status { get; set; }
        PortMode Mode { get; set; }

        event EventHandler<EventArgs> FinishTimerEvent;

        void StartTimer();
        void StopTimer();
    }
}