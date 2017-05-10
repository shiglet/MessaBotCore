using System;
using Discord.WebSocket;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.Diagnostics;
using NLog;
using NLog.Config;
using NLog.Targets;
using System.Reflection;
using MessaBotCore.Services.Configuration;

namespace MessaBotCore
{
    class MessaBot
    {
        private Config _config;
        private DependencyMap _map = new DependencyMap();
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
            _config = Config.Load();
            _log = LogManager.GetCurrentClassLogger();
            _log.Info("Starting MessaBot");

            Client.Log += Log;
            await InitCommands();
            _map.Add(Client);
            _map.Add(_config);
            CommandHandler = new CommandHandler(CommandService,_map);
            try
            {
                var sw = Stopwatch.StartNew();
                await Client.LoginAsync(TokenType.Bot, _config.DiscordToken);
                await Client.StartAsync().ConfigureAwait(false);
                sw.Stop();
                _log.Info($"Connected in {sw.Elapsed.TotalSeconds.ToString("F2")}");
            }
            catch(Exception e )
            {
                Console.WriteLine($"ERROR :  Connection to discord API failed : {e.Message}");
            }

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