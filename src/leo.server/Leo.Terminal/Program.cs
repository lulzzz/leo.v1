using CommandLine;
using Leo.Actors.Interfaces;
using Leo.Core.Id;
using Leo.Core.Id.Bson;
using Ninject;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MarkdownLog;
using Leo.Actors.Interfaces.Users;
using Leo.Actors.Interfaces.Boards;

namespace Leo.Terminal
{
    class Program
    {
        private static readonly Parser _parser = new Parser(config =>
        {
            config.HelpWriter = Console.Out;
            config.EnableDashDash = true;
        });
        private static readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private static IKernel _kernel;
        private static IClusterClient _cluster;

        static void Main(string[] args)
        {
            Console.CancelKeyPress += (sender, cancelArgs) =>
            {
                _tokenSource.Cancel();
            };

            _kernel = new StandardKernel()
                .WithBsonIds()
                .WithActorsClient();
            _cluster = _kernel.Get<IClusterClient>();
            _cluster.Connect().Wait();
            MainAsync(args).Wait();
        }

        static async Task MainAsync(string[] args)
        {            
            try
            {   
                Console.WriteLine("Enter command...");
                var input = Console.ReadLine();
                var commandArgs = input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                _parser.ParseArguments<GetBoards, AddAccounts>(commandArgs)
                    .WithParsedAsync(Execute);
            }
            catch (AggregateException ex)
            {
                foreach (var err in ex.InnerExceptions)
                {
                    Console.WriteLine($"Exception Message: {err.Message}");
                    Console.WriteLine($"Exception Stack: {err.StackTrace}");
                }
            }
            finally
            {
                await MainAsync(args);
            }
        }

        static async Task Execute(GetBoards verb)
        {
            var groups = await _cluster.GetGrain<IUserAggregate>(verb.UserId).GetBoardsAsync();

            Console.Write(groups.ToMarkdownTable());
        }
    }

    public static class ParserResultExtensions
    {
        public static ParserResult<T> WithParsedAsync<T>(this ParserResult<T> result, Func<T, Task> action)
        {
            return result.WithParsed(x => 
            {
                try
                {
                    action(x).Wait();
                }
                catch(Exception ex)
                {
                    throw;
                }
            });
        }
    }

    [Verb("boards", HelpText = "get a list of boards")]
    public class GetBoards
    {
        public GetBoards(string userId)
        {
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
        }

        [Option('u')]
        public string UserId { get; }
    }
}
