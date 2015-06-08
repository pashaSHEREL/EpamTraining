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

        protected PortMode PickUp()
        {
            return Terminal.PickUp(this);
        }

        protected PortMode Dialing(int phoneNumber)
        {
            return Terminal.Dialing(this, phoneNumber);
        }
        
        public void Call(int numberPhone)
        {
            PortMode portMode=PickUp();
            if (portMode == PortMode.Tone)
            {
                portMode=Dialing(numberPhone);  
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

        public void Answer()
        {
            PickUp();
        }

        public void EndCall()
        {
            Terminal.EndCall(this,new EventArgForEndCall(PhoneNumber));
        }
    }
}
