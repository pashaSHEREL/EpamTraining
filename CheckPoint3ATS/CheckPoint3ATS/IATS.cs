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
        int NumberATS { get; set; }
        int NumberingCapacity { get; set; }
        void StartCallHandler();
        void FinishCallHandler();
        void AnswerCallHandler();
    }
}
