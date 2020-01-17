using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Placement.InteractiveInvoice.Models;

namespace Placement.InteractiveInvoice.Data
{
    public static class DbInitializer
    {
       public static void Initialize(InteractiveInvoiceContext context)
       {
            context.Database.EnsureCreated();

            //context.Campaigns.RemoveRange(context.Campaigns);
            //context.LineItems.RemoveRange(context.LineItems);
            //context.SaveChanges();

            // search for any existing LineItems
            if (context.LineItems.Any())
            {
                return; // DB has been seeded
            }

            var jsonString = File.ReadAllText(@"Seed/placements_teaser_data.json");
            var lineItems = JsonSerializer.Deserialize<LineItem[]>(jsonString);

            using (context)
            {
                foreach (LineItem item in lineItems)
                {
                    Campaign campaign;
                    bool IsDuplicate = context.Campaigns.Any(c => c.CampaignID == item.CampaignID);

                    if (!IsDuplicate)
                    {
                        campaign = new Campaign { CampaignID=item.CampaignID, CampaignName = item.CampaignName };
                        context.Campaigns.Add(campaign);
                    }
                    else
                    {
                        campaign = context.Campaigns.Where(c => c.CampaignID == item.CampaignID).Single();
                    }

                    item.Campaign = campaign;

                    context.LineItems.Add(item);
                    context.SaveChanges();
                }
            }
       }
    }
}
