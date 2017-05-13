using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using MessaBotCore.Services.Configuration;
namespace MessaBotCore.Modules
{
    public class HelpModule : ModuleBase
    {
        private CommandService _service;
        private Config _config;
        private DependencyMap _map;

        public HelpModule(DependencyMap map ,CommandService service) : base()          // Create a constructor for the commandservice dependency
        {
            _map = map;
            _service = service;
            _config = map.Get<Config>();
        }

        [Command("help"),Summary("Display all available commands")]
        public async Task HelpAsync()
        {
            string prefix = _config.Prefix.ToString();
            var builder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
                Description = "These are the commands you can use"
            };
            
            foreach (var module in _service.Modules)
            {
                string description = null;
                foreach (var cmd in module.Commands)
                {
                    var result = await cmd.CheckPreconditionsAsync(Context);
                    if (result.IsSuccess)
                    {
                        string param="";
                        if(cmd.Parameters.FirstOrDefault() != null)
                            param ="<"+ cmd.Parameters.FirstOrDefault().ToString() +">";
                        description += $"***{prefix}{cmd.Aliases.First()} *** {param}\n";
                    }
                }
                
                if (!string.IsNullOrWhiteSpace(description))
                {
                    builder.AddField(x =>
                    {
                        x.Name = module.Name;
                        x.Value = description;
                        x.IsInline = false;
                    });
                }
            }

            await ReplyAsync("", false, builder.Build());
        }

        [Command("help"),Summary("Display information about a command")]
        public async Task HelpAsync([RemainderAttribute]string command)
        {
            var result = _service.Search(Context, command);

            if (!result.IsSuccess)
            {
                await ReplyAsync($"Sorry, I couldn't find a command like **{command}**.");
                return;
            }

            string prefix = _config.Prefix.ToString();
            var builder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
                Description = $"Here are some commands like **{command}**"
            };

            foreach (var match in result.Commands)
            {
                var cmd = match.Command;

                builder.AddField(x =>
                {
                    x.Name = string.Join(", ", cmd.Aliases);
                    x.Value = $"Parameters: {string.Join(", ", cmd.Parameters.Select(p => p.Name))}\n" + 
                                $"Summary: {cmd.Summary}";
                    x.IsInline = false;
                });
            }

            await ReplyAsync("", false, builder.Build());
        }
    }
}