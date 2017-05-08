using System;
using Discord.WebSocket;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using NLog;
using NLog.Config;
using NLog.Targets;
using System.Reflection;

namespace MessaBotCore
{
    class MessaBot
    {
        private Logger _log;
        public DiscordSocketClient  Client { get;private set; }
        public CommandService CommandService { get; private set; }
        public CommandHandler CommandHandler{get;private set;}
        private MessaBot()
        {
            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });
        }
        static void Main(string[] args) => new MessaBot().RunAsync(args).GetAwaiter().GetResult();
        public async Task RunAsync(string[] args)
        {
            SetupLogger();
            string riotToken;
            string token;
            _log = LogManager.GetCurrentClassLogger();
            _log.Info("Starting MessaBot");

            Client.Log += Log;
            await InitCommands();
            CommandHandler = new CommandHandler(Client,CommandService);
            var config = new ConfigurationBuilder()
            .AddJsonFile("config.json")
            .Build();
            token = config["Discord_Token"];
            riotToken = config["Riot_Token"];

            try
            {
                var sw = Stopwatch.StartNew();
                await Client.LoginAsync(TokenType.Bot, token);
                await Client.StartAsync().ConfigureAwait(false);
                sw.Stop();
                _log.Info($"Connected in {sw.Elapsed.TotalSeconds.ToString("F2")}");
            }
            catch(Exception e )
            {
                Console.WriteLine($"ERROR :  Connection to discord API failed : {e.Message}");
            }

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private async Task InitCommands()
        {
            CommandService = new CommandService(new CommandServiceConfig() {
                CaseSensitiveCommands = false,
                DefaultRunMode = RunMode.Sync
            });
            await CommandService.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private Task Log(LogMessage message)
        {
            _log.Warn(message.Source + " | " + message.Message);
            if (message.Exception != null)
                _log.Warn(message.Exception);

            return Task.CompletedTask;
        }
        private static void SetupLogger()
        {
            try
            {
                var logConfig = new LoggingConfiguration();
                var consoleTarget = new ColoredConsoleTarget();

                consoleTarget.Layout = @"${date:format=HH\:mm\:ss} ${logger} | ${message}";

                logConfig.AddTarget("Console", consoleTarget);

                logConfig.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, consoleTarget));

                LogManager.Configuration = logConfig;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}