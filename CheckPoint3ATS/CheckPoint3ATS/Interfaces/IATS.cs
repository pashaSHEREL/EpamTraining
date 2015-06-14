using System;
using System.Collections.ObjectModel;

namespace CheckPoint3ATS
{
    public interface IATS
    {
        ReadOnlyCollection<IPort> Ports { get; }
        int NumberOfATS { get; }
        bool FreePorts { get; }

        event EventHandler<EventArgsForATSFinishCall> FinishCallEvent;

        void InstallTime(Time time);
        void RegistryBilling(IBillingSystem billingSystem);
        void RegistryTerminal(ISubscriber subscriber);
        void UnRegistryTerminal(ISubscriber subscriber);
    }
}