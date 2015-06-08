using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    interface ITerminal
    {
        string Model { get; set; }
        int Cost { get; set; }
        int TerminalId { get; set; }

        event Func<ISubscriber, int, PortMode> DialingEvent;
        event EventHandler<EventArgForEndCall> EndCallEvent;
        event Func<ISubscriber, PortMode> PickUpEvent;

        PortMode Dialing(ISubscriber sender, int phoneNumber);
        PortMode PickUp(ISubscriber sender);
        void EndCall(object sender, EventArgForEndCall arg);
    }
}
