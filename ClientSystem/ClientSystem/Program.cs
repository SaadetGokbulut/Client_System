using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;
using MyMongoWebApp.Models;
using MyMongoWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------------------------------------------
// MongoDB Ayarlarını appsettings.json'dan yükle
// -------------------------------------------------------------------
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings")
);

// -------------------------------------------------------------------
// MongoClient'i DI container'a ekle
// -------------------------------------------------------------------
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

// -------------------------------------------------------------------
// CustomerService'i DI container'a ekle
// -------------------------------------------------------------------
builder.Services.AddSingleton<ICustomerService, CustomerService>();

// -------------------------------------------------------------------
// MVC Desteğini ekle
// -------------------------------------------------------------------
builder.Services.AddControllersWithViews();

var app = builder.Build();

// -------------------------------------------------------------------
// Hata yönetimi ve HTTPS ayarları
// -------------------------------------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// -------------------------------------------------------------------
// Varsayılan yönlendirme: CustomerController -> Index action
// -------------------------------------------------------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Customer}/{action=Index}/{id?}"
);

app.Run();
