using System;

namespace CheckPoint3ATS
{
    public interface ICallInfo
    {
        TimeSpan StartCall { get; set; }
        DateTime DayCall { get; set; }
    }
}