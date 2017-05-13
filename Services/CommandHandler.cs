using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using MessaBotCore.Services.Configuration;

namespace MessaBotCore.Services
{
    public class CommandHandler
    {
        private DiscordSocketClient Client{get;set;}
        private CommandService CommandService{get;set;}
        private DependencyMap _map;
        private Config _config;
        
        public CommandHandler(CommandService commandService,DependencyMap map)
        {
            Client = map.Get<DiscordSocketClient>();
            CommandService = commandService;
            _map = map;
            _config = map.Get<Config>();
            Client.MessageReceived += HandleCommand;
        }

        private async Task HandleCommand(SocketMessage arg)
        {
            var message = (SocketUserMessage) arg;
            if(message == null) return;
            int pos = 0;
            if (!(message.HasCharPrefix(_config.Prefix, ref pos) || message.HasMentionPrefix(Client.CurrentUser, ref pos))) return;

            var context = new CommandContext(Client,message);
            
            var result = await CommandService.ExecuteAsync(context, pos,_map);
        }
    }
}