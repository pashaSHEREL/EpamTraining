namespace CheckPoint3ATS
{
    public interface ITariffPlan
    {
        string Name { get; set; }
        int LicenseFee { get; set; }
        int PricePerMinute { get; set; }
        int FreeMinute { get; set; }
    }
}