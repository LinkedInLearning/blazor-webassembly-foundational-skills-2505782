using MyBlazorShopHosted.Libraries.Services.Product;
using MyBlazorShopHosted.Libraries.Services.ShoppingCart;
using MyBlazorShopHosted.Libraries.Services.Storage;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Dependency injection
builder.Services.AddSingleton<IStorageService, StorageService>();
builder.Services.AddSingleton<IShoppingCartService, ShoppingCartService>();
builder.Services.AddTransient<IProductService, ProductService>();

builder.Host.UseSerilog((hbc, lc) => lc.ReadFrom.Configuration(hbc.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSerilogIngestion();
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();