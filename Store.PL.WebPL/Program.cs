using Microsoft.EntityFrameworkCore;
using Store.BLL;
using Store.BLL.Interfaces;
using Store.DAL;
using Store.DAL.Context;
using Store.DAL.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<StoreDbContext>(x => x.UseSqlServer(connection));

builder.Services.AddScoped<IOrderDao, OrderDao>();
builder.Services.AddScoped<IOrderLogic, OrderLogic>();

builder.Services.AddScoped<IOrderItemDao, OrderItemDao>();
builder.Services.AddScoped<IOrderItemLogic, OrderItemLogic>();

builder.Services.AddScoped<IProviderDao, ProviderDao>();
builder.Services.AddScoped<IProviderLogic, ProviderLogic>();

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
    pattern: "{controller=Orders}/{action=Index}/{id?}");

app.Run();
