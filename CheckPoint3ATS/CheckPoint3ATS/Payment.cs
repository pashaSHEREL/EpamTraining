using System;

namespace CheckPoint3ATS
{
    public class Payment : IPayment
    {
        public int AmountOfMoney { get; set; }
        public DateTime PayDay { get; set; }
    }
}