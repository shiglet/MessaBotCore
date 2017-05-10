using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Discord.Commands;
using Imgur.API.Authentication.Impl;
using MessaBotCore.Services.Configuration;

namespace MessaBotCore.Modules
{
    [Group("Imgur")]
    public class ImgurModule : ModuleBase
    {
        private Random rnd = new Random();
        private ImgurClient ImgurClient {get;set;}
        private DependencyMap _map;
        private Config _config;
        public ImgurModule(DependencyMap map)
        {
            _config = map.Get<Config>();
            _map = map;
            ImgurClient = new ImgurClient(_config.ImgurId, _config.ImgurSecret);
        }
       // 
 /*       [Command("Random"),Summary("Return a random trending gif from GfyCat using tag")]
        public async Task Trending(string tag="Trending")
        {

            try
            {
                var gifs = new List<Gfy>((await gfyClient.GetTrendingGfysAsync(tag)).Content);
                await ReplyAsync(gifs[rnd.Next(0,gifs.Count)].Url);
            }
            catch(GfycatException ex)
            {
                await ReplyAsync(ex.Description);
            }
        }
*/
    }
}