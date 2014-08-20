using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using VisualStudioOnline.Api.Rest.Test.Properties;

namespace VisualStudioOnline.Api.Rest.Test
{
    [TestClass]
    public class WitRestClientV2Test
    {
        private WitRestClientV2 _client;

        [TestInitialize]
        public void Initialize()
        {
            _client = new WitRestClientV2(Settings.Default.AccountName,
                new NetworkCredential(Settings.Default.UserName, Settings.Default.Password));
        }
    }
}
