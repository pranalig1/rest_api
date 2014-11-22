using System;
using System.Collections.Generic;
using System.Net;

namespace VisualStudioOnline.Api.Rest.V1.Client
{
    /// <summary>
    /// Main entry point for using VSO REST APIs
    /// </summary>
    public class VsoClient
    {
        private const string ACCOUNT_ROOT_URL = "https://{0}.visualstudio.com/{1}";
        private const string DEFAULT_COLLECTION = "DefaultCollection";

        private string _rootUrl;
        private NetworkCredential _userCredential;

        private static Dictionary<Type, Type> _serviceMapping = new Dictionary<Type, Type>()
        {
            { typeof(IVsoBuild), typeof(BuildRestClient) },
            { typeof(IVsoProjectCollection), typeof(ProjectCollectionRestClient) },
            { typeof(IVsoProject), typeof(ProjectRestClient) },
            { typeof(IVsoTag), typeof(TagRestClient) },
            { typeof(IVsoVersionControl), typeof(VersionControlRestClient) },
            { typeof(IVsoWit), typeof(WitRestClient) }
        };

        public VsoClient(string accountName, NetworkCredential userCredential, string collectionName = DEFAULT_COLLECTION)
            : this(string.Format(ACCOUNT_ROOT_URL, accountName, collectionName), userCredential)
        {
        }

        public VsoClient(string url, NetworkCredential userCredential)
        {
            _rootUrl = url;
            _userCredential = userCredential;
        }

        public T GetService<T>()
        {
            if(!_serviceMapping.ContainsKey(typeof(T)))
            {
                throw new VsoException("Unknown service requested.");
            }

            return (T)Activator.CreateInstance(_serviceMapping[typeof(T)], _rootUrl, _userCredential);
        }
    }
}
