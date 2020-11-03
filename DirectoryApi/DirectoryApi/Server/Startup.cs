using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

using Google.Cloud.SecretManager.V1;
using Npgsql;

using DirectoryApi.DataAccess;

namespace DirectoryApi.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //var connString = Configuration.GetConnectionString("DefaultConnection");

            //if (String.IsNullOrWhiteSpace(connString))
            //    connString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

            // Equivalent connection string: 
            // "Server=<dbSocketDir>/<INSTANCE_CONNECTION_NAME>;Uid=<DB_USER>;Pwd=<DB_PASS>;Database=<DB_NAME>"
            string dbSocketDir = Environment.GetEnvironmentVariable("DB_SOCKET_PATH") ?? "/cloudsql";
            string instanceConnectionName = "dev-office-294516:us-central1:cory-sql";

            var connectionString = new NpgsqlConnectionStringBuilder()
            {
                // The Cloud SQL proxy provides encryption between the proxy and instance. 
                SslMode = SslMode.Disable,

                // Remember - storing secrets in plaintext is potentially unsafe. Consider using
                // something like https://cloud.google.com/secret-manager/docs/overview to help keep
                // secrets secret.
                Host = string.Format("{0}/{1}", dbSocketDir, instanceConnectionName),
                Username = GetDBUserName(),
                Password = GetDBPassword(),
                Database = "postgres"

            };
            connectionString.Pooling = true;

            NpgsqlConnection connection =
                new NpgsqlConnection(connectionString.ConnectionString);

            //  PostgreSql Database
            if (connection != null)
                services.AddDbContext<DirectoryContext>(options => options.UseNpgsql(connection));

            services.AddTransient<IRepository, RepositoryPg>();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }

        string GetDBPassword()
        {
            // Create the client.
            SecretManagerServiceClient client = SecretManagerServiceClient.Create();

            // Build the resource name.
            SecretVersionName secretVersionName = new SecretVersionName(
                                                        "dev-office-294516",
                                                        "db-password",
                                                        "1");

            // Call the API.
            AccessSecretVersionResponse result = client.AccessSecretVersion(secretVersionName);

            // Convert the payload to a string. Payloads are bytes by default.
            return result.Payload.Data.ToStringUtf8();
        }

        string GetDBUserName()
        {
            // Create the client.
            SecretManagerServiceClient client = SecretManagerServiceClient.Create();

            // Build the resource name.
            SecretVersionName secretVersionName = new SecretVersionName(
                                                        "dev-office-294516",
                                                        "db-user-name",
                                                        "1");

            // Call the API.
            AccessSecretVersionResponse result = client.AccessSecretVersion(secretVersionName);

            // Convert the payload to a string. Payloads are bytes by default.
            return result.Payload.Data.ToStringUtf8();
        }
    }
}
