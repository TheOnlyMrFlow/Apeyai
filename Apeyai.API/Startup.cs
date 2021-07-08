using Apeyai.API.UseCases.AddForeignSchemaReferenceAttributeToSchema;
using Apeyai.API.UseCases.AddTextAttributeToSchema;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Apeyai.API.UseCases.CreateEmptySchema;
using Apeyai.API.UseCases.GetSchema;
using Apeyai.Core.Infra.Persistence.Ports;
using Apeyai.Persistence.Sqlite;
using Apeyai.Persistence.Sqlite.Repositories;

namespace Apeyai.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services
                .AddControllers()
                .AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Apeyai.API", Version = "v1" }));

            services
                .AddTransient<SqliteContext>()
                // repositories
                .AddTransient<ISchemaRepository, SchemaRepository>()
                // http presenters
                .AddTransient<CreateEmptySchemaHttpPresenter>()
                .AddTransient<GetSchemaHttpPresenter>()
                .AddTransient<AddTextAttributeToSchemaHttpPresenter>()
                .AddTransient<AddForeignSchemaReferenceAttributeToSchemaHttpPresenter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Apeyai.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
