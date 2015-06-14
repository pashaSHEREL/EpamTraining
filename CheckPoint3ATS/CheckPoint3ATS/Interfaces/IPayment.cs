using System;

namespace CheckPoint3ATS
{
    public interface IPayment
    {
        int AmountOfMoney { get; set; }
        DateTime PayDay { get; set; }
    }
}