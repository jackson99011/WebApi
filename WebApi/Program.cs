

using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System.Text.Json;
using WebApi.Datas;
using WebApi.Models;

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.Seq("http://localhost:5341")
            .CreateLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

    //builder.Logging.ClearProviders();
    //builder.Logging.AddJsonConsole(option =>
    //{
    //    option.JsonWriterOptions = new JsonWriterOptions
    //    {
    //        Indented = true
    //    };
    //    option.IncludeScopes = true;
    //});
    //builder.Logging.AddDebug();

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

    // Add setting to the container.

    builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

    // Add services to the container.

    builder.Services.AddControllers();

    // Add services to the Serilog
    builder.Host.UseSerilog();

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



    //MiddleWares´ú¸Õ°õ¦æ½d¨Ò
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

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}