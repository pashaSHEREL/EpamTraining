﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace WindowsServiceForCheckPoint4
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new WindowsServiceForCheckPoint4()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}