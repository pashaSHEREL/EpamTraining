﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CheckPoint3ATS
{
    interface ICallInfo
    {
        TimeSpan StartCall { get; set; }
        DateTime DayCall { get; set; }
    }
}
