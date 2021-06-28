using System;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Neo.Extensions.Api
{
    public static class NeoApi
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services
                .AddMvcCore(op => op.Filters.Add(typeof(RequestValidatorFilter)))
                .AddNewtonsoftJson(op =>
                {
                    op.SerializerSettings.Converters.Add(new StringEnumConverter());
                    op.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddControllers();

            services.AddCors(options => options.AddPolicy("CorsPolicy", policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }));

            services.AddResponseCompression();

            return services;
        }

        public static void UseFluentValidation(this IApplicationBuilder app, Type assemblyContainingValidators)
        {
            var mvcCoreBuilder = (IMvcCoreBuilder)app.ApplicationServices.GetService(typeof(IMvcCoreBuilder));
            mvcCoreBuilder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining(assemblyContainingValidators));
        }

        public static void UseApi(this IApplicationBuilder app, IConfiguration configuration, IWebHostEnvironment env)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors("CorsPolicy");
            app.UseStaticFiles();
            //app.ConfigureSwagger(configuration);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public static IServiceCollection AddMediator(this IServiceCollection services, ApplicationSettings applicationSettings)
        {
            if (applicationSettings == null)
                throw new ArgumentNullException(nameof(applicationSettings));

            foreach (var assembly in applicationSettings.Assemblies)
                services.AddMediatR(AppDomain.CurrentDomain.Load(assembly));

            return services;
        }

        public static IServiceCollection AddAutomapper(this IServiceCollection services, ApplicationSettings applicationSettings)
        {
            if (applicationSettings == null)
                throw new ArgumentNullException(nameof(applicationSettings));

            foreach (var assembly in applicationSettings.Assemblies)
                services.AddAutoMapper(AppDomain.CurrentDomain.Load(assembly));

            return services;
        }
    }
}
