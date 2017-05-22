using CLNPrintMonitor.Controller;
using CLNPrintMonitor.Util;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace CLNPrintMonitor.Model
{
    /// <summary>
    /// Struct for printer basic paper input
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
    /// Struct for printer basic paper output
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
    /// Enum for Status managment and UI integration
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
    /// Represents a printer
    /// </summary>
    public class Printer 
    {
        internal static string OK = "OK";
        internal static string HTTP = "http://";
        internal static string TOPBAR_URI = "/cgi-bin/dynamic/topbar.html";
        internal static string STATUS_URI = "/cgi-bin/dynamic/printer/PrinterStatus.html";
        internal static string NODE_QUERY = "//span[contains(@class,'top_prodname')]";
        internal static string LOW_TONER_WARNING = "Baixo";
        internal static string UNKNOWN_WARNING = "Desconhecido";

        private IPAddress address;
        private string name;
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
        public string Name { get => name; }
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

        /// <summary>
        /// Basic constructor for a printer 
        /// </summary>
        /// <param name="name">Name of printer</param>
        /// <param name="inet">IPV4 address object</param>
        public Printer(string name, IPAddress inet)
        {
            this.name = name;
            this.address = inet;
            this.status = StatusIcon.Offline;
        }

        /// <summary>
        /// Returns if printers is online using PingReply
        /// </summary>
        /// <returns>An anwser about the availablity of the device</returns>
        public bool IsOnline()
        {
            PingReply response = new Ping().Send(this.address);
            return (response.Status == IPStatus.Success);
        }

        /// <summary>
        /// Performs an asynchronous request for standard Lexmark pages, 
        /// retrieves printer information and 
        /// bind the data in Printer object
        /// </summary>
        /// <returns>Execution response</returns>
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

            HtmlDocument htmlInfo = this.CreateDocument(bufferInfo);
            HtmlDocument htmlStatus = this.CreateDocument(bufferStatus);

            this.SearchForPrinterInformation(list, htmlInfo);
            this.SearchForPrinterStatus(list, htmlStatus);
            
            return this.SetInformations(list);
        }


        /// <summary>
        /// Remove warnings strings from attributes list
        /// </summary>
        /// <param name="attributes">Extrated attributes from page</param>
        private void RemoveWarning(List<string> attributes)
        {
            attributes.RemoveAll(item => item == Printer.LOW_TONER_WARNING);
            attributes.RemoveAll(item => item == Printer.UNKNOWN_WARNING);
        }

        /// <summary>
        /// Create a HtmlDocument from string
        /// </summary>
        /// <param name="text">Data from request</param>
        /// <returns>A HtmlDocument from text</returns>
        private HtmlDocument CreateDocument(string text)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(text);
            return document;
        }

        /// <summary>
        /// Search for printer information inside HtmlDocument
        /// </summary>
        /// <param name="attributes">Extrated attributes from page</param>
        /// <param name="searchIn">Where it will be searched</param>
        private void SearchForPrinterInformation(List<string> attributes, HtmlDocument searchIn)
        {
            foreach (HtmlNode span in searchIn.DocumentNode.SelectNodes(Printer.NODE_QUERY))
            {
                attributes.Add(span.InnerText);
            }
        }

        /// <summary>
        /// Search for printer status inside HtmlDocument
        /// </summary>
        /// <param name="attributes">Extrated attributes from page</param>
        /// <param name="searchIn">Where it will be searched</param>
        private void SearchForPrinterStatus(List<string> attributes, HtmlDocument searchIn)
        {
            foreach (HtmlNode td in searchIn.DocumentNode.SelectNodes("//td"))
            {
                string inner = Helpers.Normalize(td.InnerText);
                if (inner != string.Empty)
                {
                    attributes.Add(inner);
                }
            }
        }

        /// <summary>
        /// Bind informations in this Printer object 
        /// </summary>
        /// <param name="attributes">Extrated attributes</param>
        /// <returns>Response</returns>
        private bool SetInformations(List<string> attributes)
        {
            this.RemoveWarning(attributes);
            try
            {
                this.model = attributes[0];
                this.deviceType = attributes[28];
                this.speed = attributes[30];
                this.tonerCapacity = attributes[32];
                this.ink = Helpers.GetInteger(attributes[2]);
                this.maintenance = Helpers.GetInteger(attributes[34]);
                this.fc = Helpers.GetInteger(attributes[36]);
                this.defaultInput = new PaperInput(
                    attributes[8], 
                    attributes[9], 
                    Int32.Parse(attributes[11]), 
                    attributes[12], 
                    attributes[13]);
                this.supplyMF = new PaperInput(
                    attributes[14], 
                    attributes[15], 
                    Int32.Parse(attributes[17]), 
                    attributes[18], 
                    attributes[19]);
                this.defaultOutput = new PaperOutput(
                    attributes[23], 
                    attributes[24], 
                    Int32.Parse(attributes[26]));
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
        /// Set the status icon using the printer attributes
        /// </summary>
        private void SetStatusIcon()
        {
            if (this.model == null)
            {
                this.status = StatusIcon.Error;
            } else if (!IsOnline())
            {
                this.status = StatusIcon.Offline;
            }
            else if (this.defaultInput.Status != Printer.OK ||
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
    }
}
