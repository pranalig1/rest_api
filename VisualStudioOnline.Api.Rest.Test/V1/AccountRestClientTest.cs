using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using VisualStudioOnline.Api.Rest.Test.Properties;
using VisualStudioOnline.Api.Rest.V1;

namespace VisualStudioOnline.Api.Rest.Test.V1
{
    [TestClass]
    public class AccountRestClientTest
    {
        private AccountRestClient _client;

        [TestInitialize]
        public void Initialize()
        {
            _client = new AccountRestClient(Settings.Default.AccessToken);
        }

        [TestMethod]
        public void TestGetAccountList()
        {
            var accounts = _client.GetAccountList().Result;
        }

        [TestMethod]
        public void TestGetAccount()
        {
            var account = _client.GetAccount(Settings.Default.AccountName).Result;
        }
    }
}
