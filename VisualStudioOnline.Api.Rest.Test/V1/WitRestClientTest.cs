using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using VisualStudioOnline.Api.Rest.Test.Properties;
using VisualStudioOnline.Api.Rest.V1;
using VisualStudioOnline.Api.Rest.V1.Model;

namespace VisualStudioOnline.Api.Rest.Test.V1
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
        public void TestCreateAndUpdateQuery()
        {
            QueryCollection queryHierarchy = _client.GetQueries(Settings.Default.ProjectName, null, WitRestClient.QueryExpandOptions.all).Result;
            var sharedQueriesFolder = queryHierarchy.Queries[0];

            Query queryFolder = _client.CreateQuery(new Query()
                {
                    Name = "REST_" + DateTime.Now.Ticks.ToString(),
                    ParentId = sharedQueriesFolder.Id,
                    Type = QueryType.folder
                }).Result;

            Query query = _client.CreateQuery(new Query()
                {
                    Name = "REST_" + DateTime.Now.Ticks.ToString(),
                    ParentId = queryFolder.Id,
                    QueryText = "select System.Id from workitems"
                }).Result;

            query.Name = "REST_" + DateTime.Now.Ticks.ToString();
            query = _client.UpdateQuery(query).Result;

            var queryResults = _client.GetQueryResult(query).Result;
            queryResults = _client.GetQueryResult("select System.Id from workitems").Result;

            string result = _client.DeleteQuery(queryFolder).Result;
        }

        [TestMethod]
        public void TestCreateAndUpdateWorkItem()
        {
            var bug = CreateBug();
            var task = CreateTask();

            WorkItemCollection workItems = _client.GetWorkItems(new int[] { bug.Id }, WitRestClient.WorkItemExpandOptions.all).Result;

            bug = _client.GetWorkItem(workItems.WorkItems[0].Id, WitRestClient.WorkItemExpandOptions.all).Result;

            bug["System.Title"] = "REST: " + DateTime.Now.ToString();
            bug.Links.Add(new Link() { Comment = DateTime.Now.ToString(), Source = bug, Target = task, LinkType = "System.LinkTypes.Dependency-Forward" });
            bug = _client.UpdateWorkItem(bug).Result;

            bug.Links[0].Comment = DateTime.Now.ToString();
            bug.Links[0].UpdateType = LinkUpdateType.update;
            bug = _client.UpdateWorkItem(bug).Result;

            bug.Links[0].UpdateType = LinkUpdateType.delete;
            bug = _client.UpdateWorkItem(bug).Result;

            var bugInitialRevision = _client.GetWorkItemRevision(bug.Id, 1).Result;
            var allBugUpdates = _client.GetWorkItemUpdates(bug.Id).Result;
            var bugInitialUpdate = _client.GetWorkItemUpdate(bug.Id, 1).Result;
        }

        [TestMethod]
        public void TestUploadDownloadAttachments()
        {
            var bug = CreateBug();
            var resourceLink = _client.UploadAttachment(Settings.Default.ProjectName, bug["System.AreaPath"], "Test.txt", "Hello world").Result;
            
            resourceLink.Name = "TestFile";
            resourceLink.Comment = DateTime.Now.ToString();
            bug.ResourceLinks.Add(resourceLink);
            bug = _client.UpdateWorkItem(bug).Result;

            bug = _client.GetWorkItem(bug.Id, WitRestClient.WorkItemExpandOptions.all).Result;

            string content = _client.DownloadAttachment(bug.ResourceLinks[0].Location).Result;
        }

        private WorkItem CreateBug()
        {
            var bug = new WorkItem();
            bug["System.WorkItemType"] = "Bug";
            bug["System.Title"] = "REST: " + DateTime.Now.ToString();
            bug["System.State"] = "Active";
            bug["System.Reason"] = "New";
            bug["System.AreaPath"] = Settings.Default.ProjectName;
            bug["System.IterationPath"] = string.Format("{0}\\Iteration 1", Settings.Default.ProjectName);
            bug["Microsoft.VSTS.Common.ActivatedBy"] = Settings.Default.DefaultUser;

            return  _client.CreateWorkItem(bug).Result;
        }

        private WorkItem CreateTask()
        {
            var task = new WorkItem();
            task["System.WorkItemType"] = "Task";
            task["System.Title"] = "REST: " + DateTime.Now.ToString();
            task["System.State"] = "New";
            task["System.Reason"] = "New";
            task["System.AreaPath"] = Settings.Default.ProjectName;
            task["System.IterationPath"] = string.Format("{0}\\Iteration 1", Settings.Default.ProjectName);

            return _client.CreateWorkItem(task).Result;
        }
    }
}
