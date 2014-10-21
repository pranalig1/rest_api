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
        protected const string ACCOUNT_ROOT_URL = "https://{0}.visualstudio.com/{1}";
        protected const string DEFAULT_COLLECTION = "DefaultCollection";
        protected const string JSON_MEDIA_TYPE = "application/json";
        protected const string JSON_PATCH_MEDIA_TYPE = "application/json-patch+json";

        private string _rootUrl;
        private IHttpRequestHeaderFilter _authProvider;
        private string _apiVersion;

        protected abstract string SubSystemName
        {
            get;
        }

        public RestClient(string rootUrl, IHttpRequestHeaderFilter authProvider, string apiVersion)
        {
            _rootUrl = rootUrl;
            _authProvider = authProvider;
            _apiVersion = apiVersion;
        }

        protected async Task<string> GetResponse(string path, string projectName = null)
        {
            return await GetResponse(path, new Dictionary<string, object>(), projectName);
        }

        protected async Task<string> GetResponse(string path, IDictionary<string, object> arguments, string projectName = null)
        {
            using (HttpClient client = GetHttpClient())
            {
                using (HttpResponseMessage response = client.GetAsync(ConstructUrl(projectName, path, arguments)).Result)
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

        protected async Task<string> PostResponse(string path, object content, string projectName = null)
        {
            return await PostResponse(path, new Dictionary<string, object>(), content, projectName);
        }

        protected async Task<string> PostResponse(string path, IDictionary<string, object> arguments, object content, string projectName = null, string mediaType = JSON_MEDIA_TYPE)
        {            
            var httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, mediaType);

            using (HttpClient client = GetHttpClient())
            {
                using (HttpResponseMessage response = client.PostAsync(ConstructUrl(projectName, path, arguments), httpContent).Result)
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

        protected async Task<string> PatchResponse(string path, object content, string projectName = null, string mediaType = JSON_PATCH_MEDIA_TYPE)
        {
            return await PatchResponse(path, new Dictionary<string, object>(), content, projectName, mediaType);
        }

        protected async Task<string> PatchResponse(string path, IDictionary<string, object> arguments, object content, string projectName = null, string mediaType = JSON_PATCH_MEDIA_TYPE)
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, mediaType);

            using (HttpClient client = GetHttpClient())
            {
                using (HttpResponseMessage response = client.PatchAsync(ConstructUrl(projectName, path, arguments), httpContent).Result)
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

        protected async Task<string> DeleteResponse(string path, string projectName = null)
        {
            using (HttpClient client = GetHttpClient())
            {
                using (HttpResponseMessage response = client.DeleteAsync(ConstructUrl(projectName, path)).Result)
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

        private HttpClient GetHttpClient(string mediaType = JSON_MEDIA_TYPE)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            _authProvider.ProcessHeaders(client.DefaultRequestHeaders);
            return client;
        }

        private string ConstructUrl(string projectName, string path)
        {
            return ConstructUrl(projectName, path, new Dictionary<string, object>());
        }

        private string ConstructUrl(string projectName, string path, IDictionary<string, object> arguments)
        {
            if (!arguments.ContainsKey("api-version"))
            {
                arguments.Add("api-version", _apiVersion);
            }

            StringBuilder resultUrl = new StringBuilder(
                string.IsNullOrEmpty(projectName) ? 
                string.Format("{0}/_apis/{1}", _rootUrl, SubSystemName) :
                string.Format("{0}/{1}/_apis/{2}", _rootUrl, projectName, SubSystemName));

            if(!string.IsNullOrEmpty(path))
            {
                resultUrl.AppendFormat("/{0}", path);
            }

            resultUrl.AppendFormat("?{0}", string.Join("&", arguments.Where(kvp => kvp.Value != null).Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value))));
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
