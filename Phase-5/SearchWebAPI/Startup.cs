using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using SearchEngine.Classes;
using SearchEngine.Classes.Core;
using SearchEngine.Classes.Indexers;
using SearchEngine.Classes.IO;
using SearchEngine.Interfaces.Core;

namespace SearchWebAPI
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private const string DatabaseConfigPath = "WebApiDatabaseConfig.json";
        private DatabaseConfig _databaseConfig = new ();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            );
            _databaseConfig = JsonFileConverter.ReadConfig<DatabaseConfig>(DatabaseConfigPath);
            services.Add(new ServiceDescriptor(typeof(ISearchEngineCore), 
                new SearchEngineCore(new WordProcessor(),
                    new DatabaseInvertedIndex(_databaseConfig.DatabaseProvider))));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}