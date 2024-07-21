using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Syncfusion.Licensing;
using LibraryBook.Application.Common.Interfaces;
using LibraryBook.Application.Services.Implementation;
using LibraryBook.Application.Services.Interface;
using LibraryBook.Domain.Entities;
using LibraryBook.Infrastructure.Data;
using LibraryBook.Infrastructure.Repositoty;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IDbInitializer,DbInitializer>();
builder.Services.AddScoped<IVillaService,VillaService>();
builder.Services.AddScoped<IVillaNumberService,VillaNumberService>();
builder.Services.AddScoped<IAmenityService,AmenityService>();
builder.Services.AddScoped<IBookingService,BookingService>();
builder.Services.AddScoped<IWebsiteInfomationService,WebsiteInfomationService>();
builder.Services.AddScoped<IMenuCategoryService,MenuCategoryService>();
builder.Services.AddScoped<IMenuService,MenuService>();
builder.Services.AddScoped<IBannerCategoryService,BannerCategoryService>();
builder.Services.AddScoped<IBannerService,BannerService>();

builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.ConfigureApplicationCookie(option =>
{
    option.AccessDeniedPath = "/Account/AccessDenied";
    option.LoginPath = "/Account/Login";
});
builder.Services.Configure<IdentityOptions>(option =>
{
    option.Password.RequiredLength = 8;
});

var app = builder.Build();
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
SyncfusionLicenseProvider.RegisterLicense(builder.Configuration.GetSection("Syncfusion:Licensekey").Get<string>());

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
SeedDatabase();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}