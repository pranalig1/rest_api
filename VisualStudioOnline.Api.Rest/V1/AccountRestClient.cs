using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using VisualStudioOnline.Api.Rest.V1.Model;

namespace VisualStudioOnline.Api.Rest.V1
{
    /// <summary>
    /// Account REST API client
    /// </summary>
    public class AccountRestClient : RestClient
    {
        private const string VSSPS_ROOT_URL = "https://app.vssps.visualstudio.com";
        
        protected override string SubSystemName
        {
            get 
            {
                return "accounts";
            }
        }

         public AccountRestClient(string authToken)
            : base(VSSPS_ROOT_URL, new OAuthFilter(authToken), "1.0-preview.1")
        {
        }

        /// <summary>
        /// Get account list for current user
        /// </summary>
        /// <returns></returns>
        public async Task<AccountCollection> GetAccountList()
        {
            string response = await GetResponse(string.Empty);
            return JsonConvert.DeserializeObject<AccountCollection>(response);            
        }

        /// <summary>
        /// Get specific account
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Account> GetAccount(string name)
        {
            string response = await GetResponse(string.Format("/{0}", name));
            return JsonConvert.DeserializeObject<Account>(response);
        }
    }
}
