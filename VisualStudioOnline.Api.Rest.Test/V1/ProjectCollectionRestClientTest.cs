using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using VisualStudioOnline.Api.Rest.Test.Properties;
using VisualStudioOnline.Api.Rest.V1.Client;

namespace VisualStudioOnline.Api.Rest.Test.V1
{
    [TestClass]
    public class ProjectCollectionRestClientTest
    {
        private ProjectCollectionRestClient _client;

        [TestInitialize]
        public void Initialize()
        {
            _client = new ProjectCollectionRestClient(Settings.Default.AccountName,
                new NetworkCredential(Settings.Default.UserName, Settings.Default.Password));
        }

        [TestMethod]
        public void TestGetProjectCollections()
        {
            var collections = _client.GetProjectCollections().Result;
            var collection = _client.GetProjectCollection(collections[0].Name).Result;
        }
    }
}
