using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    public class CompanyOperator : ICompanyOperator
    {
        private readonly IATS _ats = new ATS();
        private readonly IBillingSystem _billingSystem = new BillingSystem();
        private readonly List<IContract> _contracts = new List<IContract>();
        private readonly string _name;
        private readonly List<ITerminal> _terminals = new List<ITerminal>();

        public CompanyOperator()
        {
        }

        public CompanyOperator(IATS ats, string name, Time time)
        {
            _ats = ats;
            _name = name;
            _billingSystem.RegistrationATS(_ats);
            _billingSystem.Date = time.Days;
            _billingSystem.InstallTime(time);
            _ats.RegistryBilling(_billingSystem);
            _ats.InstallTime(time);
        }

        public void AddTerminal(ITerminal terminal)
        {
            _terminals.Add(terminal);
        }

        public string Name
        {
            get { return _name; }
        }

        public void ConcludeContract(ISubscriber subscriber, int phoneNumber, IStandartTariffPlan tariffPlan)
        {
            if (_contracts.Any(x => x.PhoneNumber == phoneNumber))
            {
                throw new Exception("This number is already in use.");
            }

            PhoneVerification(phoneNumber);

            if (_ats.FreePorts && _terminals.Count > 0)
            {
                _contracts.Add(new Contract()
                {
                    NameLSubscriber = subscriber.NameL,
                    NameFSubscriber = subscriber.NameF,
                    AddressSubscriber = subscriber.Address,
                    PhoneNumber = phoneNumber,
                    Connected = false,
                    ContractId = _contracts.Count + 1,
                    TariffPlan = tariffPlan
                });

                RegistrySubscriber(subscriber, phoneNumber);

                SwitchOnAtsPort(phoneNumber);

                _ats.RegistryTerminal(subscriber);

                _billingSystem.AddSubscriber(new SubscriberStatistics(
                    _contracts.Last().ContractId,
                    _contracts.Last().PhoneNumber,
                    _contracts.Last().TariffPlan
                    ));
            }
            else
            {
                if (_terminals.Count == 0)
                {
                    throw new Exception("Terminals ended");
                }
                else
                {
                    throw new Exception("No free ports");
                }
            }
        }

        public void TerminateContract(ISubscriber subscriber)
        {
            for (int i = 0; i < _contracts.Count; i++)
            {
                if (_contracts[i].ContractId == subscriber.Contract.ContractId)
                {
                    SwitchOfAtsPort(subscriber);

                    UnRegistrySubscriber(subscriber, i);

                    _ats.UnRegistryTerminal(subscriber);
                    _billingSystem.DelSubscriber(_contracts[i].ContractId);
                    break;
                }
            }
        }

        protected void UnRegistrySubscriber(ISubscriber subscriber, int i)
        {
            subscriber.ChangeTariffPlanEvent -= ChangeTariffPlanEventHandler;
            subscriber.GetStatisticEvent -= GetStatisticEventHandler;
            subscriber.PayEvent -= PayEventHandler;
            subscriber.PhoneNumber = 0;
            subscriber.Contract = null;
            _terminals.Add(subscriber.Terminal);
            subscriber.Terminal = null;
            _contracts.RemoveAt(i);
        }

        protected void SwitchOfAtsPort(ISubscriber subscriber)
        {
            foreach (var item in _ats.Ports)
            {
                if (item.PhoneNumber == subscriber.PhoneNumber)
                {
                    item.PhoneNumber = 0;
                    item.Status = PortStatus.Free;
                    item.Mode = PortMode.Off;
                    break;
                }
            }
        }

        protected void SwitchOnAtsPort(int phoneNumber)
        {
            foreach (var item in _ats.Ports)
            {
                if (item.Status == PortStatus.Free)
                {
                    item.PhoneNumber = phoneNumber;
                    item.Status = PortStatus.Busy;
                    item.Mode = PortMode.On;
                    break;
                }
            }
        }

        protected void RegistrySubscriber(ISubscriber subscriber, int phoneNumber)
        {
            subscriber.PhoneNumber = phoneNumber;
            subscriber.Contract = _contracts.Last();
            subscriber.Terminal = _terminals[0];
            subscriber.GetStatisticEvent += GetStatisticEventHandler;
            subscriber.PayEvent += PayEventHandler;
            subscriber.ChangeTariffPlanEvent += ChangeTariffPlanEventHandler;
            _terminals.RemoveAt(0);
        }

        protected void ChangeTariffPlanEventHandler(object obj, EventArgsForSubscriberChangeTariffPlan e)
        {
            foreach (var item in _billingSystem.SubscribersStatistics)
            {
                if (e.GetContract.ContractId == item.AccountNumber)
                {
                    if ((_billingSystem.Date - item.ChangeTariffPlanDay).Days > 30)
                    {
                        item.ChangeTariffPlanDay = _billingSystem.Date;
                        item.TariffPlan = e.TariffPlan;

                        foreach (var variable in _contracts)
                        {
                            if (variable.ContractId == item.AccountNumber)
                            {
                                variable.TariffPlan = e.TariffPlan;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("The tariff plan can not change more than 1 time per month");
                    }
                }
            }
        }

        protected void PayEventHandler(object obj, EventArgsForSubscriberPay args)
        {
            foreach (var variable in _billingSystem.SubscribersStatistics)
            {
                if (args.ContractId == variable.AccountNumber)
                {
                    variable.AddPayment(new Payment() {AmountOfMoney = args.AmountOfPay, PayDay = _billingSystem.Date});
                    variable.Balance += args.AmountOfPay;
                    break;
                }
            }
        }

        protected void PhoneVerification(int phoneNumber)
        {
            string str = _ats.NumberOfATS.ToString();
            string str1 = phoneNumber.ToString();

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != str1[i])
                {
                    throw new Exception("phone number should start with the station number");
                }
            }
        }

        protected List<ICallInfo> GetStatisticEventHandler(ISubscriber sender)
        {
            List<ICallInfo> listCallInfo = new List<ICallInfo>();

            foreach (var variable in _billingSystem.SubscribersStatistics)
            {
                if (sender.Contract.ContractId == variable.AccountNumber)
                {
                    listCallInfo = variable.CallsInfo.ToList();
                }
            }

            return listCallInfo;
        }
    }
}