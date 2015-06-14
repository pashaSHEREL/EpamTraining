using System;
using System.Collections.Generic;
using System.Linq;


namespace CheckPoint3ATS
{
    public class Subscriber : ISubscriber
    {
        private readonly string _nameL;
        private readonly string _nameF;
        private readonly string _address;

        public event Func<ISubscriber, List<ICallInfo>> GetStatisticEvent;
        public event EventHandler<EventArgsForSubscriberPay> PayEvent; 

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

        public void Pay(int amountOfMoney)
        {
            OnPayEvent(new EventArgsForSubscriberPay(amountOfMoney, Contract.ContractId));
        }

        public void ViewStatisticFilterByDate(DateTime startDate, DateTime endDate)
        {
            var stat = Sort(startDate, endDate, x => x.DayCall.ToOADate());
            ViewStatistic(stat);
        }

        public void ViewStatisticFilterByCostCall(DateTime startDate, DateTime endDate)
        {
            var stat = Sort(startDate, endDate, x => ((CallInfoForBilling) x).CostCall);
            ViewStatistic(stat);
        }

        public void ViewStatisticFilterBySubscriber(DateTime startDate, DateTime endDate)
        {
            var stat = Sort(startDate, endDate, x => ((CallInfoForBilling) x).PhoneNumber);
            ViewStatistic(stat);
        }

        public void DisconnectFromPort()
        {
            Terminal.DisconnectFromPort(this);
        }

        public void ConnectFromPort()
        {
            Terminal.ConnectFromPort(this);
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

        protected void OnPayEvent(EventArgsForSubscriberPay args)
        {
            if (PayEvent!=null)
            {
                PayEvent(this, args);
            }
        }

        protected List<ICallInfo> Sort(DateTime startDate, DateTime endDate, Func<ICallInfo, double> order)
        {
            List<ICallInfo> stat = GetStatistic(startDate, endDate).OrderBy(order).Select(x => x).ToList();
            return stat;
        }

        protected static void ViewStatistic(List<ICallInfo> stat)
        {
            foreach (var item in stat)
            {
                CallInfoForBilling callInfo = item as CallInfoForBilling;

                if (callInfo != null)
                {
                    Console.WriteLine(callInfo.StartCall);
                    Console.WriteLine(
                        "Date {0}, Start call {1}, Duration call {2}, Cost call {3:c}, Outgoing call {4}, Subscriber number phone {5}",
                        callInfo.DayCall.ToString("d"), callInfo.StartCall, callInfo.DurationCall, callInfo.CostCall,
                        callInfo.OutgoingCall, callInfo.PhoneNumber);
                    Console.WriteLine();
                }
            }
        }

        protected List<ICallInfo> GetStatistic(DateTime firstDay, DateTime lastDay)
        {
            return OnGetStatisticEvent()
                .Where(
                    x => (((CallInfoForBilling) x).DayCall > firstDay) && (((CallInfoForBilling) x).DayCall < lastDay))
                .Select(x => x)
                .ToList();
        }

        protected List<ICallInfo> OnGetStatisticEvent()
        {
            return GetStatisticEvent != null ? GetStatisticEvent(this) : null;
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