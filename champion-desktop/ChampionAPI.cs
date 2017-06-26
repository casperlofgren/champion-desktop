/*
 * Champion.gg API wrapper
 * Initial author: Casper Löfgren
 */

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
        private async Task<string> GETRequest(string method)
        {
            var response = await client.GetAsync("http://api.champion.gg/" + apiver + "/" + method + "&api_key=" + apikey);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return "fail";
            }
        }

        private async Task<string> GETDepractedRequest(string method)
        {
            var response = await client.GetAsync("http://api.champion.gg/" + method + "?api_key=" + apikey);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return "fail";
            }
        }

        // API calls

        // Champions
        // Requests general champs information
        public string getChampions(int limit = 100, int skip = 0, string elo = "", string champData = "", string sort = "", Boolean abridged = false)
        {
            string requestdata = String.Format("champions?limit={0}&skip={1}&elo={2}&champData={3}&sort={4}&abridged={5}", limit, skip, elo, champData, sort, abridged);
            return Task.Run(() => { return GETRequest(requestdata); }).Result;
        }

        // Requests specific champ information
        public string getChampion(int id, int limit = 100, int skip = 0, string elo = "", string champData = "", string sort = "")
        {
            string requestdata = String.Format("champions/{0}?limit={1}&skip={2}&elo={3}&champData={4}&sort={5}", id, limit, skip, elo, champData, sort);
            return Task.Run(() => { return GETRequest(requestdata); }).Result;
        }

        // Requests specific champ information in a specific role
        public string getChampionInRole(int id, string role, int limit = 100, int skip = 0, string elo = "", string champData = "")
        {
            string requestdata = String.Format("champions/{0}/{1}?limit={2}&skip={3}&elo={4}&champData={5}", id, role, limit, skip, elo, champData);
            return Task.Run(() => { return GETRequest(requestdata); }).Result;
        }

        // Requests matchups for a specific champ in a specific role
        public string getMatchupsInRole(int id, string role, string elo = "", int skip = 0, int limit = 10)
        {
            string requestdata = String.Format("champions/{0}/{1}/matchups?elo={2}&skip={3}&limit={4}", id, role, elo, skip, limit);
            return Task.Run(() => { return GETRequest(requestdata); }).Result;
        }

        // Requests information about a specific matchup
        public string getSpecificMatchupInRole(int id, int enemy, string role, string elo = "")
        {
            string requestdata = String.Format("champions/{0}/matchups/{1}/{2}?elo={3}", id, enemy, role, elo);
            return Task.Run(() => { return GETRequest(requestdata); }).Result;
        }

        // Requests matchups for a specific champ
        public string getMatchups(int id, string elo = "", int skip = 0, int limit = 10)
        {
            string requestdata = String.Format("champions/{0}/matchups?elo={1}&skip={2}&limit={3}", id, elo, skip, limit);
            return Task.Run(() => { return GETRequest(requestdata); }).Result;
        }

        // General
        // Requests general site information
        public string getInfo(string elo = "")
        {
            string requestdata = String.Format("general?elo={0}", elo);
            return Task.Run(() => { return GETRequest(requestdata); }).Result;
        }

        // Requests overall performance champ data sets (as in championgg landing page)
        public string getOverall(string elo = "")
        {
            string requestdata = String.Format("overall?elo={0}", elo);
            return Task.Run(() => { return GETRequest(requestdata); }).Result;
        }


        // Depracted API calls

        // Champion
        // All champions' data
        public string getAllChampionData()
        {
            return Task.Run(() => { return GETDepractedRequest("champion"); }).Result;
        }

        // All data / General Data
        public string getChampionData(string champion, Boolean general = false)
        {
            return Task.Run(() => { return GETDepractedRequest("champion/" + champion + (general ? "/general" : "")); }).Result;
        }

        // Most Popular item set
        public string getMostPopularItemSet(string champion)
        {
            return Task.Run(() => { return GETDepractedRequest("champion/" + champion + "/items/finished/mostPopular"); }).Result;
        }

        // Most Popular starting item set
        public string getMostPopularStartingItemSet(string champion)
        {
            return Task.Run(() => { return GETDepractedRequest("champion/" + champion + "/items/starters/mostPopular"); }).Result;
        }

        // Most Popular skill order information
        public string getMostPopularSkillOrder(string champion)
        {
            return Task.Run(() => { return GETDepractedRequest("champion/" + champion + "/skills/mostPopular"); }).Result;
        }

        // Most Popular summoner spells
        public string getMostPopularSummoners(string champion)
        {
            return Task.Run(() => { return GETDepractedRequest("champion/" + champion + "/summoners/mostPopular"); }).Result;
        }

        // Most popular runes information
        public string getMostPopularRunes(string champion)
        {
            return Task.Run(() => { return GETDepractedRequest("champion/" + champion + "/runes/mostPopular"); }).Result;
        }

        // Most Winning item set
        public string getMostWinningItemSet(string champion)
        {
            return Task.Run(() => { return GETDepractedRequest("champion/" + champion + "/items/finished/mostWins"); }).Result;
        }

        // Most Winning starting item set
        public string getMostWinningStartingItemSet(string champion)
        {
            return Task.Run(() => { return GETDepractedRequest("champion/" + champion + "/items/starters/mostWins"); }).Result;
        }

        // Most Winning skill order information
        public string getMostWinningSkillOrder(string champion)
        {
            return Task.Run(() => { return GETDepractedRequest("champion/" + champion + "/skills/mostWins"); }).Result;
        }

        // Most Winning summoner spells
        public string getMostWinningSummoners(string champion)
        {
            return Task.Run(() => { return GETDepractedRequest("champion/" + champion + "/summoners/mostWins"); }).Result;
        }

        // Most Winning runes information
        public string getMostWinningRunes(string champion)
        {
            return Task.Run(() => { return GETDepractedRequest("champion/" + champion + "/runes/mostWins"); }).Result;
        }
    }
}
