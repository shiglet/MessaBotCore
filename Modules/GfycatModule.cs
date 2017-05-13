using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Discord.Commands;
using Gfycat;
using MessaBotCore.Services.Configuration;

namespace MessaBotCore.Modules
{
    [Group("GfyCat")]
    public class GfycatModule : ModuleBase
    {
        private Random rnd = new Random();
        private DependencyMap _map;
        private Config _config;
        private GfycatClient GfycatClient{get;set;}
        public GfycatModule(DependencyMap map ) : base()
        {
            _config = map.Get<Config>();
            _map = map;
            GfycatClient =  new GfycatClient(_config.GyfcatID,_config.GyfcatSecret);
        }
        [Command("Trending"),Summary("Return a random trending gif from GfyCat using tag")]
        public async Task Trending([RemainderAttribute]string tag="Trending")
        {
            try
            {
                var ret = await GfycatClient.GetTrendingGfysAsync(tag);
                var gifs = new List<Gfy>(ret.Content);
                await ReplyAsync(gifs[rnd.Next(0,gifs.Count)].Url);
            }
            catch(GfycatException ex)
            {
                await ReplyAsync(ex.Description);
            }
        }

        [Command("Search"),Summary("Return a random gif from GfyCat using search argument")]
        public async Task Search([RemainderAttribute]string search)
        {
            try
            {
                var gifs = new List<Gfy>((await GfycatClient.SearchAsync(search)).Content);
                await ReplyAsync(gifs[rnd.Next(0,gifs.Count)].Url);
            }
            catch(GfycatException ex)
            {
                await ReplyAsync(ex.Description);
            }
        }
    }
}