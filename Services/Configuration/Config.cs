using System;
using System.IO;
using Newtonsoft.Json;
namespace MessaBotCore.Services.Configuration
{
    public class Config
    {
        [JsonProperty("Discord_Token")]
        public string DiscordToken {get;private set;}
        [JsonProperty("Riot_Token")]
        
        public string RiotToken {get;private set;}
        [JsonProperty("Gyfcat_Id")]
        public string GyfcatID {get;private set;}
        [JsonProperty("Gyfcat_Secret")]

        public string GyfcatSecret {get;private set;}
        [JsonProperty("Prefix")]

        public char Prefix {get;private set;}

        [JsonProperty("Imgur_Id")]
        public string ImgurId {get;private set;}
        [JsonProperty("Imgur_Secret")]
        public string ImgurSecret {get;private set;}

        public static Config Load()
        {
            if(File.Exists("config.json"))
            {
                return JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));
            }
            var config = new Config();
            config.Save();  
            throw new InvalidOperationException("Configuration file just created, complete it then restart ! ");
        }

        private void Save()
        {
            var json = JsonConvert.SerializeObject(this);
            File.WriteAllText("config.json", json);
        }
    }
}
