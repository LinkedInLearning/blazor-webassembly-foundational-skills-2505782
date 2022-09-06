using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyBlazorShopHosted.Web.BlazorWasm.Client;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var http = new HttpClient()
{
    BaseAddress = new Uri("https://localhost:8002")
};
builder.Services.AddScoped(sp => http);

var webAssemblyHost = new HttpClient()
{
    BaseAddress = new Uri("https://localhost:9002")
};

using var response = await webAssemblyHost.GetAsync("productlisting.json");
using var stream = await response.Content.ReadAsStreamAsync();

builder.Configuration.AddJsonStream(stream);

using var environmentResponse = await webAssemblyHost.GetAsync("productlisting." + builder.HostEnvironment.Environment + ".json");

if (environmentResponse.IsSuccessStatusCode)
{
    using var environmentStream = await environmentResponse.Content.ReadAsStreamAsync();
    builder.Configuration.AddJsonStream(environmentStream);
}

builder.Services.AddLocalization();

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-GB");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-GB");

await builder.Build().RunAsync();
