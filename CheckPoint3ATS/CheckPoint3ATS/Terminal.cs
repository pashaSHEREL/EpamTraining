using System;

namespace CheckPoint3ATS
{
    public class Terminal : ITerminal
    {
        private readonly int _cost;
        private readonly string _model;
        private readonly int _terminalId;

        public Terminal()
        {
        }

        public Terminal(string model, int cost, int terminalId)
        {
            _model = model;
            _cost = cost;
            _terminalId = terminalId;
        }

        public event Func<ISubscriber, PortMode> PickUpEvent;
        public event Func<ISubscriber, int, PortMode> DialingEvent;
        public event EventHandler<EventArgsForTerminalEndCall> HangUpPhoneEvent;
        public event EventHandler<EventArgs> DisconnectFromPortEvent;
        public event EventHandler<EventArgs> ConnectFromPortEvent;

        public string Model
        {
            get { return _model; }
        }

        public int Cost
        {
            get { return _cost; }
        }

        public int TerminalId
        {
            get { return _terminalId; }
        }

        public void DisconnectFromPort(ISubscriber sender)
        {
            OnDisconnectFromPort(sender);
        }

        public void ConnectFromPort(ISubscriber sender)
        {
            OnConnectFromPort(sender);
        }

        public PortMode PickUp(ISubscriber sender)
        {
            return OnPickUpEvent(sender);
        }

        public PortMode Dialing(ISubscriber sender, int phoneNumber)
        {
            return OnDialingEvent(sender, phoneNumber);
        }

        public void HangUpPhone(object sender, EventArgsForTerminalEndCall args)
        {
            OnHangUpPhoneEvent(sender, args);
        }

        protected void OnConnectFromPort(ISubscriber sender)
        {
            if (ConnectFromPortEvent != null)
            {
                ConnectFromPortEvent(sender, null);
            }
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

        protected void OnHangUpPhoneEvent(object sender, EventArgsForTerminalEndCall args)
        {
            if (HangUpPhoneEvent != null)
            {
                HangUpPhoneEvent(sender, args);
            }
        }
    }
}