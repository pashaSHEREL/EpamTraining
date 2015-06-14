namespace CheckPoint3ATS
{
    public class TariffPlan : ITariffPlan
    {
        public string Name { get; set; }

        public int LicenseFee { get; set; }

        public int PricePerMinute { get; set; }

        public int FreeMinute { get; set; }
    }
}