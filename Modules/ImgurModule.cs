using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Discord.Commands;
using Imgur.API.Authentication.Impl;


namespace MessaBotCore.Modules
{
    [Group("Imgur")]
    public class ImgurModule : ModuleBase
    {
        private Random rnd = new Random();
        ImgurClient client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET");

       // private var gfyClient ("2_Wz_5hd","JLd1JKMHxxjjcuJFqK7FDcKeuZnOepGNE3c52Lapae7RKO9ESnVHEtgrXU7jxZkq");

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