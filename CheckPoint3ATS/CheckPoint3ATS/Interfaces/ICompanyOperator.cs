namespace CheckPoint3ATS
{
    public interface ICompanyOperator
    {
        string Name { get; }
        void AddTerminal(ITerminal terminal);
        void ConcludeContract(ISubscriber subscriber, int phoneNumber, IStandartTariffPlan tariffPlan);
        void TerminateContract(ISubscriber subscriber);
    }
}