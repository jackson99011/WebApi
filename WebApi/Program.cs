

using Microsoft.EntityFrameworkCore;
using WebApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<ContosouniversityContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthorization();

app.MapControllers();



//´ú¸Õ°õ¦æ½d¨Ò
//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("1");
//    await next();
//    await context.Response.WriteAsync("2");
//});

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("3");
//    await next();
//    await context.Response.WriteAsync("4");
//});

//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("5");
//});

app.Run();