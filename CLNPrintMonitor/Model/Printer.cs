using CLNPrintMonitor.Controller;
using CLNPrintMonitor.Model.Interfaces;
using CLNPrintMonitor.Properties;
using CLNPrintMonitor.Util;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace CLNPrintMonitor.Model
{

    /// <summary>
    /// Struct que representa uma entrada de papel da impressora
    /// </summary>
    public struct PaperInput 
    {
        public PaperInput(string name, string status, int capacity, string scale, string type)
        {
            this.name = name;
            this.status = status;
            this.capacity = capacity;
            this.scale = scale;
            this.type = type;
        }

        private string name;
        private string status;
        private int capacity;
        private string scale;
        private string type;

        public string Name { get => name; }
        public string Status { get => status; }
        public int Capacity { get => capacity; }
        public string Scale { get => scale; }
        public string Type { get => type; }
    };

    /// <summary>
    /// Struct que representa uma bandeja de saída de papel da impressora
    /// </summary>
    public struct PaperOutput 
    {
        public PaperOutput(string name, string status, int capacity)
        {
            this.name = name;
            this.status = status;
            this.capacity = capacity;
        }

        private string name;
        private string status;
        private int capacity;

        public string Name { get => name; }
        public string Status { get => status; }
        public int Capacity { get => capacity; }
    };

    /// <summary>
    /// Enum que representa um status da impressora
    /// </summary>
    public enum StatusIcon {
        Ink0 = 0,
        Ink30 = 1,
        Ink60 = 2,
        Ink90 = 3,
        Ink100 = 4,
        Offline = 5,
        Error = 6
    };

    /// <summary>
    /// Representa uma impressora
    /// </summary>
    public class Printer : IPrinter, ICloneable
    {
        #region Constants

        internal static string OK = Resources.Ok;
        internal static string HTTP = Resources.Http;
        internal static string TOPBAR_URI = Resources.TopbarUri;
        internal static string STATUS_URI = Resources.StatusUri;
        internal static string REPORT_URI = Resources.ReportUri;
        internal static string NODE_QUERY = Resources.NodeQuery;
        internal static string NODE_STATUS_QUERY = Resources.NodeStatusQuery;
        internal static string LOW_TONER_WARNING = Resources.NodeQuery;
        internal static string UNKNOWN_WARNING = Resources.UnknownWarning;
        internal static string NODE_TD = Resources.NodeTd;

        #endregion

        #region Attributes

        private IPAddress address;
        private string name;
        private string feedback;
        private string model;
        private string deviceType;
        private string speed;
        private string tonerCapacity;
        private int ink;
        private int maintenance;
        private int fc;
        private PaperInput defaultInput;
        private PaperInput supplyMF;
        private PaperOutput defaultOutput;
        private StatusIcon status;
        private UpdateUIHandler updateUIInformation;
        private PrinterController controllerUIRelation;
        public IPAddress Address { get => address; }
        public string Name { get => name; set => name = value; }
        public string Feedback { get => feedback; }
        public string Model { get => model; }
        public string DeviceType { get => deviceType; }
        public string Speed { get => speed; }
        public string TonerCapacity { get => tonerCapacity; }
        public int Ink { get => ink; }
        public int Maintenance { get => maintenance; }
        public int Fc { get => fc; }
        public PaperInput DefaultInput { get => defaultInput; }
        public PaperInput SupplyMF { get => supplyMF; }
        public PaperOutput DefaultOutput { get => defaultOutput; }
        public StatusIcon Status { get => status; }
        public UpdateUIHandler UpdateUIInformation { get => updateUIInformation; set => updateUIInformation = value; }
        public PrinterController ControllerUIRelation { get => controllerUIRelation; set => controllerUIRelation = value; }
        public delegate void UpdateUIHandler(PrinterController relation);

        #endregion

        /// <summary>
        /// Construtor da impressora
        /// </summary>
        /// <param name="name">Nome da impressora</param>
        /// <param name="inet">IPV4 da impressora</param>
        public Printer(string name, IPAddress inet)
        {
            this.name = name;
            this.address = inet;
            this.status = StatusIcon.Offline;
        }

        /// <summary>
        /// Manda um pacote PING para a impressora
        /// Recuperando se ela está online ou não
        /// </summary>
        /// <returns>Resposta quanto a disponibilidade do host</returns>
        public bool IsOnline()
        {
            PingReply response = new Ping().Send(this.address);
            return (response.Status == IPStatus.Success);
        }

        /// <summary>
        /// Realiza uma requisição assincrona para uma página Lexmark,
        /// recuperando as informações e inflando os dados de status no objeto
        /// </summary>
        /// <returns>Se a execução foi completa</returns>
        public async Task<bool> GetInformationFromDevice()
        {
            List<string> list = new List<string>();
            string bufferInfo;
            string bufferStatus;

            try
            {
                bufferInfo = await Helpers.SendHttpRequest(Printer.HTTP + this.address + Printer.TOPBAR_URI);
                bufferStatus = await Helpers.SendHttpRequest(Printer.HTTP + this.address + Printer.STATUS_URI);
            }
            catch (Exception)
            {
                return false;
            }

            HtmlDocument htmlInfo = Helpers.CreateDocument(bufferInfo);
            HtmlDocument htmlStatus = Helpers.CreateDocument(bufferStatus);

            this.SearchForPrinterInformation(list, htmlInfo);
            this.SearchForPrinterStatus(list, htmlStatus);
            
            return this.SetInformations(list);
        }

        /// <summary>
        /// Recupera um arquivo PDF com os dados do relatório da impressora
        /// por meio de uma requisição assincrona
        /// </summary>
        /// <returns>Arquivo PDF</returns>
        public async Task<byte[]> GetReportFromDevice()
        {
            string response;
            try
            {
                response = await Helpers.SendHttpRequest(Printer.HTTP + this.address + Printer.REPORT_URI, true);
            }
            catch (Exception)
            {
                return null;
            }
            HtmlDocument html = Helpers.CreateDocument(response);
            return Helpers.CreatePDF(html);
        }

        /// <summary>
        /// Recupera um arquivo HTML com os dados do relatório da impressora 
        /// por meio de uma requisição assincrona
        /// </summary>
        /// <returns>HTML em formato string</returns>
        public async Task<string> GetRawReportFromDevice()
        {
            string response;
            try
            {
                response = await Helpers.SendHttpRequest(Printer.HTTP + this.address + Printer.REPORT_URI, true);
            }
            catch (Exception)
            {
                return String.Empty;
            }
            return response;
        }
        
        /// <summary>
        /// Remove avisos da lista dos atributos
        /// </summary>
        /// <param name="attributes">Atributos extraídos da página web</param>
        private void RemoveWarning(List<string> attributes)
        {
            attributes.RemoveAll(item => item == Printer.LOW_TONER_WARNING || item == Printer.UNKNOWN_WARNING);
        }
        
        /// <summary>
        /// Procura pelas informações ESPECÍFICAS da impressora dentro do arquivo HTML
        /// </summary>
        /// <param name="attributes">Lista de atributos</param>
        /// <param name="searchIn">HTML onde os dados serão extraídos</param>
        private void SearchForPrinterInformation(List<string> attributes, HtmlDocument searchIn)
        {
            foreach (HtmlNode span in searchIn.DocumentNode.SelectNodes(Printer.NODE_QUERY))
            {
                attributes.Add(span.InnerText);
            }
            foreach (HtmlNode span in searchIn.DocumentNode.SelectNodes(Printer.NODE_STATUS_QUERY))
            {
                attributes.Add(span.InnerText);
            }
        }

        /// <summary>
        /// Procura por dados de status da impressora dentro do arquivo HTML
        /// </summary>
        /// <param name="attributes">Lista de atributos</param>
        /// <param name="searchIn">HTML onde os dados serão extraídos</param>
        private void SearchForPrinterStatus(List<string> attributes, HtmlDocument searchIn)
        {
            foreach (HtmlNode td in searchIn.DocumentNode.SelectNodes(NODE_TD))
            {
                string inner = Helpers.Normalize(td.InnerText);
                if (inner != string.Empty)
                {
                    attributes.Add(inner);
                }
            }
        }

        /// <summary>
        /// A partir do arquivo de atributos
        /// infla os dados no objeto
        /// </summary>
        /// <param name="attributes">Atributos extraídos</param>
        /// <returns>Respostas</returns>
        private bool SetInformations(List<string> attributes)
        {
            this.RemoveWarning(attributes);
            try
            {
                this.model = attributes[0];
                this.feedback = attributes[1].Replace("&#032;", " ");
                this.deviceType = attributes[30];
                this.speed = attributes[32];
                this.tonerCapacity = attributes[34];
                this.ink = Helpers.GetInteger(attributes[4]);
                this.maintenance = Helpers.GetInteger(attributes[36]);
                this.fc = Helpers.GetInteger(attributes[38]);
                this.defaultInput = new PaperInput(
                    attributes[10], 
                    attributes[12], 
                    Int32.Parse(attributes[13]), 
                    attributes[14], 
                    attributes[15]);
                this.supplyMF = new PaperInput(
                    attributes[16], 
                    attributes[17], 
                    Int32.Parse(attributes[19]), 
                    attributes[20], 
                    attributes[21]);
                this.defaultOutput = new PaperOutput(
                    attributes[25], 
                    attributes[26], 
                    Int32.Parse(attributes[28]));
                this.SetStatusIcon();
                if(UpdateUIInformation != null)
                {
                    this.UpdateUIInformation(this.controllerUIRelation);
                }
                return true;
            } catch (Exception) { 
                this.SetStatusIcon();
                return false;
            }
        }

        /// <summary>
        /// Define o icone de status de acordo com determinadas conbinações de atributos
        /// </summary>
        private void SetStatusIcon()
        {
            if (this.model == null)
            {
                this.status = StatusIcon.Error;
            } else if (!IsOnline())
            {
                this.status = StatusIcon.Offline;
            } else if (this.defaultInput.Status != Printer.OK ||
              this.defaultOutput.Status != Printer.OK ||
              this.supplyMF.Status != Printer.OK ||
              this.ink < 30)
            {
                this.status = StatusIcon.Ink0;
            }
            else if (this.ink >= 30 && this.ink < 60)
            {
                this.status = StatusIcon.Ink30;
            }
            else if (this.ink >= 60 && this.ink < 90)
            {
                this.status = StatusIcon.Ink60;
            }
            else if (this.ink >= 90 && this.ink < 100)
            {
                this.status = StatusIcon.Ink90;
            }
            else if (this.ink == 100)
            {
                this.status = StatusIcon.Ink100;
            }
        }

        /// <summary>
        /// Realiza a clonagem do objeto 
        /// </summary>
        /// <returns>Novo objeto identico</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
