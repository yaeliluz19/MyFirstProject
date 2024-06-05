using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services;
using Repositories;
using MyFirstProject;
using NLog.Web;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseNLog();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Market326354982Context>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings"]));
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepositories, UserRepositories>();

builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IOrderRepositories, OrderRepositories>();

builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProductRepositories, ProductRepositories>();

builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ICategoryReposities, CategoryReposities>();

builder.Services.AddTransient<IRatingService, RatingService>();
builder.Services.AddTransient<IRatingRepository, RatingRepository>();



builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();
app.UseAuthorization();

app.UseMiddleware();
app.UseErrorMiddleware();

app.UseStaticFiles();

app.MapControllers();

app.Run();
