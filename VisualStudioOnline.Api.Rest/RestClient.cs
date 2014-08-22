using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioOnline.Api.Rest
{
    /// <summary>
    /// Base class for TFS subsystem REST API client
    /// </summary>
    public abstract class RestClient
    {
        protected const string ACCOUNT_ROOT_URL = "https://{0}.visualstudio.com/DefaultCollection";

        private string _rootUrl;
        private IHttpRequestHeaderFilter _authProvider;
        private string _apiVersion;

        protected abstract string SubSystemName
        {
            get;
        }

        public RestClient(string rootUrl, IHttpRequestHeaderFilter authProvider, string apiVersion)
        {
            _rootUrl = string.Format("{0}/_apis/{1}", rootUrl, SubSystemName);
            _authProvider = authProvider;
            _apiVersion = apiVersion;
        }

        protected async Task<string> GetResponse(string path)
        {
            return await GetResponse(path, new Dictionary<string, string>());
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
                        throw new Exception(string.Format("GET request failed: {0}", responseBody));
                    }

                    return responseBody;
                }
            }
        }

        protected async Task<string> PostResponse(string path, object content)
        {
            return await PostResponse(path, new Dictionary<string, string>(), content);
        }

        protected async Task<string> PostResponse(string path, IDictionary<string, string> arguments, object content)
        {            
            var httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            using (HttpClient client = GetHttpClient())
            {
                using (HttpResponseMessage response = client.PostAsync(ConstructUrl(path, arguments), httpContent).Result)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception(string.Format("POST request failed: {0}", responseBody));
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
                        throw new Exception(string.Format("PATCH request failed: {0}", responseBody));
                    }

                    return responseBody;
                }
            }
        }

        protected async Task<string> DeleteResponse(string path)
        {
            using (HttpClient client = GetHttpClient())
            {
                using (HttpResponseMessage response = client.DeleteAsync(ConstructUrl(path)).Result)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception(string.Format("DELETE request failed: {0}", responseBody));
                    }

                    return responseBody;
                }
            }
        }

        private HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _authProvider.ProcessHeaders(client.DefaultRequestHeaders);
            return client;
        }

        private string ConstructUrl(string path)
        {
            return ConstructUrl(path, new Dictionary<string, string>());
        }

        private string ConstructUrl(string path, IDictionary<string, string> arguments)
        {
            arguments.Add("api-version", _apiVersion);

            StringBuilder resultUrl = new StringBuilder(_rootUrl);

            if(!string.IsNullOrEmpty(path))
            {
                resultUrl.AppendFormat("/{0}", path);
            }

            resultUrl.AppendFormat("?{0}", string.Join("&", arguments.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value))));
            return resultUrl.ToString();
        }
    }

    /// <summary>
    /// HttpClient extensions
    /// </summary>
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
