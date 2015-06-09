using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace CheckPoint3ATS
{
    class BillingSystem:IBillingSystem
    {
        List<ICallInfo> callsInfo = new List<ICallInfo>();

        protected void AddCallInfo(ICallInfo callInfo)
        {
            callsInfo.Add(callInfo);
        }

        public ReadOnlyCollection<ICallInfo> CallsInfo
        {
            get { return new ReadOnlyCollection<ICallInfo>(callsInfo); }
        }

    }
}
