using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using MessaBotCore.Services.Configuration;
using RedditSharp;
using RedditSharp.Things;

namespace MessaBotCore.Modules
{
    [Group("Reddit")]
    public class RedditModule : ModuleBase
    {
        private Reddit _redditClient;
        private Random _rnd = new Random();
        private Config _config;
        private DependencyMap _map;
        public RedditModule(DependencyMap map)
        {
            _map = map;
            _config = map.Get<Config>();
            var webAgent = new BotWebAgent(_config.RedditUsername,_config.RedditPassword,_config.RedditClientId,_config.RedditSecret,"https://www.reddit.com");
            _redditClient = new Reddit(webAgent,false);
        }
        [Command("Gif"),Summary("Get a random gif from Reddit /r/gifs")]
        public async Task Gif()
        {
            var subReddit = await _redditClient.GetSubredditAsync("/r/gifs");
            var top = await (subReddit.GetTop(FromTime.Day).Take(50)).ToList();
            int r = _rnd.Next(top.Count);
            await ReplyAsync(top.ElementAt(r).Url.AbsoluteUri);
        }
        [Command("Funny"),Summary("Get a random gif/video/pic from Reddit /r/funny")]
        public async Task Funny()
        {
            var subReddit = await _redditClient.GetSubredditAsync("/r/funny");
            var top = await (subReddit.GetTop(FromTime.Day).Take(50).ToList());
            int r = _rnd.Next(top.Count);
            var post = top.ElementAt(r);
            await ReplyAsync($"***{post.Title}*** \r\n {post.Url.AbsoluteUri}") ;
        }
        [Command("Hell"),Summary("Get a random gif/video/pic from Reddit /r/ImGoingToHellForThis/")]
        public async Task Hell()
        {
            var subReddit = await _redditClient.GetSubredditAsync("r/ImGoingToHellForThis/");
            var top = await (subReddit.GetTop(FromTime.Week).Take(100).ToList());
            int r = _rnd.Next(top.Count);
            var post = top.ElementAt(r);
            await ReplyAsync($"***{post.Title}*** \r\n {post.Url.AbsoluteUri}") ;
        }
    }
}