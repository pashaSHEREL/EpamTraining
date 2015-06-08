using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace CheckPoint3ATS
{
    class Port:IPort
    {
        Timer timer = new Timer();
        public event EventHandler<EventArgs> FinishTimerEvent;

        public void StartTimer()
        {
            timer.Interval = 10000;
            timer.AutoReset = false;
            timer.Elapsed += ElapsedHandler;
            timer.Start();
        }

        public void StopTimer()
        {
            timer.Stop();
        }

        private void ElapsedHandler(object obj, ElapsedEventArgs e)
        {
            this.Mode = PortMode.On;
            Console.WriteLine("Port {0} {1}", this.Number, this.Mode);
            OnFinishTimerEvent(this,null);
        }

        private void OnFinishTimerEvent(object obj, EventArgs e)
        {
            if (FinishTimerEvent != null)
            {
                FinishTimerEvent(obj, e);
            }   
        }

        public int Number
        {
            get;
            set;
        }

        public int PhoneNumber
        {
            get;
            set;
        }

        public PortStatus Status
        {
            get;
            set;
        }

        public PortMode Mode
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            return ((IPort)obj).Number == this.Number;
        }

        public override int GetHashCode()
        {
            return this.Number;
        }
    }
}
