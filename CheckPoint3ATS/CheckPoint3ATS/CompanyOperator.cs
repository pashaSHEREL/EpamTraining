using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    class CompanyOperator:ICompanyOperator
    {
        string name;
        IATS ats = new ATS();

        List<IContract> contracts = new List<IContract>();
        List<ITerminal> terminals = new List<ITerminal>();

        public CompanyOperator()
        { 
        }

        public CompanyOperator(IATS ats, string name)
        {
            this.ats = ats;
            this.name = name;
        }

        public List<ITerminal> Terminals
        {
            get { return terminals; }
            set { terminals = value; }
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

        public void ConcludeContract(Subscriber subscriber, int phoneNumber)
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

        public void TerminateContract()
        {
            throw new NotImplementedException();
        }

        public List<IContract> Contracts
        {
            get { return contracts; }
            set { contracts = value; }
        }

        public IBillingSystem BillingSystem
        {
            get;
            set;
        }
    }
}
