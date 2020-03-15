using MediatR;
using Kanbersky.Customer.DAL.Concrete.EntityFramework.Context;
using Kanbersky.Customer.DAL.Concrete.EntityFramework.GenericRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Kanbersky.Customer.Business.Mappings.AutoMapper;
using Kanbersky.Customer.Business.Handlers;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.IO;
using FluentValidation.AspNetCore;
using Kanbersky.Customer.Business.Validators;
using Kanbersky.Customer.Business.DTO.Request;
using FluentValidation;
using Kanbersky.Customer.Core.Extensions;
using Kanbersky.Customer.Business.Extensions;
using Kanbersky.Customer.Core.Validators;

namespace Kanbersky.Customer.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configure Services Methods
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new ResponseValidator());
            });

            services.AddControllers()
                .ConfigureApiBehaviorOptions(opt=> 
                {
                    opt.SuppressModelStateInvalidFilter = true;
                })
                .AddFluentValidation();

            services.AddDbContext<KanberContext>(opt =>
            {
                opt.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]);
            });

            services.RegisterHandlers();
            services.RegisterValidators();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BusinessProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Kanbersky.Customer Microservice",
                    Version = "v1",
                    Description = "An API to perform Customer operations",
                    Contact = new OpenApiContact
                    {
                        Name = "Þefik Can Kanber",
                        Email = "sefikcankanber@gmail.com",
                        Url = new Uri("https://github.com/Sefikcan"),
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// Configure Methods
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseExceptionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kanbersky Customer");
            });
        }
    }
}
