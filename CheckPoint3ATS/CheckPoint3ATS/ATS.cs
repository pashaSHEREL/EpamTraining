using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CheckPoint3ATS
{
    internal class ATS : IATS
    {
        private readonly List<IPort> _ports = new List<IPort>();
        private readonly Dictionary<IPort, IPort> _listOfConnections = new Dictionary<IPort, IPort>();
        private readonly int _numberOfAts;
        private DateTime _date = new DateTime();

        public event EventHandler<EventArgsForATSFinishCall> FinishCallEvent;

        public ATS()
        {
        }

        public ATS(int numberOfAts, DateTime date)
        {
            _numberOfAts = numberOfAts;
            _date = date;
        }

        public void AddPort(IPort port)
        {
            bool errorPort = _ports.Any(x => port.Number == x.Number);

            if (errorPort)
            {
                throw new Exception("With this port number is already available");
            }
            else
            {
                _ports.Add(port);
            }
        }

        public ReadOnlyCollection<IPort> Ports
        {
            get { return new ReadOnlyCollection<IPort>(_ports); }
        }

        public int NumberOfATS
        {
            get { return _numberOfAts; }
        }

        public bool FreePorts
        {
            get { return _ports.Any(t => t.Status == PortStatus.Free); }
        }

        public void InstallTime(Time time)
        {
            time.ChangeTimeEvent += ChangeTimeEventHandler;
        }

        protected void ChangeTimeEventHandler(object obj, EventArgs e)
        {
            Time time = obj as Time;
            if (time != null)
            {
                _date = time.Days;
            }
        }

        public void RegistryBilling(IBillingSystem billingSystem)
        {
            billingSystem.PayDateEvent += PayDateEventHandler;
        }

        protected void PayDateEventHandler(object obj, EventArgs e)
        {
            IBillingSystem billingSystem = obj as IBillingSystem;
            if (billingSystem != null)
            {
                foreach (var variable in _ports)
                {
                    if (billingSystem.ListOfDebtors.Contains(variable.PhoneNumber))
                    {
                        variable.Mode = PortMode.Off;
                    }
                }
            }
        }

        public void RegistryTerminal(ISubscriber subscriber)
        {
            subscriber.Terminal.PickUpEvent += PickUpHandler;
            subscriber.Terminal.DialingEvent += DialingHandler;
            subscriber.Terminal.HangUpPhoneEvent += HangUpPhoneHandler;
            subscriber.Terminal.DisconnectFromPortEvent += DisconnectFromPortHandler;
        }

        public void UnRegistryTerminal(ISubscriber subscriber)
        {
            subscriber.Terminal.PickUpEvent -= PickUpHandler;
            subscriber.Terminal.DialingEvent -= DialingHandler;
            subscriber.Terminal.HangUpPhoneEvent -= HangUpPhoneHandler;
            subscriber.Terminal.DisconnectFromPortEvent -= DisconnectFromPortHandler;
        }

        protected void DisconnectFromPortHandler(object obj, EventArgs args)
        {
            ISubscriber subscriber = obj as ISubscriber;
            if (subscriber != null)
            {
                foreach (var item in _ports)
                {
                    if (subscriber.PhoneNumber == item.PhoneNumber)
                    {
                        item.Mode = PortMode.Pause;
                        break;
                    }
                }
            }
        }

        protected PortMode PickUpHandler(ISubscriber sender)
        {
            PortMode portMode = PortMode.NoPort;

            foreach (var item in _ports)
            {
                if (item.PhoneNumber == sender.PhoneNumber)
                {
                    if (item.Mode == PortMode.On)
                    {
                        item.Mode = PortMode.Tone;
                        portMode = item.Mode;
                        break;
                    }
                    else
                    {
                        if (item.Mode == PortMode.LongBeeps)
                        {
                            try
                            {
                                _listOfConnections[item].StopTimer();
                                item.StopTimer();
                                _listOfConnections[item].Mode = PortMode.Connected;
                                item.Mode = PortMode.Connected;
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Operation is not available.");
                            }
                        }

                        portMode = item.Mode;
                    }
                }
            }

            return portMode;
        }

        protected PortMode DialingHandler(ISubscriber sender, int phoneNumber)
        {
            PortMode portMode;
            IPort outgoingCallPort = new Port();
            IPort incomingCallPort = new Port();

            incomingCallPort.Mode = PortMode.NoPort;

            foreach (var item in _ports)
            {
                if (item.PhoneNumber == phoneNumber)
                {
                    incomingCallPort = item;
                    portMode = item.Mode;
                }
                if (item.PhoneNumber == sender.PhoneNumber)
                {
                    outgoingCallPort = item;
                }
            }

            ICallInfo call=null;

            if (!Equals(outgoingCallPort, incomingCallPort))
            {
                call = CreateCallInfo(outgoingCallPort, incomingCallPort, true);
            }
    
            switch (incomingCallPort.Mode)
            {
                case PortMode.Pause:
                    portMode = ActionForPortModeOffPause(outgoingCallPort);
                    break;
                case PortMode.Off:
                    portMode = ActionForPortModeOffPause(outgoingCallPort);
                    break;
                case PortMode.On:
                    portMode = ActionForPortModeOn(outgoingCallPort, incomingCallPort);
                    break;
                case PortMode.Tone:
                    portMode = ActionForPortModeTone(outgoingCallPort, call, incomingCallPort);
                    break;
                case PortMode.Connected:
                    portMode = ActionForPortModeConnected(outgoingCallPort, call);
                    break;
                case PortMode.NoPort:
                    portMode = ActionForPortModeNoPortDefault(outgoingCallPort);
                    break;
                default:
                    portMode = ActionForPortModeNoPortDefault(outgoingCallPort);
                    break;
            }

            return portMode;
        }

        private static PortMode ActionForPortModeNoPortDefault(IPort outgoingCallPort)
        {
            outgoingCallPort.Mode = PortMode.NoPort;
            return outgoingCallPort.Mode;
        }

        private PortMode ActionForPortModeConnected(IPort outgoingCallPort, ICallInfo call)
        {
            outgoingCallPort.Mode = PortMode.ShortBeeps;
            OnFinisCallEvent(this, new EventArgsForATSFinishCall(call));
            return outgoingCallPort.Mode;
        }

        private PortMode ActionForPortModeTone(IPort outgoingCallPort, ICallInfo call,IPort incomingCallPort)
        {
            outgoingCallPort.Mode = PortMode.ShortBeeps;

            if (!Equals(outgoingCallPort, incomingCallPort))
            {
                OnFinisCallEvent(this, new EventArgsForATSFinishCall(call));
            }

            return outgoingCallPort.Mode;
        }

        private PortMode ActionForPortModeOffPause(IPort outgoingCallPort)
        {
            outgoingCallPort.Mode = PortMode.NotAvailable;
            return outgoingCallPort.Mode;
        }

        private PortMode ActionForPortModeOn(IPort outgoingCallPort, IPort incomingCallPort)
        {
            outgoingCallPort.Mode = PortMode.LongBeeps;
            incomingCallPort.Mode = PortMode.LongBeeps;
            _listOfConnections.Add(incomingCallPort, outgoingCallPort);

            incomingCallPort.StartTimer();
            outgoingCallPort.StartTimer();

            incomingCallPort.FinishTimerEvent += (o, e) =>
            {
                if (incomingCallPort.Mode == PortMode.On)
                {
                    _listOfConnections.Remove(incomingCallPort);
                }
            };

            return outgoingCallPort.Mode;
        }

        private ICallInfo CreateCallInfo(IPort outgoingCallPort, IPort incomingCallPort, bool zeroCall)
        {
            Random rH = new Random();
            Random rM = new Random();

            int tH = rH.Next(0, 24);
            int tM = rH.Next(0, 60);
            int tS = rH.Next(0, 60);

            TimeSpan start = new TimeSpan(tH, tM, tS);
            TimeSpan stop = new TimeSpan(rH.Next(tH, 24), rM.Next(tM, 60), rM.Next(tS, 60));
            if (zeroCall)
            {
                stop = start;
            }

            CallInfoForATS call = new CallInfoForATS()
            {
                NumberOfIncomingCall = incomingCallPort.PhoneNumber,
                NumberOfOutgoingCall = outgoingCallPort.PhoneNumber,
                DayCall = _date,
                StartCall = start,
                StopCall = stop
            };

            return call;
        }

        protected void OnFinisCallEvent(object obj, EventArgsForATSFinishCall e)
        {
            if (FinishCallEvent != null)
            {
                FinishCallEvent(obj, e);
            }
        }

        protected void HangUpPhoneHandler(object sender, EventArgForTerminalEndCall arg)
        {
            ICallInfo call;
            Dictionary<IPort, IPort> listOfConnectionsCopy = _listOfConnections.ToDictionary(item => item.Key,
                item => item.Value);

            foreach (var item in listOfConnectionsCopy)
            {
                if (item.Key.PhoneNumber == arg.NumberPhone
                    || item.Value.PhoneNumber == arg.NumberPhone)
                {
                    item.Key.StopTimer();
                    item.Value.StopTimer();
                    if (item.Key.Mode == PortMode.Connected && item.Value.Mode == PortMode.Connected)
                    {
                        call = CreateCallInfo(item.Value, item.Key, false);
                    }
                    else
                    {
                        call = CreateCallInfo(item.Value, item.Key, true);
                    }
                    OnFinisCallEvent(this, new EventArgsForATSFinishCall(call));
                    item.Key.Mode = PortMode.On;
                    item.Value.Mode = PortMode.On;
                    _listOfConnections.Remove(item.Key);
                    break;
                }
            }

            foreach (var item in _ports)
            {
                if (item.PhoneNumber == arg.NumberPhone && item.Mode!=PortMode.Off)
                {
                    item.Mode = PortMode.On;
                }
            }
        }
    }
}