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
        public void TestGetRelationTypes()
        {
            var relations = _client.GetWorkItemRelationTypes().Result;
            var relation = _client.GetWorkItemRelationType(relations.Relations[0].ReferenceName).Result;
        }

        [TestMethod]
        public void TestGetFields()
        {
            var fields = _client.GetFields().Result;
            var field = _client.GetField(fields.Fields[0].ReferenceName).Result;
        }

        [TestMethod]
        public void TestGetWorkItemTypeCategories()
        {
            var workItemTypeCategories = _client.GetWorkItemTypeCategories(Settings.Default.ProjectName).Result;
            var workItemTypeCategory = _client.GetWorkItemTypeCategory(Settings.Default.ProjectName, workItemTypeCategories.Categories[0].ReferenceName).Result;
        }

        [TestMethod]
        public void TestGetWorkItemTypes()
        {
            var workItemTypes = _client.GetWorkItemTypes(Settings.Default.ProjectName).Result;
            var workItemType = _client.GetWorkItemType(Settings.Default.ProjectName, workItemTypes.Types[0].Name).Result;
        }

        [TestMethod]
        public void TestGetClassificationNodes()
        {
            var nodes = _client.GetClassificationNodes(Settings.Default.ProjectName).Result;

            var rootArea = _client.GetAreaNode(Settings.Default.ProjectName, 5).Result;
            var rootIteration = _client.GetIterationNode(Settings.Default.ProjectName, 5).Result;

            var iteration1 = _client.GetIterationNode(Settings.Default.ProjectName, "Iteration 1").Result;
            var area1 = _client.GetAreaNode(Settings.Default.ProjectName, "Area 1").Result;
        }

        [TestMethod]
        public void TestGetWorkItemHistory()
        {
            var history = _client.GetWorkItemHistory(Settings.Default.WorkItemId).Result;
            var revHistory = _client.GetWorkItemRevisionHistory(Settings.Default.WorkItemId, Settings.Default.WorkItemRevision).Result;
        }
    }
}
