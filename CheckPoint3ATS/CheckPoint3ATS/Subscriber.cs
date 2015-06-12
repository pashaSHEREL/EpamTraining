using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    internal class Subscriber : ISubscriber
    {
        private readonly string _nameL;
        private readonly string _nameF;
        private readonly string _address;

        public event Func<ISubscriber, List<ICallInfo>> GetStatisticEvent;

        public Subscriber()
        {
        }

        public Subscriber(string nameL, string nameF, string address)
        {
            _nameL = nameL;
            _nameF = nameF;
            _address = address;
        }

        public string NameL
        {
            get { return _nameL; }
        }

        public string NameF
        {
            get { return _nameF; }
        }

        public string Address
        {
            get { return _address; }
        }

        public int PhoneNumber { get; set; }

        public ITerminal Terminal { get; set; }

        public IContract Contract { get; set; }

        public List<ICallInfo> GetStatistic(DateTime firstDay, DateTime lastDay)
        {
            return OnGetStatisticEvent()
                .Where(
                    x => (((CallInfoForBilling) x).DayCall > firstDay) && (((CallInfoForBilling) x).DayCall < lastDay)).Select(x=>x).ToList();
        }

        protected List<ICallInfo> OnGetStatisticEvent()
        {
            return GetStatisticEvent!=null ? GetStatisticEvent(this) : null;
        }

        public void DisconnectFromPort()
        {
            Terminal.DisconnectFromPort(this);
        }

        public void ConnectFromPort()
        {
        }

        public PortMode Call(int numberPhone)
        {
            PortMode portMode = PortMode.NoPort;

            if (Contract != null)
            {
                portMode = PickUp();

                if (portMode == PortMode.Tone)
                {
                    portMode = Dialing(numberPhone);
                }
                else
                {
                    if (portMode != PortMode.LongBeeps)
                    {
                        Console.WriteLine("Port disabled.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Enclose the first contract.");
            }

            return portMode;
        }

        public PortMode Answer()
        {
            PortMode portMode = PortMode.NoPort;

            if (Contract != null)
            {
                portMode = PickUp();
            }
            else
            {
                Console.WriteLine("Enclose the first contract.");
            }

            return portMode;
        }

        public void HangUpPhone()
        {
            if (Contract != null)
            {
                Terminal.HangUpPhone(this, new EventArgForTerminalEndCall(PhoneNumber));
            }
            else
            {
                Console.WriteLine("Enclose the first contract.");
            }
        }

        protected PortMode PickUp()
        {
            return Terminal.PickUp(this);
        }

        protected PortMode Dialing(int phoneNumber)
        {
            return Terminal.Dialing(this, phoneNumber);
        }
    }
}