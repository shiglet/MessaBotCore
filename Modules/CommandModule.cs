using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Discord.Commands;
using Gfycat;


namespace MessaBotCore.Modules
{
    [Group("GfyCat")]
    public class CommandModule : ModuleBase
    {
        private Random rnd = new Random();
        private GfycatClient gfyClient = new GfycatClient("2_Wz_5hd","JLd1JKMHxxjjcuJFqK7FDcKeuZnOepGNE3c52Lapae7RKO9ESnVHEtgrXU7jxZkq");

        [Command("Trending"),Summary("Return a random trending gif from GfyCat")]
        public async Task Trending(string category=null)
        {
            var gifs = new List<Gfy>((await gfyClient.GetTrendingGfysAsync(category)).Content);
            await ReplyAsync(gifs[rnd.Next(0,gifs.Count)].Url);
        }

        [Command("Search"),Summary("Return a random gif from GfyCat using search")]
        public async Task Search([RemainderAttribute]string search)
        {
                var gifs = new List<Gfy>((await gfyClient.SearchAsync(search)).Content);
                await ReplyAsync(gifs[rnd.Next(0,gifs.Count)].Url);
        }
    }
}