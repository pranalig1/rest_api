using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace VisualStudioOnline.Api.Rest
{
    public class WitRestClientV2 : RestClient
    {
        protected override string SubSystemName
        {
            get { return "wit"; }
        }

        public WitRestClientV2(string accountName, NetworkCredential userCredential)
            : base(accountName, userCredential, "1.0-preview.2")
        {
        }
    }
}
