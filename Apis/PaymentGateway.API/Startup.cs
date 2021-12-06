using CardService.Application.Commands;
using Domain.Common;
using Domain.Common.Repositories;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaymentGateway.API.Exceptions;
using PaymentGateway.API.Validations.Filters;
using PaymentService.Application.Commands;
using System;
using System.Reflection;
using TransactionService.Application.Commands;

namespace PaymentGateway.API
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
            services.AddHttpClient("PaymentService", c =>
            {
                c.BaseAddress = new Uri(Configuration["PaymentGatewayURL"]);
            });
            services.AddMvc(opt =>
            {
                opt.Filters.Add(typeof(ValidationActionFilter));
            }).AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>());
           
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<ICardRepository, CardRepository>();           
            services.AddMediatR(typeof(CreateTransactionCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(MakePaymentCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateCardDetailsCommand).GetTypeInfo().Assembly);
            services.AddSwaggerGen();
            services.AddDbContext<PaymentGatewayDbContext>(options => options.UseInMemoryDatabase(databaseName: "PaymentGateway"));
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
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            #region Swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaymentGateway.API");
            });
            #endregion
        }
    }
}
