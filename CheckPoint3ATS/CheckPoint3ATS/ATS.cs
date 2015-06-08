using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Timers;
using System.Threading;

namespace CheckPoint3ATS
{
    class ATS:IATS
    {
        private List<IPort> ports = new List<IPort>();
        private Dictionary<IPort, IPort> listOfConnections = new Dictionary<IPort, IPort>();
        private int numberOfATS;

        public ATS()
        { 
        }

        public ATS(List<IPort> ports, int numberOfATS)
        {
            this.ports = ports;
            this.numberOfATS = numberOfATS;
        }

        public ReadOnlyCollection<IPort> Ports
        {
            get 
            {
                return new ReadOnlyCollection<IPort>(ports);
            }
        }

        public int NumberOfATS
        {
            get { return numberOfATS; }
        }

        public bool FreePorts
        {
            get
            {
                bool free = false;

                for (int i = 0; i < ports.Count; i++)
                {
                    if (ports[i].Status == PortStatus.Free)
                    {
                        free = true;
                        break;
                    }
                }

                return free;
            }
        }

        public void RegistryTerminal(ISubscriber subscriber)
        {
            subscriber.Terminal.PickUpEvent += PickUpHandler;
            subscriber.Terminal.DialingEvent += DialingHandler;
            subscriber.Terminal.EndCallEvent += EndCallHandler;
        }

        protected void EndCallHandler(object sender, EventArgForEndCall arg)
        {
            Dictionary<IPort, IPort> listOfConnectionsCopy = new Dictionary<IPort, IPort>();

            foreach (var item in listOfConnections)
	        {
                listOfConnectionsCopy.Add(item.Key,item.Value);
	        }

            foreach (var item in listOfConnectionsCopy)
            {
                if (item.Key.PhoneNumber == arg.NumberPhone
                    || item.Value.PhoneNumber == arg.NumberPhone)
                {
                    item.Key.Mode = PortMode.On;
                    item.Value.Mode = PortMode.On;
                    listOfConnections.Remove(item.Key);
                    break;
                }
            }

            foreach (var item in ports)
            {
                if (item.PhoneNumber == arg.NumberPhone)
                {
                    item.Mode = PortMode.On;
                }
            }
            //timer.Stop();
        }

        

        protected PortMode PickUpHandler(ISubscriber sender)
        {
            PortMode portMode = PortMode.NoPort; 

            foreach (var item in ports)
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
                                listOfConnections[item].Mode = PortMode.Connected;
                                item.Mode = PortMode.Connected;
                                listOfConnections[item].StopTimer();
                                item.StopTimer();
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
            PortMode portMode =new PortMode();
            IPort outgoingCallPort = new Port();
            IPort incomingCallPort = new Port();

            incomingCallPort.Mode = PortMode.NoPort;

            foreach (var item in ports)
            {
                if (item.PhoneNumber==phoneNumber)
                {
                    incomingCallPort = item;
                    portMode = item.Mode;
                }
                if (item.PhoneNumber == sender.PhoneNumber)
                {
                    outgoingCallPort = item;
                }
            }

            switch (incomingCallPort.Mode)
            {
                case PortMode.Off:
                    outgoingCallPort.Mode = PortMode.NotAvailable;
                    portMode = outgoingCallPort.Mode;
                    break;
                case PortMode.On:

                    outgoingCallPort.Mode = PortMode.LongBeeps;
                    incomingCallPort.Mode = PortMode.LongBeeps;
                    listOfConnections.Add(incomingCallPort,outgoingCallPort);
                    portMode = outgoingCallPort.Mode;

                    incomingCallPort.FinishTimerEvent += (object o, EventArgs e) =>
                    {
                        if (incomingCallPort.Mode == PortMode.On)
                        {
                            listOfConnections.Remove(incomingCallPort);
                        }
                    };

                    incomingCallPort.StartTimer();
                    outgoingCallPort.StartTimer();
                    break;
                case PortMode.Tone:
                    outgoingCallPort.Mode = PortMode.ShortBeeps;
                    portMode = outgoingCallPort.Mode;
                    break;
                case PortMode.NoPort:
                    portMode = incomingCallPort.Mode;
                    outgoingCallPort.Mode = PortMode.NoPort;
                    break;
                default:
                    portMode = PortMode.NoPort;
                    outgoingCallPort.Mode = PortMode.NoPort;
                    break;
            }

            return portMode; 
        }
    }
}
