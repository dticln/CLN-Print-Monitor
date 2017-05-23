using CLNPrintMonitor.Model;
using CLNPrintMonitor.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        private Repository() { }

        private static Repository instance;

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
                Dictionary<string,string> param = new Dictionary<string, string>();
                param.Add("printer_ipv4", printer.Address.ToString());
                param.Add("printer_name", printer.Name);
                string response = await this.Api(APICommand.Create, param);
                JObject json = (JObject)JsonConvert.DeserializeObject(response);
                if(json.Property("response").Value.ToString() == "success")
                {
                    return true;
                }
            } catch(Exception ex)
            {
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
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("printer_ipv4", ip);
                string response = await this.Api(APICommand.Read, param);
                JObject json = (JObject)JsonConvert.DeserializeObject(response);
                if (json.Property("response").Value.ToString() == "success")
                {
                    JObject content = (JObject)json.Property("data").Value;
                    printer = new Printer(
                        content.Property("name").Value.ToString(),
                        IPAddress.Parse(content.Property("ipv4").Value.ToString())
                    );
                    return printer;
                }
            } catch(Exception ex)
            {
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
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("printer_ipv4", ip);
                string response = await this.Api(APICommand.Read, param);
                JObject json = (JObject)JsonConvert.DeserializeObject(response);
                if (json.Property("response").Value.ToString() == "success")
                {
                    JObject content = (JObject)json.Property("data").Value;
                    return Int32.Parse(content.Property("id").Value.ToString());
                }
            }
            catch (Exception ex)
            {
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
                if (json.Property("response").Value.ToString() == "success")
                {
                    foreach (JObject content in json.Property("data").Values())
                    {
                        printers.Add(new Printer(content.Property("name").Value.ToString(), IPAddress.Parse(content.Property("ipv4").Value.ToString())));
                    }
                }
            }
            catch (Exception ex)
            {
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
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("printer_ipv4", printer.Address.ToString());
                param.Add("printer_name", printer.Name);
                param.Add("printer_id", id.ToString());
                string response = await this.Api(APICommand.Update, param);
                JObject json = (JObject)JsonConvert.DeserializeObject(response);
                if (json.Property("response").Value.ToString() == "success")
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
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
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("printer_ipv4", ip);
                string response = await this.Api(APICommand.Create, param);
                JObject json = (JObject)JsonConvert.DeserializeObject(response);
                if (json.Property("response").Value.ToString() == "success")
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
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
            param.Add("secure_key", "c21aeb51b992aa742705791e976ca16ab38fd2431c98bb38b7ea97a609548ffb");
            string response = null;
            switch (command)
            {
                case APICommand.Create:
                    param.Add("api_command", Convert.ToInt32(APICommand.Create).ToString());
                    response = await Helpers.SendHttpPostRequest("http://localhost/CaCln/printer/api", param);
                    break;
                case APICommand.Read:
                    param.Add("api_command", Convert.ToInt32(APICommand.Read).ToString());
                    response = await Helpers.SendHttpPostRequest("http://localhost/CaCln/printer/api", param);
                    break;
                case APICommand.ReadAll:
                    param.Add("api_command", Convert.ToInt32(APICommand.ReadAll).ToString());
                    response = await Helpers.SendHttpPostRequest("http://localhost/CaCln/printer/api", param);
                    break;
                case APICommand.Update:
                    param.Add("api_command", Convert.ToInt32(APICommand.Update).ToString());
                    response = await Helpers.SendHttpPostRequest("http://localhost/CaCln/printer/api", param);
                    break;
                case APICommand.Delete:
                    param.Add("api_command", Convert.ToInt32(APICommand.Delete).ToString());
                    response = await Helpers.SendHttpPostRequest("http://localhost/CaCln/printer/api", param);
                    break;
            }
            return response;
        }

    }
}
