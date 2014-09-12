using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using VisualStudioOnline.Api.Rest.Test.Properties;
using VisualStudioOnline.Api.Rest.V2;

namespace VisualStudioOnline.Api.Rest.Test.V2
{
    [TestClass]
    public class WitRestClientTest
    {
        private WitRestClient _client;

        [TestInitialize]
        public void Initialize()
        {
            _client = new WitRestClient(Settings.Default.AccountName,
                new NetworkCredential(Settings.Default.UserName, Settings.Default.Password));
        }

        [TestMethod]
        public void TestGetWorkItemTypes()
        {
            var workItemTypes = _client.GetWorkItemTypes(Settings.Default.ProjectName).Result;

            var workItemType = _client.GetWorkItemType(Settings.Default.ProjectName, workItemTypes.Types[0].Name).Result;
        }
    }
}
