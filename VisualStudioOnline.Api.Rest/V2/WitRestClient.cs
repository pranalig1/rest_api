using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace VisualStudioOnline.Api.Rest.V2
{
    public class WitRestClient : RestClient
    {
        protected override string SubSystemName
        {
            get { return "wit"; }
        }

        public WitRestClient(string accountName, NetworkCredential userCredential)
            : base(accountName, userCredential, "1.0-preview.2")
        {
        }
    }
}
