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
    /// <summary>
    /// Lista de comandos da API
    /// </summary>
    public enum APICommand : int
    {
        Create = 1,
        Read = 2,
        ReadAll = 3,
        Update = 4,
        Delete = 5,
    }

    /// <summary>
    /// Representa uma conexão com a API de persistencia de dados
    /// </summary>
    public class Repository
    {

        #region Constantes

        internal static string API_IPV4 = Resources.ApiIpv4;
        internal static string API_NAME = Resources.ApiName;
        internal static string API_ID = Resources.ApiId;
        internal static string API_COMMAND = Resources.ApiCommand;
        internal static string RESPONSE = Resources.Response;
        internal static string SUCCESS = Resources.ResponseSuccess;
        internal static string ERROR = Resources.ResponseError;
        internal static string DATA = Resources.Data;
        internal static string NAME = Resources.Name;
        internal static string IPV4 = Resources.Ipv4;
        internal static string ID = Resources.Id;
        internal static string SECURE_KEY = Resources.SecureKey;
        internal static string SECURE_KEY_CONTENT = Security.Decrypt(Resources.SecureKeyContent);
        internal static string API_URI = Resources.ApiUri;

        #endregion

        private static Repository instance;

        private UIHandler connectionErrorHandler = null;
        private UIHandler defaultErrorHandler = null;
        private UIHandler alreadyExistsHandler = null;

        public UIHandler ConnectionErrorHandler { get => connectionErrorHandler; set => connectionErrorHandler = value; }
        public UIHandler DefaultErrorHandler { get => defaultErrorHandler; set => defaultErrorHandler = value; }
        public UIHandler AlreadyExistsHandler { get => alreadyExistsHandler; set => alreadyExistsHandler = value; }


        public delegate void UIHandler();

        private Repository() {}

        /// <summary>
        /// Recupera o repositório
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
        /// Adiciona a impressora no repositório utilizando o objeto da impressora
        /// </summary>
        /// <param name="printer">Impressora</param>
        /// <param name="handlers">Manipulador de erro (Atributo opcional para tratamento de erro)</param>
        /// <returns>Resposta</returns>
        public async Task<bool> Add(Printer printer, bool handlers = true)
        {
            try
            {
                ///
                /// Verifica se já existe uma impressora cadastrada
                ///
                if(await this.Select(printer.Address.ToString(), false) == null)
                {
                    Dictionary<string, string> param = new Dictionary<string, string>
                    {
                        { API_IPV4, printer.Address.ToString() },
                        { API_NAME, printer.Name }
                    };
                    string response = await this.Api(APICommand.Create, param);
                    JObject json = (JObject)JsonConvert.DeserializeObject(response);
                    if (json.Property(RESPONSE).Value.ToString() == SUCCESS)
                    {
                        return true;
                    }
                    else if (json.Property(RESPONSE).Value.ToString() == ERROR && handlers)
                    {
                        this.defaultErrorHandler?.Invoke();
                    }
                } else if (handlers)
                {
                     this.alreadyExistsHandler?.Invoke();
                }
            } catch(Exception)
            {
                if(handlers)
                {
                    this.connectionErrorHandler?.Invoke();
                }
            }
            return false;
        }

        /// <summary>
        /// Realiza a consulta no repositório utilizando o IPV4 como referência
        /// </summary>
        /// <param name="ip">IPV4 da impressora</param>
        /// <param name="handlers">Manipulador de erro (Atributo opcional para tratamento de erro)</param>
        /// <returns>Impressora</returns>
        public async Task<Printer> Select(string ip, bool handlers = true)
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
                else if (json.Property(RESPONSE).Value.ToString() == ERROR && handlers)
                {
                    this.defaultErrorHandler?.Invoke();
                }
            } catch(Exception)
            {
                if (handlers)
                {
                    this.connectionErrorHandler?.Invoke();
                }
            }
            return null;
        }

        /// <summary>
        /// Realiza a consulta no repositório utilizando de ID utilizando o IPV4 como referência
        /// </summary>
        /// <param name="ip">IP procurado</param>
        /// <param name="handlers">Manipulador de erro (Atributo opcional para tratamento de erro)</param>
        /// <returns>ID da impressora</returns>
        public async Task<int> SelectId(string ip, bool handlers = true)
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
                else if (json.Property(RESPONSE).Value.ToString() == ERROR && handlers)
                {
                    this.defaultErrorHandler?.Invoke();
                }
            }
            catch (Exception)
            {
                if (handlers)
                {
                    this.connectionErrorHandler?.Invoke();
                }
            }
            return 0;
        }

        /// <summary>
        /// Realiza a consulta de todas as impressoras cadastradas na base de dados
        /// </summary>
        /// <param name="handlers">Manipulador de erro (Atributo opcional para tratamento de erro)</param>
        /// <returns>Lista de impressoras</returns>
        public async Task<List<Printer>> SelectAll(bool handlers = true)
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
                else if (json.Property(RESPONSE).Value.ToString() == ERROR && handlers)
                {
                    this.defaultErrorHandler?.Invoke();
                }
            }
            catch (Exception)
            {
                if (handlers)
                {
                    this.connectionErrorHandler?.Invoke();
                }
            }
            return printers;
        }

        /// <summary>
        /// Realiza a atualização dos dados de uma impressora utilizando como referência o ID 
        /// </summary>
        /// <param name="id">Id da impressora</param
        /// <param name="printer">Novos dados da impressora</param>
        /// <param name="handlers">Manipulador de erro (Atributo opcional para tratamento de erro)</param>
        /// <returns>Resposta</returns>
        public async Task<bool> Update(int id, Printer printer, bool handlers = true)
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
                else if (json.Property(RESPONSE).Value.ToString() == ERROR && handlers)
                {
                    this.defaultErrorHandler?.Invoke();
                }
            }
            catch (Exception)
            {
                if (handlers)
                {
                    this.connectionErrorHandler?.Invoke();
                }
            }
            return true;
        }

        /// <summary>
        /// Realiza a exclusão da impressora utilizando como referência o IPV4
        /// </summary>
        /// <param name="ip">IPV4 da impressora</param>
        /// <returns>Se foi excluído</returns>
        public async Task<bool> Remove(string ip, bool handlers = true)
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
                else if (json.Property(RESPONSE).Value.ToString() == ERROR && handlers)
                {
                    this.defaultErrorHandler?.Invoke();
                }
            }
            catch (Exception)
            {
                if (handlers)
                {
                    this.connectionErrorHandler?.Invoke();
                }
            }
            return true;
        }

        /// <summary>
        /// Método que realiza a requisição para cada tipo de pedido
        /// Responsável por empacotar os dados de forma que a API responda a requisição de forma correta
        /// </summary>
        /// <param name="command">Comando desejado</param>
        /// <param name="param">Chave => valor que serão utilizando como parametros POST</param>
        /// <returns>Resposta JSON da requisição</returns>
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
