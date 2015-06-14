using System;
using System.Timers;

namespace CheckPoint3ATS
{
    public class Port : IPort
    {
        private readonly int _number;
        private readonly Timer _timer = new Timer(1000000);

        public event EventHandler<EventArgs> FinishTimerEvent;

        public Port()
        {
        }

        public Port(int number)
        {
            _number = number;
        }

        public int Number
        {
            get { return _number; }
        }

        public int PhoneNumber { get; set; }

        public PortStatus Status { get; set; }

        public PortMode Mode { get; set; }

        public void StartTimer()
        {
            _timer.AutoReset = false;
            _timer.Elapsed += ElapsedHandler;
            _timer.Start();
        }

        public void StopTimer()
        {
            _timer.Stop();
        }

        public override bool Equals(object obj)
        {
            return ((IPort) obj).Number == Number;
        }

        public override int GetHashCode()
        {
            return Number;
        }

        private void ElapsedHandler(object obj, ElapsedEventArgs e)
        {
            Mode = PortMode.On;
            Console.WriteLine("Port {0} {1}", Number, Mode);
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