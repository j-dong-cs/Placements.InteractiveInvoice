using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Placements.InteractiveInvoice.Models;
using Microsoft.EntityFrameworkCore;

namespace Placements.InteractiveInvoice.Data
{
    public static class DbInitializer
    {
        public static void Initialize(InteractiveInvoiceContext context)
        {
            context.Database.EnsureCreated();

            //context.Campaigns.RemoveRange(context.Campaigns);
            //context.Lineitems.RemoveRange(context.Lineitems);
            //context.SaveChanges();

            // search for any existing LineItems
            if (context.Lineitems.Any() && context.Campaigns.Any())
            {
                return; // DB has been seeded
            }

            var jsonString = File.ReadAllText(@"Data/placements_teaser_data.json");
            var lineItems = JsonSerializer.Deserialize<Lineitem[]>(jsonString);

            var campaignDict = new Dictionary<int, Campaign>();

            foreach (Lineitem item in lineItems)
            {
                if (!campaignDict.ContainsKey(item.CampaignID))
                {
                    campaignDict.Add(item.CampaignID, new Campaign { CampaignID=item.CampaignID, CampaignName=item.CampaignName});
                }
            }

            context.Database.OpenConnection();
            try
            {
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Campaigns ON");
                foreach (var campaign in campaignDict.Values)
                {
                    context.Campaigns.Add(campaign);
                }
                context.SaveChanges();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Campaigns OFF");

                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Lineitems ON");
                foreach (Lineitem item in lineItems)
                {
                    item.Campaign = context.Campaigns.Where(c => c.CampaignID == item.CampaignID).Single();
                    context.Lineitems.Add(item);
                }
                context.SaveChanges();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Lineitems OFF");
            }
            finally
            {
                context.Database.CloseConnection();
            }
        }
    }
}