﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAPI.Services;

namespace WebAPI
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
			services.AddControllers();
			services
					.AddAuthentication(sharedOptions =>
					{
						sharedOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
					})
					.AddJwtBearer(options =>
					{
						var authSettings = Configuration.GetSection("AzureAd").Get<AzureAdOptions>();

						options.Audience = authSettings.ClientId;
						options.Authority = authSettings.Authority;
					});

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddScoped<IIdentityService, AzureAdIdentityService>();
			services.AddCors();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseAuthentication();

			app.UseCors(builder => builder
			.SetIsOriginAllowed(_ => true)
			.AllowAnyMethod()
			.AllowAnyHeader()
			.AllowCredentials());

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();
			//app.UseMvc();
		}
	}
}
