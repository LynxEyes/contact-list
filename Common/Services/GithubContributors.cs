using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Common.Services {

    public class GithubContributors {
        private const string API = "https://api.github.com/repos";

        public async Task<ICollection<string>> GetContributors(string owner, string repo) {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("rip curl hail wget");

            Uri requestUri = new Uri($"{API}/{owner}/{repo}/contributors");

            var httpResponse = await httpClient.GetAsync(requestUri);
            var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<IList<JObject>>(httpResponseBody);

            return data.Select((JObject item) => item["login"].ToString()).ToList();
        }
    }
}