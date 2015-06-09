using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace CheckPoint3ATS
{
    class Port:IPort
    {
        private int number;
        private Timer timer = new Timer();

        public event EventHandler<EventArgs> FinishTimerEvent;

        public Port()
        { 
        }

        public Port(int number)
        {
            this.number = number;
        }

        public int Number
        {
            get { return number;}
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

        public void StartTimer()
        {
            timer.Interval = 100000;
            timer.AutoReset = false;
            timer.Elapsed += ElapsedHandler;
            timer.Start();
        }

        public void StopTimer()
        {
            timer.Stop();
        }

        public override bool Equals(object obj)
        {
            return ((IPort)obj).Number == this.Number;
        }

        public override int GetHashCode()
        {
            return this.Number;
        }

        private void ElapsedHandler(object obj, ElapsedEventArgs e)
        {
            this.Mode = PortMode.On;
            Console.WriteLine("Port {0} {1}", this.Number, this.Mode);
            OnFinishTimerEvent(this, null);
        }

        private void OnFinishTimerEvent(object obj, EventArgs e)
        {
            if (FinishTimerEvent != null)
            {
                FinishTimerEvent(obj, e);
            }
        }
    }
}
