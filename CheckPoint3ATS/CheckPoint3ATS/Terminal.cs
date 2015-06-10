using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    class Terminal : ITerminal
    {
        private string model;
        private int cost;
        private int terminalId;

        public event Func<ISubscriber, PortMode> PickUpEvent;
        public event Func<ISubscriber, int, PortMode> DialingEvent;
        public event EventHandler<EventArgForTerminalEndCall> EndCallEvent;

        public Terminal()
        { 
        }

        public Terminal(string model, int cost, int terminalId)
        {
            this.model = model;
            this.cost = cost;
            this.terminalId = terminalId;
        }

        public string Model
        {
            get { return model; }
        }

        public int Cost
        {
            get { return cost;}
            
        }

        public int TerminalId
        {
            get {return terminalId ;}
            
        }

        public PortMode PickUp(ISubscriber sender)
        {
            return OnPickUp(sender);
        }

        public PortMode Dialing(ISubscriber sender, int phoneNumber)
        {
            return OnDialing(sender, phoneNumber);
        }

        public void EndCall(object sender, EventArgForTerminalEndCall arg)
        {
            OnEndCall(sender, arg);
        }

        protected PortMode OnPickUp(ISubscriber sender)
        {
            PortMode portMode = PortMode.NoPort;

            if (PickUpEvent != null)
            {
                portMode = PickUpEvent(sender);
            }

            return portMode;
        }

        protected PortMode OnDialing(ISubscriber sender, int phoneNumber)
        {
            PortMode portMode = PortMode.NoPort;

            if (DialingEvent != null)
            {
                portMode = DialingEvent(sender, phoneNumber);
            }

            return portMode;
        }

        protected void OnEndCall(object sender, EventArgForTerminalEndCall arg)
        {
            if (EndCallEvent != null)
            {
                EndCallEvent(sender, arg);
            }
        }
    }
}
