using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    class Terminal : ITerminal
    {
        private readonly string _model;
        private readonly int _cost;
        private readonly int _terminalId;

        public event Func<ISubscriber, PortMode> PickUpEvent;
        public event Func<ISubscriber, int, PortMode> DialingEvent;
        public event EventHandler<EventArgForTerminalEndCall> HangUpPhoneEvent;
        public event EventHandler<EventArgs> DisconnectFromPortEvent;

        public Terminal()
        { 
        }

        public Terminal(string model, int cost, int terminalId)
        {
            _model = model;
            _cost = cost;
            _terminalId = terminalId;
        }

        public string Model
        {
            get { return _model; }
        }

        public int Cost
        {
            get { return _cost;}
            
        }

        public int TerminalId
        {
            get {return _terminalId ;}
            
        }

        public void DisconnectFromPort(ISubscriber sender)
        {
            OnDisconnectFromPort(sender);
        }

        public PortMode PickUp(ISubscriber sender)
        {
            return OnPickUpEvent(sender);
        }

        public PortMode Dialing(ISubscriber sender, int phoneNumber)
        {
            return OnDialingEvent(sender, phoneNumber);
        }

        public void HangUpPhone(object sender, EventArgForTerminalEndCall arg)
        {
            OnHangUpPhoneEvent(sender, arg);
        }

        protected void OnDisconnectFromPort(ISubscriber sender)
        {
            if (DisconnectFromPortEvent != null)
            {
                DisconnectFromPortEvent(sender, null);
            }
        }

        protected PortMode OnPickUpEvent(ISubscriber sender)
        {
            PortMode portMode = PortMode.NoPort;

            if (PickUpEvent != null)
            {
                portMode = PickUpEvent(sender);
            }

            return portMode;
        }

        protected PortMode OnDialingEvent(ISubscriber sender, int phoneNumber)
        {
            PortMode portMode = PortMode.NoPort;

            if (DialingEvent != null)
            {
                portMode = DialingEvent(sender, phoneNumber);
            }

            return portMode;
        }

        protected void OnHangUpPhoneEvent(object sender, EventArgForTerminalEndCall arg)
        {
            if (HangUpPhoneEvent != null)
            {
                HangUpPhoneEvent(sender, arg);
            }
        }
    }
}
