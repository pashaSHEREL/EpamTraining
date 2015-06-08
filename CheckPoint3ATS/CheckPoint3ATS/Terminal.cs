using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    class Terminal : ITerminal
    {
        public event Func<ISubscriber, PortMode> PickUpEvent;
        public event Func<ISubscriber, int, PortMode> DialingEvent;
        public event EventHandler<EventArgForEndCall> EndCallEvent;

        public string Model
        {
            get;
            set;
        }

        public int Cost
        {
            get;
            set;
        }

        public int TerminalId
        {
            get;
            set;
        }

        public PortMode PickUp(ISubscriber sender)
        {
            return OnPickUp(sender);
        }

        protected PortMode OnPickUp(ISubscriber sender)
        {
            PortMode portMode = new PortMode();

            if (PickUpEvent != null)
            {
                portMode = PickUpEvent(sender);
            }

            return portMode;
        }

        public PortMode Dialing(ISubscriber sender, int phoneNumber)
        {
            return OnDialing(sender, phoneNumber);
        }

        protected PortMode OnDialing(ISubscriber sender, int phoneNumber)
        {
            PortMode portMode = new PortMode();

            if (DialingEvent != null)
            {
                portMode = DialingEvent(sender, phoneNumber);
            }

            return portMode;
        }

        public void EndCall(object sender, EventArgForEndCall arg)
        {
            OnEndCall(sender, arg);
        }

        protected void OnEndCall(object sender, EventArgForEndCall arg)
        {
            if (EndCallEvent != null)
            {
                EndCallEvent(sender, arg);
            }
        }
    }
}
