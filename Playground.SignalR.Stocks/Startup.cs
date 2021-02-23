using MessagePack;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Playground.SignalR.Stocks.Filters;
using Playground.SignalR.Stocks.Hubs;
using Playground.SignalR.Stocks.Models;
using Playground.SignalR.Stocks.Workers;

namespace Playground.SignalR.Stocks
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
            services.AddSingleton<QuoteHub>()
                    .AddSingleton<QuotePriceGenerator>()
                    .AddHostedService<QuoteWorker>()
                    .AddSignalR(options =>
                        options.AddFilter<ConnectionFilter>()
                    )
                    .AddHubOptions<QuoteHub>(options =>
                        options.AddFilter<QuoteHubFilter>()
                    )
                    .AddMessagePackProtocol(options =>
                        options.SerializerOptions = MessagePackSerializerOptions.Standard
                                                                                .WithSecurity(MessagePackSecurity.UntrustedData)
                    );

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<QuoteHub>("/quoteHub");

                endpoints.MapRazorPages();
            });
        }
    }
}
