﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    interface ITerminal
    {
        string Model { get;}
        int Cost { get;}
        int TerminalId { get;}

        event Func<ISubscriber, int, PortMode> DialingEvent;
        event EventHandler<EventArgForTerminalEndCall> EndCallEvent;
        event Func<ISubscriber, PortMode> PickUpEvent;

        PortMode Dialing(ISubscriber sender, int phoneNumber);
        PortMode PickUp(ISubscriber sender);
        void EndCall(object sender, EventArgForTerminalEndCall arg);
    }
}
