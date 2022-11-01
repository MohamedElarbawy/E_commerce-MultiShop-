using CoreLayer;
using CoreLayer.Interfaces;
using BusinessLogicLayer;
using BusinessLogicLayer.Repositories;
using BusinessLogicLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using MVC_Layer.Mapper;
using Microsoft.AspNetCore.Identity;
using X.Paymob.CashIn;
using Hangfire;
using MVC_Layer.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
.AddDataAnnotationsLocalization();
// services.AddLocalization(opt => opt.ResourcesPath = "");
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IMailService,MailService>();

builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));
  
builder.Services.AddDbContext<MultiShopContext>(options =>
options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<MultiShopContext>()   
 .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(TokenOptions.DefaultProvider);
builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
});
builder.Services.AddPaymobCashIn(config => {
   
    config.ApiKey = builder.Configuration["PaymobKeys:apikey"];
    config.Hmac = builder.Configuration["PaymobKeys:hmac"];
});

//builder.Services.AddSqlServer<MultiShopContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var supportedCultures = new[] {
      new CultureInfo("ar-EG"),
      new CultureInfo("en-US"),
};
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
    RequestCultureProviders = new List<IRequestCultureProvider>
                {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
                }
});
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseHangfireDashboard("/HangfireDashBoard");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
