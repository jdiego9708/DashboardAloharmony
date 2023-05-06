using DashboardAloha.DataAccess.Dacs;
using DashboardAloha.DataAccess.Interfaces;
using DashboardAloha.Services.Interfaces;
using DashboardAloha.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Assembly GetAssemblyByName(string name)
        {
            Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().
                   SingleOrDefault(assembly => assembly.GetName().Name == name);

            if (assembly == null)
                return null;

            return assembly;
        }

        var a = GetAssemblyByName("DashboardAloha.API");

        using var stream = a.GetManifestResourceStream("DashboardAloha.API.appsettings.json");

        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();

        // Add services to the container.
        builder.Services.AddTransient<IConnectionDac, ConnectionDac>();
        builder.Services.AddTransient<IDashboardDac, DashboardDac>();
        builder.Services.AddTransient<IDashboardService, DashboardService>();

        builder.Services.AddControllers();
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

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}