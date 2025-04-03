using BLL.API;
using BLL.Services;
using DAL.API;
using DAL.Models;
using DAL.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<DB_Manager>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IGoodsService, GoodsService>();
builder.Services.AddScoped<ISuppliersService, SuppliersService>();
builder.Services.AddScoped<IGoodsToSupplierService, GoodsToSupplierService>();
builder.Services.AddScoped<IGoodsToOrdersService, GoodsToOrdersService>();

builder.Services.AddScoped<IOrdersManagment, OrdersManagment>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

   

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
