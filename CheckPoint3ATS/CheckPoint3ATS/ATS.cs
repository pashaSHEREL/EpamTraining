using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CheckPoint3ATS
{
    public class ATS : IATS
    {
        private DateTime _date;
        private Random _random = new Random();
        private readonly Dictionary<IPort, IPort> _listOfConnections = new Dictionary<IPort, IPort>();
        private readonly int _numberOfAts;
        private readonly List<IPort> _ports = new List<IPort>();

        public ATS()
        {
        }

        public ATS(int numberOfAts, DateTime date)
        {
            _numberOfAts = numberOfAts;
            _date = date;
        }

        public event EventHandler<EventArgsForATSFinishCall> FinishCallEvent;

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

        public void RegistryBilling(IBillingSystem billingSystem)
        {
            billingSystem.PayDateEvent += PayDateEventHandler;
            billingSystem.PaymentIsMadeEvent += PaymentIsMadeEventHandler;
        }

        public void RegistryTerminal(ISubscriber subscriber)
        {
            subscriber.Terminal.PickUpEvent += PickUpEventHandler;
            subscriber.Terminal.DialingEvent += DialingEventHandler;
            subscriber.Terminal.HangUpPhoneEvent += HangUpPhoneEventHandler;
            subscriber.Terminal.DisconnectFromPortEvent += DisconnectFromPortEventHandler;
            subscriber.Terminal.ConnectFromPortEvent += ConnectFromPortEventHandler;
        }

        public void UnRegistryTerminal(ISubscriber subscriber)
        {
            subscriber.Terminal.PickUpEvent -= PickUpEventHandler;
            subscriber.Terminal.DialingEvent -= DialingEventHandler;
            subscriber.Terminal.HangUpPhoneEvent -= HangUpPhoneEventHandler;
            subscriber.Terminal.DisconnectFromPortEvent -= DisconnectFromPortEventHandler;
            subscriber.Terminal.ConnectFromPortEvent -= ConnectFromPortEventHandler;
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

        protected void PaymentIsMadeEventHandler(object obj, EventArgs e)
        {
            ISubscriberStatistics subscriberStat = obj as ISubscriberStatistics;

            if (subscriberStat != null)
            {
                foreach (var variable in _ports)
                {
                    if (subscriberStat.PhoneNumber == variable.PhoneNumber)
                    {
                        variable.Mode = PortMode.On;
                    }
                }
            }
        }

        protected void ChangeTimeEventHandler(object obj, EventArgs e)
        {
            Time time = obj as Time;

            if (time != null)
            {
                _date = time.Days;
            }
        }

        protected void ConnectFromPortEventHandler(object obj, EventArgs args)
        {
            ISubscriber subscriber = obj as ISubscriber;

            if (subscriber != null)
            {
                foreach (var item in _ports)
                {
                    if (subscriber.PhoneNumber == item.PhoneNumber)
                    {
                        item.Mode = PortMode.On;
                        break;
                    }
                }
            }
        }

        protected void DisconnectFromPortEventHandler(object obj, EventArgs args)
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

        protected PortMode PickUpEventHandler(ISubscriber sender)
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

        protected PortMode DialingEventHandler(ISubscriber sender, int phoneNumber)
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

            ICallInfo call = new CallInfoForATS();

            if (Equals(outgoingCallPort, incomingCallPort))
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

        protected PortMode ActionForPortModeNoPortDefault(IPort outgoingCallPort)
        {
            outgoingCallPort.Mode = PortMode.NoPort;
            return outgoingCallPort.Mode;
        }

        protected PortMode ActionForPortModeConnected(IPort outgoingCallPort, ICallInfo call)
        {
            outgoingCallPort.Mode = PortMode.ShortBeeps;
            OnFinisCallEvent(this, new EventArgsForATSFinishCall(call));
            return outgoingCallPort.Mode;
        }

        protected PortMode ActionForPortModeTone(IPort outgoingCallPort, ICallInfo call, IPort incomingCallPort)
        {
            outgoingCallPort.Mode = PortMode.ShortBeeps;

            if (!Equals(outgoingCallPort, incomingCallPort))
            {
                OnFinisCallEvent(this, new EventArgsForATSFinishCall(call));
            }

            return outgoingCallPort.Mode;
        }

        protected PortMode ActionForPortModeOffPause(IPort outgoingCallPort)
        {
            outgoingCallPort.Mode = PortMode.NotAvailable;
            return outgoingCallPort.Mode;
        }

        protected PortMode ActionForPortModeOn(IPort outgoingCallPort, IPort incomingCallPort)
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

        protected ICallInfo CreateCallInfo(IPort outgoingCallPort, IPort incomingCallPort, bool zeroCall)
        {
            int tH = _random.Next(0, 24);
            int tM = _random.Next(0, 60);
            int tS = _random.Next(0, 60);

            TimeSpan start = new TimeSpan(tH, tM, tS);
            TimeSpan stop = new TimeSpan(_random.Next(tH, 24), _random.Next(tM, 60), _random.Next(tS, 60));

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

        protected void HangUpPhoneEventHandler(object sender, EventArgsForTerminalEndCall args)
        {
            ICallInfo call = new CallInfoForATS();
            Dictionary<IPort, IPort> listOfConnectionsCopy = _listOfConnections.ToDictionary(item => item.Key,
                item => item.Value);

            foreach (var item in listOfConnectionsCopy)
            {
                if (item.Key.PhoneNumber == args.NumberPhone
                    || item.Value.PhoneNumber == args.NumberPhone)
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
                if (item.PhoneNumber == args.NumberPhone && item.Mode != PortMode.Off)
                {
                    item.Mode = PortMode.On;
                }
            }
        }
    }
}