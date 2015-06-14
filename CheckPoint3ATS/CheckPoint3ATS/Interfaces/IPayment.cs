using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    public interface IPayment
    {
        int AmountOfMoney { get; set; }
        DateTime PayDay { get; set; }
    }
}
