using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using VisualStudioOnline.Api.Rest.Test.Properties;
using VisualStudioOnline.Api.Rest.V1.Client;

namespace VisualStudioOnline.Api.Rest.Test.V1
{
    [TestClass]
    public class BuildRestClientTests
    {
        private BuildRestClient _client;

        [TestInitialize]
        public void Initialize()
        {
            _client = new BuildRestClient(Settings.Default.AccountName,
                new NetworkCredential(Settings.Default.UserName, Settings.Default.Password));
        }

        [TestMethod]
        public void TestGetBuildDefinitions()
        {
            var definitions = _client.GetBuildDefinitions(Settings.Default.ProjectName).Result;
            var definition = _client.GetBuildDefinition(Settings.Default.ProjectName, definitions.Items[0].Id);
        }

        [TestMethod]
        public void TestBuildQualities()
        {
            var qualities = _client.GetBuildQualities(Settings.Default.ProjectName).Result;

            var newQuality = DateTime.Now.Ticks.ToString();
            var result = _client.AddBuildQuality(Settings.Default.ProjectName, newQuality).Result;
            qualities = _client.GetBuildQualities(Settings.Default.ProjectName).Result;

            result = _client.DeleteBuildQuality(Settings.Default.ProjectName, newQuality).Result;
            qualities = _client.GetBuildQualities(Settings.Default.ProjectName).Result;
        }
    }
}
