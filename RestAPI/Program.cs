using System;
using System.Linq;
using System.Net;
using VisualStudioOnline.Api.Rest;

namespace RestApiTest
{  
    class Program
    {
        const string ACCOUNT_NAME = "testaccount";

        static void Main(string[] args)
        {
            try
            {
                NetworkCredential userCredentials = new NetworkCredential("username", "password");
                WitRestClient client = new WitRestClient(ACCOUNT_NAME, userCredentials);

                //TestReadWorkItems(client, 1);
                //ReadAndUpdateWorkItem(client, 4);
                //TestGetQueries(client, "Agile32");
                //TestGetQueryAndUpdate(client, "dd442eb1-b79f-4766-a846-20e280aaf5b8");
                var query = TestCreateQuery(client, "5fee6ef8-24ca-4fe5-aace-24789c6ff56a");
                var r = client.DeleteQuery(query).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static Query TestCreateQuery(WitRestClient client, string parentId)
        {
            var query = new Query();
            query.Name = "REST_" + DateTime.Now.Ticks.ToString();
            query.ParentId = parentId;
            query.QueryText = "select System.Id from workitems";
            query = client.CreateQuery(query).Result;

            var queryFolder = new Query();
            queryFolder.Name = "REST_" + DateTime.Now.Ticks.ToString();
            queryFolder.ParentId = parentId;
            queryFolder.Type = QueryType.folder;
            queryFolder = client.CreateQuery(queryFolder).Result;

            return queryFolder;
        }

        private static void TestGetQueries(WitRestClient client, string projectName)
        {
            var queries = client.GetQueries(projectName, null, WitRestClient.QueryExpandOptions.all).Result;
        }

        private static void TestGetQueryAndUpdate(WitRestClient client, string queryId)
        {
            var query = client.GetQuery(queryId).Result;
            query.Name = "REST_" + DateTime.Now.Ticks.ToString();
            query = client.UpdateQuery(query).Result;
        }

        private static void TestCreateWorkItem(WitRestClient client)
        {
            var newWi = new WorkItem();
            newWi.SetFieldValue("System.WorkItemType", "Bug");
            newWi.SetFieldValue("System.Title", "REST: " + DateTime.Now.ToString());
            newWi.SetFieldValue("System.State", "Active");
            newWi.SetFieldValue("System.Reason", "New");
            newWi.SetFieldValue("System.AreaPath", "Agile32");
            newWi.SetFieldValue("System.IterationPath", "Agile32\\Iteration 1");
            newWi.SetFieldValue("Microsoft.VSTS.Common.ActivatedBy", "UserName");
            newWi = client.CreateWorkItem(newWi).Result;
        }

        private static void TestReadWorkItems(WitRestClient client, params int[] workItemIds)
        {
            var wis = client.GetWorkItems(workItemIds, WitRestClient.WorkItemExpandOptions.all).Result;
        }

        private static void TestReadAndUpdateWorkItem(WitRestClient client, int workItemId)
        {
            var wi = client.GetWorkItem(workItemId, WitRestClient.WorkItemExpandOptions.all).Result;
            wi.SetFieldValue("System.Title", "REST: " + DateTime.Now.ToString());
            wi = client.UpdateWorkItem(wi).Result;
        }
    }
}
