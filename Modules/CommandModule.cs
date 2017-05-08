using System.Threading.Tasks;
using Discord.Commands;
namespace MessaBotCore.Modules
{
    public class CommandModule : ModuleBase
    {
        [Command("Info"),Summary("Give informations")]
        public async Task Info()
        {
            await ReplyAsync("Fine");
        }
    }
}