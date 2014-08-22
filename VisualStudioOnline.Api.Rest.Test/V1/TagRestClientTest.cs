using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using VisualStudioOnline.Api.Rest.Test.Properties;
using VisualStudioOnline.Api.Rest.V1;

namespace VisualStudioOnline.Api.Rest.Test.V1
{
    [TestClass]
    public class TagRestClientTest
    {
        private TagRestClient _client;

        [TestInitialize]
        public void Initialize()
        {
            _client = new TagRestClient(Settings.Default.AccountName,
                new NetworkCredential(Settings.Default.UserName, Settings.Default.Password));
        }

        [TestMethod]
        public void TestGetTagList()
        {
            var tags = _client.GetTagList(Settings.Default.ProjectId).Result;
        }

        [TestMethod]
        public void TestCreateAndUpdateTag()
        {
            var newTag = _client.CreateTag(Settings.Default.ProjectId, "TestTag").Result;
            
            newTag.Name = "TestTagRenamed";
            newTag = _client.UpdateTag(Settings.Default.ProjectId, newTag).Result;

            newTag = _client.GetTag(Settings.Default.ProjectId, "TestTagRenamed").Result;

            var response = _client.DeleteTag(Settings.Default.ProjectId, newTag).Result;
        }
    }
}
