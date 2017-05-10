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

        [Command("Trending"),Summary("Return a random trending gif from GfyCat using tag")]
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

        [Command("Search"),Summary("Return a random gif from GfyCat using search")]
        public async Task Search([RemainderAttribute]string search)
        {
            try
            {
                var gifs = new List<Gfy>((await gfyClient.SearchAsync(search)).Content);
                await ReplyAsync(gifs[rnd.Next(0,gifs.Count)].Url);
            }
            catch(GfycatException ex)
            {
                await ReplyAsync(ex.Description);
            }
        }
    }
}