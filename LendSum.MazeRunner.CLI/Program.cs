using LendSum.MazeRunner.CLI;
using LendSum.MazeRunner.CLI.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

try
{
    using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureCliServices()
    .Build();

    using var scope = host.Services.CreateScope();

    await scope.ServiceProvider.GetRequiredService<App>().Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}


