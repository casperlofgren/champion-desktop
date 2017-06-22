using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace champion_desktop
{
    class ChampionAPI
    {
        // API settings
        private string apikey;
        private string apiver = "v2";

        // WebClient
        private static readonly HttpClient client = new HttpClient();

        // Constructor
        public ChampionAPI(string key)
        {
            apikey = key;
        }

        // Request methods
        private static async Task<string> GETRequest(string method, string options = "")
        {
            var response = await client.GetAsync("http://api.champion.gg/" + apiver + "/" + method + "?api_key=" + apikey + "&" + options);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return "fail";
            }
        }

        private static async Task<string> POSTRequest(string method)
        {
            return "";
        }

        // API calls
        public string getInfo()
        {
            return Task.Run(() => { return GETRequest("general"); }).Result;
        }
    }
}
