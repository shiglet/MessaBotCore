using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
namespace MessaBotCore
{
    public class CommandHandler
    {
        private DiscordSocketClient Client{get;set;}
        private CommandService CommandService{get;set;}
        
        public CommandHandler(DiscordSocketClient client, CommandService commandService)
        {
            Client = client;
            CommandService = commandService;
            client.MessageReceived += HandleCommand;
        }

        private async Task HandleCommand(SocketMessage arg)
        {
            var message = (SocketUserMessage) arg;
            if(message == null) return;
            int pos = 0;//prefix position start
            if (!(message.HasCharPrefix('?', ref pos) || message.HasMentionPrefix(Client.CurrentUser, ref pos))) return;

            var context = new CommandContext(Client,message);
            var result = await CommandService.ExecuteAsync(context, pos);
        }
    }
}