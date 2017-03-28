using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModernStore.Infra.Contexts;
using ModernStore.Infra.Transactions;
using ModernStore.Domain.Repositories;
using ModernStore.Infra.Repositories;
using ModernStore.Domain.Commands.Handlers;
using ModernStore.Domain.Services;
using ModernStore.Infra.Services;

namespace ModernStore.Api
{
    public class Startup
    {        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors();
            
            services.AddScoped(_ => new ModernStoreDataContext("Data Source=ALAN-PC;Initial Catalog=ModernStoreData;Integrated Security=True"));

            services.AddTransient<IUow, Uow>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddTransient<CustomerCommandHandler, CustomerCommandHandler>();
            services.AddTransient<OrderCommandHandler, OrderCommandHandler>();

            services.AddTransient<IEmailService, EmailService>();
        }
                
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors(x => {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            app.UseMvc();
        }
    }
}
