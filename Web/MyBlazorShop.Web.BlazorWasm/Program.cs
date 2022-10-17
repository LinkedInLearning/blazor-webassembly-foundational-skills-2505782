using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyBlazorShop.Web.BlazorWasm;
using MyBlazorShop.Libraries.Services.Product;
using MyBlazorShop.Libraries.Services.Storage;
using MyBlazorShop.Libraries.Services.ShoppingCart;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var http = new HttpClient()
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
};
http.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
{
    NoCache = true
};
builder.Services.AddScoped(sp => http);
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

using var response = await http.GetAsync("productlisting.json");
using var stream = await response.Content.ReadAsStreamAsync();

builder.Configuration.AddJsonStream(stream);

using var environmentResponse = await http.GetAsync("productlisting." + builder.HostEnvironment.Environment + ".json");

if (environmentResponse.IsSuccessStatusCode)
{
    using var environmentStream = await environmentResponse.Content.ReadAsStreamAsync();
    builder.Configuration.AddJsonStream(environmentStream);
}

builder.Services.AddSingleton<IStorageService, StorageService>();
builder.Services.AddSingleton<IShoppingCartService, ShoppingCartService>();
builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddLocalization();

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-GB");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-GB");

await builder.Build().RunAsync();
