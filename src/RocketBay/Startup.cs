using RocketBay.Components;
using ElectronNET.API;
using Microsoft.Extensions.Logging;

namespace RocketBay
{
    class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddLogging();
            services.AddAuthentication();
            services.AddAuthorization();

            services.AddElectron();
            services.AddRazorComponents()
                .AddInteractiveServerComponents();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();
            app.UseAntiforgery();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorComponents<Components.App>()
                    .AddInteractiveServerRenderMode();
            });


            if (HybridSupport.IsElectronActive)
            {
                Electron.App.Ready += () => _ = CreateWindow();
            }
        }

        private async Task<BrowserWindow> CreateWindow()
        {
            var primaryWindow = await Electron.WindowManager.CreateWindowAsync();
            primaryWindow.SetMenuBarVisibility(false);


            primaryWindow.OnClosed += () =>
            {
                Electron.App.Quit();
            };

            return primaryWindow;
        }
    }
}