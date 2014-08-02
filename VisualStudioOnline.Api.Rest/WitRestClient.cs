using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace VisualStudioOnline.Api.Rest
{
    public class WitRestClient : RestClient
    {
        protected override string SubSystemName
        {
            get { return "wit"; }
        }

        public WitRestClient(string accountName, NetworkCredential userCredential) : base(accountName, userCredential)
        {
        }

        public async Task<WorkItem> CreateWorkItem(WorkItem workItem)
        {
            string response = await PostResponse("workitems", workItem);
            return JsonConvert.DeserializeObject<WorkItem>(response);
        }

        public async Task<WorkItem> GetWorkItem(int workItemId, bool includeLinks = false)
        {
            var arguments = new Dictionary<string, string>();
            if (includeLinks) { arguments.Add("$expand", "all"); }

            string response = await GetResponse(string.Format("workitems/{0}", workItemId), arguments);
            return JsonConvert.DeserializeObject<WorkItem>(response);
        }

        public async Task<WorkItemCollection> GetWorkItems(int[] workItemIds, bool includeLinks = false, DateTime? asOfDate = null, string[] fields = null)
        {
            var arguments = new Dictionary<string, string>() { {"ids", string.Join(",", workItemIds) } };
            if (includeLinks) { arguments.Add("$expand", "all"); }
            if (asOfDate.HasValue) { arguments.Add("asof", asOfDate.Value.ToUniversalTime().ToString("u")); }
            if (fields != null) { arguments.Add("fields", string.Join(",", fields)); }

            string response = await GetResponse("workitems", arguments);
            return JsonConvert.DeserializeObject<WorkItemCollection>(response);
        }

        public async Task<WorkItem> UpdateWorkItem(WorkItem workItem)
        {
            string response = await PatchResponse(string.Format("workitems/{0}", workItem.Id), workItem);
            return JsonConvert.DeserializeObject<WorkItem>(response);
        }
    }
}
