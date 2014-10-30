using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using VisualStudioOnline.Api.Rest.Test.Properties;
using VisualStudioOnline.Api.Rest.V1;

namespace VisualStudioOnline.Api.Rest.Test.V1
{
    [TestClass]
    public class VersionControlRestClientTest
    {
        private VersionControlRestClient _client;

        [TestInitialize]
        public void Initialize()
        {
            _client = new VersionControlRestClient(Settings.Default.AccountName,
                new NetworkCredential(Settings.Default.UserName, Settings.Default.Password));
        }

        [TestMethod]
        public void TestGetBranches()
        {
            var rootBranches = _client.GetRootBranches().Result;
            var branch = _client.GetBranch(rootBranches[0].Path).Result;
        }

        [TestMethod]
        public void TestGetLabels()
        {
            var labels = _client.GetLabels().Result;
            var label = _client.GetLabel(labels[0].Id.ToString()).Result;
            var items = _client.GetLabelledItems(label.Id.ToString()).Result;
        }

        [TestMethod]
        public void TestGetShelvesets()
        {
            var shelvesets = _client.GetShelvesets().Result;
            var shelveset = _client.GetShelveset(shelvesets[0].Id, true, true).Result;

            var changes = _client.GetShelvesetChanges(shelveset.Id).Result;
            var workItems = _client.GetShelvesetWorkItems(shelveset.Id).Result;
        }

        [TestMethod]
        public void TestGetChangesets()
        {
            var changesets = _client.GetChangesets().Result;
            var changesetBatch = _client.GetChangesets(new int[] { changesets[0].Id, changesets[1].Id }).Result;

            var changeset = _client.GetChangeset(changesets[0].Id).Result;
            var change = _client.GetChangesetChanges(changeset.Id).Result;
            var workitems = _client.GetChangesetWorkItems(changeset.Id).Result;
        }
    }
}
