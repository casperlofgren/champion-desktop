/*
 * DDragon parser
 * Initial author: Casper Löfgren
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace champion_desktop
{
    class DDragonParser
    {
        private Data dragondata;

        public class Champion
        {
            public string name;
            public string id;
            public string key;
            public string title;
            public string image;
        }

        private class Data
        {
            public string version;
            public string format;
            public List<Champion> champions { get; set; }
        }

        public DDragonParser()
        {
            // Load ddragon data
            using (StreamReader reader = new StreamReader(@"data\champs.json"))
            {
                string json = reader.ReadToEnd();
                dragondata = JsonConvert.DeserializeObject<Data>(json);
            }
        }

        public string getDataVersion()
        {
            return dragondata.version;
        }

        public string getFormat()
        {
            return dragondata.format;
        }

        public List<Champion> getChampions()
        {
            return dragondata.champions;
        }
    }
}
