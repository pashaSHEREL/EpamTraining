using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    class Subscriber:ISubscriber
    {
        string nameL;
        string nameF;
        string address;

        public Subscriber()
        { 
        }

        public Subscriber(string nameL, string nameF, string address)
        {
            this.nameL = nameL;
            this.nameF = nameF;
            this.address = address;
        }

        public string NameL
        {
            get { return nameL; }
        }

        public string NameF
        {
            get { return nameF; }
        }

        public string Address
        {
            get { return address; }
        }

        public int PhoneNumber
        {
            get;
            set;
        }

        public ITerminal Terminal
        {
            get; 
            set;
        }

        public IContract Contract
        {
            get;
            set; 
        }

        public PortMode Call(int numberPhone)
        {
            PortMode portMode = PortMode.NoPort;

            if (Contract!=null)
            {
                portMode = PickUp();

                if (portMode == PortMode.Tone)
                {
                    portMode = Dialing(numberPhone);
                }
                else
                {
                    if (portMode == PortMode.LongBeeps)
                    {

                    }
                    else
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
                portMode=PickUp();
            }
            else 
            {
                Console.WriteLine("Enclose the first contract.");
            }

            return portMode;
        }

        public void EndCall()
        {
            if (Contract != null)
            {
                Terminal.EndCall(this, new EventArgForEndCall(PhoneNumber));
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
