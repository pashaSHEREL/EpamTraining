using System;

namespace CheckPoint3ATS
{
    public class Time
    {
        private DateTime _days;
        public event EventHandler<EventArgs> ChangeTimeEvent;

        public Time(DateTime days)
        {
            _days = days;
        }

        public DateTime Days
        {
            get { return _days; }
        }

        public void ChangeTime(int days)
        {
            _days = _days.AddDays(days);
            OnChangeTImeEvent();
        }

        protected void OnChangeTImeEvent()
        {
            if (ChangeTimeEvent != null)
            {
                ChangeTimeEvent(this, null);
            }
        }
    }
}