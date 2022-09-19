using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyBlazorShopHostedApi.Web.BlazorWasm.Client;
using System.Globalization;
using System.Net.Http.Headers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var http = new HttpClient()
{
    BaseAddress = new Uri("https://localhost:8002")
};
http.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
{
    NoCache = true
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

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

await builder.Build().RunAsync();
