using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    class Contract:IContract
    {
        public int ContractId
        {
            get;
            set;
        }

        public string NameLSubscriber
        {
            get;
            set;
        }

        public string NameFSubscriber
        {
            get;
            set;
        }

        public string AddressSubscriber
        {
            get;
            set;
        }

        public ITariffPlan TP
        {
            get;
            set;
        }

        public int PhoneNumber
        {
            get;
            set;
        }

        public int TerminalId
        {
            get;
            set;
        }

        public bool Connected
        {
            get;
            set;
        }
    }
}
