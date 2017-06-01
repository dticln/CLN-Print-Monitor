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
        /// <summary>
        /// A impressora está online?
        /// </summary>
        /// <returns></returns>
        bool IsOnline();

        /// <summary>
        /// Recupera as informações da impressora
        /// </summary>
        /// <returns>Conseguiu</returns>
        Task<bool> GetInformationFromDevice();

        /// <summary>
        /// Recupera um PDF de relatório da impressora
        /// </summary>
        /// <returns></returns>
        Task<byte[]> GetReportFromDevice();


        /// <summary>
        /// Recupra um HTML de relatório da impressora
        /// </summary>
        /// <returns></returns>
        Task<string> GetRawReportFromDevice();

    }
}
