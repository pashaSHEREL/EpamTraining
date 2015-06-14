namespace CheckPoint3ATS
{
    public class Contract : IContract
    {
        public int ContractId { get; set; }
        public string NameLSubscriber { get; set; }
        public string NameFSubscriber { get; set; }
        public string AddressSubscriber { get; set; }
        public IStandartTariffPlan TariffPlan { get; set; }
        public int PhoneNumber { get; set; }
        public int TerminalId { get; set; }
        public bool Connected { get; set; }
    }
}