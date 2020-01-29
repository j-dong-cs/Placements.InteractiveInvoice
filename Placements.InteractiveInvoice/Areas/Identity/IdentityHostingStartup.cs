using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Placements.InteractiveInvoice.Areas.Identity.Data;

[assembly: HostingStartup(typeof(Placements.InteractiveInvoice.Areas.Identity.IdentityHostingStartup))]
namespace Placements.InteractiveInvoice.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                var connBuilder = new SqlConnectionStringBuilder(
                    context.Configuration.GetConnectionString("InteractiveInvoiceAuthConnection"));

                services.AddDbContext<InteractiveInvoiceAuth>(options =>
                    options.UseSqlServer(connBuilder.ConnectionString));

                services.AddDefaultIdentity<IdentityUser>()
                        .AddEntityFrameworkStores<InteractiveInvoiceAuth>()
                        .AddDefaultUI();
            });
        }
    }
}