// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using System.Threading.Tasks;
// using Discord.Commands;
// using Imgur.API;
// using Imgur.API.Authentication.Impl;
// using Imgur.API.Endpoints.Impl;
// using Imgur.API.Enums;
// using Imgur.API.Models.Impl;
// using MessaBotCore.Services.Configuration;

// namespace MessaBotCore.Modules
// {
//     [Group("Imgur")]
//     public class ImgurModule : ModuleBase
//     {
//         private Random rnd = new Random();
//         private ImgurClient ImgurClient {get;set;}
//         private DependencyMap _map;
//         private Config _config;
//         public ImgurModule(DependencyMap map) : base()
//         {
//             _config = map.Get<Config>();
//             _map = map;
//             ImgurClient = new ImgurClient(_config.ImgurId, _config.ImgurSecret);
//         }
       
//         [Command("Test"),Summary("Return a random trending gif from GfyCat using tag")]
//         public async Task Test(string subbredit="pics")
//         {

//             try
//             {
//                 var endpoint = new GalleryEndpoint(ImgurClient);
//                 var images = (await endpoint.GetSubredditGalleryAsync(subbredit,SubredditGallerySortOrder.Top, TimeWindow.Week).ConfigureAwait(false));
//             }
//             catch (ImgurException imgurEx)
//             {
//                 await ReplyAsync("An error occurred getting an image from Imgur.");
//                 await ReplyAsync(imgurEx.Message);
//             }
//             catch(Exception e)
//             {
//                 await ReplyAsync("An error occurred getting an image from Imgur.");
//                 await ReplyAsync(e.Message);
//             }
            
//         }

//     }
// }