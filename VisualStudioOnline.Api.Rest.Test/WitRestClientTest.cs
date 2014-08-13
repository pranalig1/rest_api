using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using VisualStudioOnline.Api.Rest.Test.Properties;

namespace VisualStudioOnline.Api.Rest.Test
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
            var sharedQueriesFolder = queryHierarchy.Queries[1];

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

            string result = _client.DeleteQuery(queryFolder).Result;
        }

        [TestMethod]
        public void TestCreateAndUpdateWorkItem()
        {
            var workItem = new WorkItem();
            workItem["System.WorkItemType"] = "Bug";
            workItem["System.Title"] = "REST: " + DateTime.Now.ToString();
            workItem["System.State"] = "Active";
            workItem["System.Reason"] = "New";
            workItem["System.AreaPath"] = Settings.Default.ProjectName;
            workItem["System.IterationPath"] = string.Format("{0}\\Iteration 1", Settings.Default.ProjectName);
            workItem["Microsoft.VSTS.Common.ActivatedBy"] = Settings.Default.DefaultUser;

            workItem = _client.CreateWorkItem(workItem).Result;

            WorkItemCollection workItems = _client.GetWorkItems(new int[] { workItem.Id }, WitRestClient.WorkItemExpandOptions.all).Result;

            workItem = _client.GetWorkItem(workItems.WorkItems[0].Id, WitRestClient.WorkItemExpandOptions.all).Result;
            workItem["System.Title"] = "REST: " + DateTime.Now.ToString();
            workItem = _client.UpdateWorkItem(workItem).Result;
        }
    }
}
