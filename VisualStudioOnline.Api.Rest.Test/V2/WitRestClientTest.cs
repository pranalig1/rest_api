using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using VisualStudioOnline.Api.Rest.Test.Properties;
using VisualStudioOnline.Api.Rest.V2;
using VisualStudioOnline.Api.Rest.V2.Model;

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
            var relation = _client.GetWorkItemRelationType(relations.Items[0].ReferenceName).Result;
        }

        [TestMethod]
        public void TestGetFields()
        {
            var fields = _client.GetFields().Result;
            var field = _client.GetField(fields.Items[0].ReferenceName).Result;
        }

        [TestMethod]
        public void TestGetWorkItemTypeCategories()
        {
            var workItemTypeCategories = _client.GetWorkItemTypeCategories(Settings.Default.ProjectName).Result;
            var workItemTypeCategory = _client.GetWorkItemTypeCategory(Settings.Default.ProjectName, workItemTypeCategories.Items[0].ReferenceName).Result;
        }

        [TestMethod]
        public void TestGetWorkItemTypes()
        {
            var workItemTypes = _client.GetWorkItemTypes(Settings.Default.ProjectName).Result;
            var workItemType = _client.GetWorkItemType(Settings.Default.ProjectName, workItemTypes.Items[0].Name).Result;
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

        [TestMethod]
        public void TestGetWorkItemRevisions()
        {
            var revisions = _client.GetWorkItemRevisions(Settings.Default.WorkItemId, null, null, WitRestClient.RevisionExpandOptions.all).Result;
            var revision = _client.GetWorkItemRevision(Settings.Default.WorkItemId, Settings.Default.WorkItemRevision).Result;

            var areaPath = revision.Fields["System.AreaPath"];
        }

        [TestMethod]
        public void TestGetWorkItemUpdates()
        {
            var updates = _client.GetWorkItemUpdates(Settings.Default.WorkItemId).Result;
            var update = _client.GetWorkItemUpdate(Settings.Default.WorkItemId, Settings.Default.WorkItemRevision).Result;
        }

        [TestMethod]
        public void TestUploadDownloadAttachments()
        {
            var fileRef = _client.UploadAttachment("Test.txt", "Hello world").Result;
            string content = _client.DownloadAttachment(fileRef.Id).Result;

            //TODO: add attachment to WI

            //TODO: remove attachment from WI
        }

        [TestMethod]
        public void TestCreateAndUpdateWorkItem()
        {
            var defaultValues = _client.GetWorkItemTypeDefaultValues(Settings.Default.ProjectName, "Bug").Result;
            var workItems = _client.GetWorkItems(new int[] { Settings.Default.WorkItemId }, WitRestClient.RevisionExpandOptions.all).Result;

            var bug = new WorkItem();
            bug.Fields["System.Title"] = "Test bug 2";
            bug.Fields["System.History"] = DateTime.Now.ToString();
            bug = _client.CreateWorkItem(Settings.Default.ProjectName, "Bug", bug).Result;

            bug = _client.GetWorkItem(bug.Id, WitRestClient.RevisionExpandOptions.all).Result;
            bug.Fields["System.Title"] = bug.Fields["System.Title"] + " (updated)";
            bug.Fields["System.Tags"] = "SimpleTag";
            bug.Relations.Add(new WorkItemRelation()
                {
                    Url = workItems[0].Url,
                    Rel = "System.LinkTypes.Dependency-Forward",
                    Attributes = new RelationAttributes() { Comment = "Hello world" }
                });

            bug = _client.UpdateWorkItem(bug).Result;

            //TODO: update link

            //TODO: remove link

            //TODO: add hyperlink
        }
    }
}
