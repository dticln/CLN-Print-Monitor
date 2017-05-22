using CLNPrintMonitor.Model;
using CLNPrintMonitor.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CLNPrintMonitor.Persistence
{
    class Repository
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
        /// <returns></returns>
        public async Task<List<Printer>> GetDataFromRemote()
        {
            List<Printer> printers = new List<Printer>();
            try
            {
                string result = await Helpers.SendHttpRequest("http://localhost/CaCln/printer/json_report");
                JArray json = (JArray)JsonConvert.DeserializeObject(result);
                foreach (JObject content in json.Children<JObject>())
                {
                    printers.Add(new Printer(content.Property("name").Value.ToString(), IPAddress.Parse(content.Property("ipv4").Value.ToString())));
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return printers;
        }

    }
}
