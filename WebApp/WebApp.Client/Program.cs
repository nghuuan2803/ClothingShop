using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace WebApp.Client;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        await builder.Build().RunAsync();
    }
}
