using CLNPrintMonitor.Model;
using CLNPrintMonitor.Properties;
using CLNPrintMonitor.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CLNPrintMonitor.Persistence
{
    public enum APICommand : int
    {
        Create = 1,
        Read = 2,
        ReadAll = 3,
        Update = 4,
        Delete = 5,
    }

    public class Repository
    {

        internal static string API_IPV4 = Resources.ApiIpv4;
        internal static string API_NAME = Resources.ApiName;
        internal static string API_ID = Resources.Id;
        internal static string API_COMMAND = Resources.ApiCommand;
        internal static string RESPONSE = Resources.Response;
        internal static string SUCCESS = Resources.Success;
        internal static string DATA = Resources.Data;
        internal static string NAME = Resources.Name;
        internal static string IPV4 = Resources.Ipv4;
        internal static string ID = Resources.Id;
        internal static string SECURE_KEY = Resources.SecureKey;
        internal static string SECURE_KEY_CONTENT = Resources.SecureKeyContent;
        internal static string API_URI = Resources.ApiUri;

        private static Repository instance;
        private ConnectionErrorUIHandler connectionErroHandler = null;
        public ConnectionErrorUIHandler ConnectionErroHandler { get => connectionErroHandler; set => connectionErroHandler = value; }
        public delegate void ConnectionErrorUIHandler();

        private Repository() { }

        /// <summary>
        /// 
        /// </summary>
        public static Repository GetInstance
        {
            get {
                if (Repository.instance == null) {
                    instance = new Repository();
                }
                return instance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="printer"></param>
        /// <returns></returns>
        public async Task<bool> Add(Printer printer)
        {
            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { API_IPV4, printer.Address.ToString() },
                    { API_NAME, printer.Name }
                };
                string response = await this.Api(APICommand.Create, param);
                JObject json = (JObject)JsonConvert.DeserializeObject(response);
                if(json.Property(RESPONSE).Value.ToString() == SUCCESS)
                {
                    return true;
                }
            } catch(Exception ex)
            {
                this.connectionErroHandler?.Invoke();
                Console.WriteLine(ex);
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public async Task<Printer> Select(string ip)
        {
            Printer printer;
            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { API_IPV4, ip }
                };
                string response = await this.Api(APICommand.Read, param);
                JObject json = (JObject)JsonConvert.DeserializeObject(response);
                if (json.Property(RESPONSE).Value.ToString() == SUCCESS)
                {
                    JObject content = (JObject)json.Property(DATA).Value;
                    printer = new Printer(
                        content.Property(NAME).Value.ToString(),
                        IPAddress.Parse(content.Property(IPV4).Value.ToString())
                    );
                    return printer;
                }
            } catch(Exception ex)
            {
                this.connectionErroHandler?.Invoke();
                Console.WriteLine(ex);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public async Task<int> SelectId(string ip)
        {
            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { API_IPV4, ip }
                };
                string response = await this.Api(APICommand.Read, param);
                JObject json = (JObject)JsonConvert.DeserializeObject(response);
                if (json.Property(RESPONSE).Value.ToString() == SUCCESS)
                {
                    JObject content = (JObject)json.Property(DATA).Value;
                    return Int32.Parse(content.Property(ID).Value.ToString());
                }
            }
            catch (Exception ex)
            {
                this.connectionErroHandler?.Invoke();
                Console.WriteLine(ex);
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Printer>> SelectAll()
        {
            List<Printer> printers = new List<Printer>();
            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>();
                string response = await this.Api(APICommand.ReadAll, param);
                JObject json = (JObject)JsonConvert.DeserializeObject(response);
                if (json.Property(RESPONSE).Value.ToString() == SUCCESS)
                {
                    foreach (JObject content in json.Property(DATA).Values())
                    {
                        printers.Add(new Printer(content.Property(NAME).Value.ToString(), IPAddress.Parse(content.Property(IPV4).Value.ToString())));
                    }
                }
            }
            catch (Exception ex)
            {
                this.connectionErroHandler?.Invoke();
                Console.WriteLine(ex);
            }
            return printers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public async Task<bool> Update(int id, Printer printer)
        {
            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { API_IPV4, printer.Address.ToString() },
                    { API_NAME, printer.Name },
                    { API_ID, id.ToString() }
                };
                string response = await this.Api(APICommand.Update, param);
                JObject json = (JObject)JsonConvert.DeserializeObject(response);
                if (json.Property(RESPONSE).Value.ToString() == SUCCESS)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.connectionErroHandler?.Invoke();
                Console.WriteLine(ex);
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="printer"></param>
        /// <returns></returns>
        public async Task<bool> Remove(string ip)
        {
            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { API_IPV4, ip }
                };
                string response = await this.Api(APICommand.Delete, param);
                JObject json = (JObject)JsonConvert.DeserializeObject(response);
                if (json.Property(RESPONSE).Value.ToString() == SUCCESS)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.connectionErroHandler?.Invoke();
                Console.WriteLine(ex);
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<string> Api(APICommand command, Dictionary<string,string> param)
        {
            if(param is null)
            {
                param = new Dictionary<string, string>();
            }
            param.Add(SECURE_KEY, SECURE_KEY_CONTENT);
            string response = null;
            switch (command)
            {
                case APICommand.Create:
                    param.Add(API_COMMAND, Convert.ToInt32(APICommand.Create).ToString());
                    response = await Helpers.SendHttpPostRequest(API_URI, param);
                    break;
                case APICommand.Read:
                    param.Add(API_COMMAND, Convert.ToInt32(APICommand.Read).ToString());
                    response = await Helpers.SendHttpPostRequest(API_URI, param);
                    break;
                case APICommand.ReadAll:
                    param.Add(API_COMMAND, Convert.ToInt32(APICommand.ReadAll).ToString());
                    response = await Helpers.SendHttpPostRequest(API_URI, param);
                    break;
                case APICommand.Update:
                    param.Add(API_COMMAND, Convert.ToInt32(APICommand.Update).ToString());
                    response = await Helpers.SendHttpPostRequest(API_URI, param);
                    break;
                case APICommand.Delete:
                    param.Add(API_COMMAND, Convert.ToInt32(APICommand.Delete).ToString());
                    response = await Helpers.SendHttpPostRequest(API_URI, param);
                    break;
            }
            return response;
        }

    }
}
