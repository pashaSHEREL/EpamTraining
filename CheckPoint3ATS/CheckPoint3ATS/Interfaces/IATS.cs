using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace CheckPoint3ATS
{
    interface IATS
    {
        ReadOnlyCollection<IPort>  Ports { get; }
        int NumberOfATS { get;}
        bool FreePorts { get; }
        void InstallTime(Time time);
        void RegistryBilling(IBillingSystem billingSystem);
        void RegistryTerminal(ISubscriber subscriber);
        void UnRegistryTerminal(ISubscriber subscriber);
        event EventHandler<EventArgsForATSFinishCall> FinishCallEvent;
    }
}
