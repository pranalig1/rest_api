using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
    }
}
