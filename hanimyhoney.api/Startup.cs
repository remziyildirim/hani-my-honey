using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;

namespace hanimyhoney.api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			// Serilog
			Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Graph DB Connection Information
			// var graphDbUrl = "https://localhost:7473/db/data";
			// var graphDbUrl = Configuration.GetConnectionString("GraphUrlForDocker");
			var graphDbUrl = Configuration.GetConnectionString("GraphUrl");
			var graphDbUser = Configuration.GetConnectionString("GraphUser");
			var graphDbPassword = Configuration.GetConnectionString("GraphPassword");

			// Dependency Injection
			services.RegisterRepository(graphDbUrl, graphDbUser, graphDbPassword);
			services.RegisterService();

			services.AddControllers();

			// Add AutoMapper
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			//Add Swagger
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = Configuration.GetValue<string>("Swagger:Title"),
					Version = Configuration.GetValue<string>("Swagger:Version"),
					TermsOfService = new Uri("https://example.com/terms"),
					Contact = new OpenApiContact()
					{
						Name = Configuration.GetValue<string>("Swagger:Contact:Name"),
						Url = new Uri(Configuration.GetValue<string>("Swagger:Contact:Url")),
						Email = Configuration.GetValue<string>("Swagger:Contact:Email")
					},
					License = new OpenApiLicense
					{
						Name = "Use under LICX",
						Url = new Uri("https://example.com/license"),
					}
				});

				// c.AddSecurityDefinition("Bearer", new ApiKeyScheme
				// {
				// 	Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
				// 	Name = "Authorization",
				// 	In = "header",
				// 	Type = "apiKey"
				// });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
		{

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			// Custom Error Handling
			app.UseExceptionMiddleware();

			// Swagger
			app.UseSwagger().UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hani My Honey");
			});

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			// Serilog
			loggerFactory.AddSerilog();
			app.UseSerilogRequestLogging();
		}
	}
}
