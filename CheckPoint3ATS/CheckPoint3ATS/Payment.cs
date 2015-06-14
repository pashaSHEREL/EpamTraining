using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    public class Payment:IPayment
    {
        public int AmountOfMoney { get; set; }
        public DateTime PayDay { get; set; }
    }
}
