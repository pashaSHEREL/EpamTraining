using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint3ATS
{
    interface ITerminal
    {
        string Model { get; set; }
        int Cost { get; set; }
        void StartCall();
        void FinishCall();
        void AnswerCall();
        event EventHandler<EventArgs> Start;
        event EventHandler<EventArgs> Finish;
        event EventHandler<EventArgs> Answer;
    }
}
