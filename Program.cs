using eTickets.Data;
using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//DbContext Services Configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


//Services Configurations
builder.Services.AddScoped<IActorsService, ActorsService>();
builder.Services.AddScoped<ICinemasService, CinemasService>();
builder.Services.AddScoped<IProducersService, ProducersService>();
builder.Services.AddScoped<IMoviesService, MoviesService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

//(Authentication and Authorization) Identity Services Configuration
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

//External Authentication using Google (Gmail accounts)
// builder.Services.AddAuthentication().
//     AddGoogle(options =>
//      {
//          IConfigurationSection GoogleAuthSection = builder.Configuration.GetSection("Authentication:Google");
//          options.ClientId = GoogleAuthSection["ClientId"];
//          options.ClientSecret = GoogleAuthSection["ClientSec"];
//      });
//////////////Note/////////////////////////
//    in appsettings.json: replace the next two lines...
//   "ClientId": "819539612309-qsq8haelp45a581tq78cohsnttfscfch.apps.googleusercontent.com",
//   "ClientSec": "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"





builder.Services.AddMemoryCache();
builder.Services.AddSession();


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movies}/{action=Index}/{id?}");
app.MapRazorPages();

AppDbInitializer.Seed(app);
app.Run();
