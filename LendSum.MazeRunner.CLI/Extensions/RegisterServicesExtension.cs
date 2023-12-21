using LendSum.MazeRunner.CLI.Clients;
using LendSum.MazeRunner.CLI.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;

namespace LendSum.MazeRunner.CLI.Extensions
{
    public static class RegisterServicesExtensions
    {
        private static readonly string mazeRunnerBaseUrl = "https://mazerunnerapi6.azurewebsites.net/api";
        public static IHostBuilder ConfigureCliServices(this IHostBuilder builder)
        {
            return builder.ConfigureServices((_, services) =>
            {
                services.AddScoped(x => RestService.For<IMazeRunnerClient>(mazeRunnerBaseUrl));
                services.AddScoped<IMazeRunnerRepository, MazeRunnerRepository>();
                services.AddSingleton<App>();
            });
        }
    }
}
