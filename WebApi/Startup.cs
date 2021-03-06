using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Domain.Order.Adapters;
using Domain.Order.Enums;
using Domain.Order.Models;
using Domain.Order.Ports;
using Domain.Order.UseCases;
using Infrastructure.Common.Contexts;
using Infrastructure.Order.Adapters;
using Infrastructure.Order.Commands.ApproveOrder;
using Infrastructure.Order.Commands.CancelOrder;
using Infrastructure.Order.Commands.DeliveryOrder;
using Infrastructure.Order.Queries.FindOrderByName;
using Infrastructure.Order.Queries.FindOrderByNumber;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebApi.Interfaces;
using WebApi.StateMachines;

namespace WebApi
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDatabase(services);
            ConfigureSwagger(services);
            ConfigureAutoMapper(services);
            ConfigureSettings(services);
            ConfigureQueries(services);
            ConfigureCommands(services);
            ConfigurePorts(services);
            ConfigureAdapters(services);
            ConfigureStateMachine(services);

            services.AddLogging(configure =>
            {
                configure.AddConsole();
            });

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddFeatureManagement();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Api V1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            string readOnlyConnectionString = configuration.GetConnectionString("ReadOnlyContext");
            string writeOnlyConnectionString = configuration.GetConnectionString("WriteOnlyContext");

            services.AddDbContext<ReadContext>(options => options.UseInMemoryDatabase(readOnlyConnectionString));
            services.AddDbContext<WriteContext>(options => options.UseInMemoryDatabase(writeOnlyConnectionString));

            services.AddUnitOfWork<ReadContext>();
            services.AddUnitOfWork<WriteContext>();
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                //c.SwaggerDoc("v1.0", new OpenApiInfo
                //{
                //    Title = "My Api",
                //    Version = "v1.0"
                //});

                //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Name = "Authorization",
                //    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                //    Scheme = "Bearer",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey
                //});

                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Name = "Bearer",
                //            In = ParameterLocation.Header,
                //            Reference = new OpenApiReference
                //            {
                //                Id = "Bearer",
                //                Type = ReferenceType.SecurityScheme
                //            }
                //        },
                //        new List<string>()
                //    }
                //});
            });
        }

        private void ConfigureAutoMapper(IServiceCollection services)
        {
            IEnumerable<Type> profiles = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => typeof(Profile).IsAssignableFrom(type));

            services.AddAutoMapper(profiles.ToArray());
        }

        private void ConfigureSettings(IServiceCollection services)
        {
            //services.Configure<MySetting>(configuration.GetSection(nameof(MySetting)));
        }

        private void ConfigureQueries(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<FindOrderByNumberQuery, Order>, FindOrderByNumberQueryHandler>();
        }

        private void ConfigureCommands(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<ApproveOrderCommand, bool>, ApproveOrderCommandHandler>();
            services.AddScoped<IRequestHandler<CancelOrderCommand, bool>, CancelOrderCommandHandler>();
            services.AddScoped<IRequestHandler<DeliveryOrderCommand, bool>, DeliveryOrderCommandHandler>();
        }

        private void ConfigurePorts(IServiceCollection services)
        {
            services.AddScoped<IFindOrderByNumberPort, FindOrderByNumberUseCase>();
            services.AddScoped<IApproveOrderPort, ApproveOrderUseCase>();
            services.AddScoped<ICancelOrderPort, CancelOrderUseCase>();
            services.AddScoped<IDeliveryOrderPort, DeliveryOrderUseCase>();
        }

        private void ConfigureAdapters(IServiceCollection services)
        {
            services.AddScoped<IFindOrderByNumberAdapter, OrderMemoryAdapter>();
            services.AddScoped<IApproveOrderAdapter, OrderMemoryAdapter>();
            services.AddScoped<ICancelOrderAdapter, OrderMemoryAdapter>();
            services.AddScoped<IDeliveryOrderAdapter, OrderMemoryAdapter>();
        }

        private void ConfigureStateMachine(IServiceCollection services)
        {
            services.AddScoped<IStateMachine<Order, OrderStatus, OrderTrigger>, OrderStateMachine>();
        }
    }
}
