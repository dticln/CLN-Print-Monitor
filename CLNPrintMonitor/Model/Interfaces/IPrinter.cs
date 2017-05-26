using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CLNPrintMonitor.Model.Interfaces
{
    interface IPrinter
    {
        bool IsOnline();
        Task<bool> GetInformationFromDevice();
        Task<byte[]> GetReportFromDevice(); 
    }
}
