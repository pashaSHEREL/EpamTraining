using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    class CompanyOperator:ICompanyOperator
    {
        private string name;
        private IATS ats = new ATS();
        IBillingSystem billingSystem = new BillingSystem();

        List<IContract> contracts = new List<IContract>();
        List<ITerminal> terminals = new List<ITerminal>();

        public CompanyOperator()
        { 
        }

        public CompanyOperator(IATS ats, string name)
        {
            this.ats = ats;
            this.name = name;
            this.billingSystem.RegistrationATS(this.ats);
        }

        public void AddTerminal(ITerminal terminal)
        {
            terminals.Add(terminal);
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
            if (ats.FreePorts && terminals.Count > 0 )
            {
                contracts.Add(new Contract()
                {
                    NameLSubscriber = subscriber.NameL,
                    NameFSubscriber = subscriber.NameF,
                    AddressSubscriber = subscriber.Address,
                    PhoneNumber = phoneNumber,
                    Connected = false,
                    ContractId = contracts.Count + 1
                });

                subscriber.PhoneNumber = phoneNumber;
                subscriber.Contract = contracts.Last();
                subscriber.Terminal = terminals[0];
                terminals.RemoveAt(0);

                foreach (var item in ats.Ports)
                {
                    if (item.Status == PortStatus.Free)
                    {
                        item.PhoneNumber = phoneNumber;
                        item.Status = PortStatus.Busy;
                        item.Mode = PortMode.On;
                        break;
                    }
                }

                ats.RegistryTerminal(subscriber);
                billingSystem.AddSubscriber(new SubscriberStatistics(contracts.Last().ContractId,
                    contracts.Last().PhoneNumber,
                    contracts.Last().TP));

            }
            else 
            {
                if (terminals.Count == 0)
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
            for (int i = 0; i < contracts.Count; i++)
			{
                if (contracts[i].ContractId == subscriber.Contract.ContractId)
                {
                    foreach (var item in ats.Ports)
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
                    terminals.Add(subscriber.Terminal);
                    subscriber.Terminal = null;
                    contracts.RemoveAt(i);
                    ats.UnRegistryTerminal(subscriber);
                    billingSystem.DelSubscriber(contracts[i].ContractId);
                    break;
                }
			}
           
        }
    }
}
