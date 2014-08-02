using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioOnline.Api.Rest
{
    public abstract class RestClient
    {
        private const string API_ROOT_URL = "https://{0}.visualstudio.com/DefaultCollection/_apis/{1}/{2}?{3}";
        private const string API_VERSION = "1.0-preview.1";

        protected string _accountName;
        private NetworkCredential _userCredential;

        protected abstract string SubSystemName
        {
            get;
        }

        private string ConstructUrl(string path)
        {
            return ConstructUrl(path, new Dictionary<string, string>());
        }

        private string ConstructUrl(string path, IDictionary<string, string> arguments)
        {
            arguments.Add("api-version", API_VERSION);
            return string.Format(API_ROOT_URL, _accountName, SubSystemName, path, string.Join("&", arguments.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value))));
        }

        public RestClient(string accountName, NetworkCredential userCredential)
        {
            _accountName = accountName;
            _userCredential = userCredential;
        }

        protected async Task<string> GetResponse(string path, IDictionary<string, string> arguments)
        {
            using (HttpClient client = GetHttpClient())
            {
                using (HttpResponseMessage response = client.GetAsync(ConstructUrl(path, arguments)).Result)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception(string.Format("Request failed: {0}", responseBody));
                    }

                    return responseBody;
                }
            }
        }

        protected async Task<string> PostResponse(string path, object content)
        {            
            var httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            using (HttpClient client = GetHttpClient())
            {
                using (HttpResponseMessage response = client.PostAsync(ConstructUrl(path), httpContent).Result)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception(string.Format("Request failed: {0}", responseBody));
                    }

                    return responseBody;
                }
            }
        }

        protected async Task<string> PatchResponse(string path, object content)
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            using (HttpClient client = GetHttpClient())
            {
                using (HttpResponseMessage response = client.PatchAsync(ConstructUrl(path), httpContent).Result)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception(string.Format("Request failed: {0}", responseBody));
                    }

                    return responseBody;
                }
            }
        }

        private HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", _userCredential.UserName, _userCredential.Password))));

            return client;
        }
    }

    public static class HttpClientExtensions
    {
        public async static Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content)
        {
            var method = new HttpMethod("PATCH");

            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };

            return await client.SendAsync(request);
        }
    }
}
