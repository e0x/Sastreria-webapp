using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SastreriaWebApp.Areas.Identity.Data;
using SastreriaWebApp.Data;

[assembly: HostingStartup(typeof(SastreriaWebApp.Areas.Identity.IdentityHostingStartup))]
namespace SastreriaWebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
            //builder.ConfigureServices((context, services) => {
            //    services.AddDbContext<SastreriaWebAppContext>(options =>
            //        options.UseSqlServer(
            //            context.Configuration.GetConnectionString("SastreriaWebAppIdentityDbContextConnection")));

            //services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<SastreriaWebAppContext>();
        //});
        }
    }
}