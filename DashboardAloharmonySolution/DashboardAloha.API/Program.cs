using DasboardAloha.Entities.Helpers;
using DasboardAloha.Entities.Helpers.Interfaces;
using DashboardAloha.DataAccess.Dacs;
using DashboardAloha.DataAccess.Interfaces;
using DashboardAloha.Services.Interfaces;
using DashboardAloha.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using Microsoft.AspNetCore.Server.Kestrel.Core;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //builder.WebHost.ConfigureKestrel(serverOptions =>
        //{
            
        //});

        builder.WebHost.UseIISIntegration();

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
        builder.Services.AddTransient<IRestHelper, RestHelper>();
        builder.Services.AddTransient<IDashboardDac, DashboardDac>();
        builder.Services.AddTransient<IDashboardService, DashboardService>();

        builder.Services.AddTransient<IUsersRegistersDac, UsersRegistersDac>();
        builder.Services.AddTransient<IUsersActivesDac, UsersActivesDac>();
        builder.Services.AddTransient<IUsersContentDac, UsersContentDac>();
        builder.Services.AddTransient<IUsersDesertersDac, UsersDesertersDac>();
        builder.Services.AddTransient<IUsersGendersDac, UsersGendersDac>();
        builder.Services.AddTransient<ITotalSalesDac, TotalSalesDac>();
        builder.Services.AddTransient<IUsersDac, UsersDac>();

        builder.Services.AddCors();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCors(options =>
        {
            options.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });

        app.UseRouting();

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.UseDefaultFiles();

        app.UseStaticFiles();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API v1");
        });

        app.Run();
    }
}