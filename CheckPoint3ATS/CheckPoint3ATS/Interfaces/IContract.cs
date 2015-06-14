namespace CheckPoint3ATS
{
    public interface IContract
    {
        int ContractId { get; set; }
        string NameLSubscriber { get; set; }
        string NameFSubscriber { get; set; }
        string AddressSubscriber { get; set; }
        IStandartTariffPlan TariffPlan { get; set; }
        int PhoneNumber { get; set; }
        int TerminalId { get; set; }
        bool Connected { get; set; }
    }
}