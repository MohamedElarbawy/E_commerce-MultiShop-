using CoreLayer;
using CoreLayer.Interfaces;
using BusinessLogicLayer;
using BusinessLogicLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using MVC_Layer.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductsRepo, ProductsRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

builder.Services.AddDbContext<MultiShopContext>(options =>
options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
//builder.Services.AddSqlServer<MultiShopContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
var app = builder.Build();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
