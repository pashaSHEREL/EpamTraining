using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    class CompanyOperator:ICompanyOperator
    {
        private string _name;
        private IATS _ats = new ATS();
        IBillingSystem _billingSystem = new BillingSystem();

        List<IContract> _contracts = new List<IContract>();
        List<ITerminal> _terminals = new List<ITerminal>();

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
            get;
            set;
        }

        public IATS ATS
        {
            get;
            set;
        }

        public void ConcludeContract(ISubscriber subscriber, int phoneNumber)
        {
            if (_ats.FreePorts && _terminals.Count > 0 )
            {
                _contracts.Add(new Contract()
                {
                    NameLSubscriber = subscriber.NameL,
                    NameFSubscriber = subscriber.NameF,
                    AddressSubscriber = subscriber.Address,
                    PhoneNumber = phoneNumber,
                    Connected = false,
                    ContractId = _contracts.Count + 1
                });


                subscriber.PhoneNumber = phoneNumber;
                subscriber.Contract = _contracts.Last();
                subscriber.Terminal = _terminals[0];
                subscriber.GetStatisticEvent += GetStatisticEventHandler;
                _terminals.RemoveAt(0);

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

                _ats.RegistryTerminal(subscriber);
                _billingSystem.AddSubscriber(new SubscriberStatistics(
                    _contracts.Last().ContractId,
                    _contracts.Last().PhoneNumber,
                    _contracts.Last().TP));

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

        protected List<ICallInfo> GetStatisticEventHandler(ISubscriber sender)
        {
            List<ICallInfo> listCallInfo=new List<ICallInfo>();
            foreach (var variable in _billingSystem.SubscribersStatistics)
            {
                if (sender.Contract.ContractId==variable.AccountNumber)
                {
                    listCallInfo = variable.CallsInfo;
                }
            }

            return listCallInfo;
        }

        public void TerminateContract(ISubscriber subscriber)
        {
            for (int i = 0; i < _contracts.Count; i++)
			{
                if (_contracts[i].ContractId == subscriber.Contract.ContractId)
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
                    subscriber.PhoneNumber = 0;
                    subscriber.Contract = null;
                    _terminals.Add(subscriber.Terminal);
                    subscriber.Terminal = null;
                    _contracts.RemoveAt(i);
                    _ats.UnRegistryTerminal(subscriber);
                    _billingSystem.DelSubscriber(_contracts[i].ContractId);
                    break;
                }
			}
           
        }
    }
}
