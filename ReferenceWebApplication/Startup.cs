using InterfaceModel.Repositories;
using M2MCommunication.Core;
using M2MCommunication.Services;
using MessageParsing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace ReferenceWebApplication
{
    public class Startup
    {
        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

        public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
#pragma warning disable CA1822
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddLocalization();
            services.Configure<UaLibrarySettings>(Configuration.GetSection("UaLibrary"));
            services.AddSingleton(s => s.GetRequiredService<IOptions<UaLibrarySettings>>().Value);
            services.AddSingleton<ServiceContainerSetup>();
            services.AddSingleton<SubscriptionFactoryService>();
            services.AddSingleton<MessageBusService>();
            services.AddSingleton<ConfigurationService>();
            services.AddTransient<IMessageParser, ImageMessageParser>();
            services.AddTransient<IImageTemplateRepository, ImageTemplateRepository>();
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
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            (app?.ApplicationServices?.GetService(typeof(ServiceContainerSetup)) as ServiceContainerSetup)?.Initialise();
        }
#pragma warning restore CA1822
    }
}
