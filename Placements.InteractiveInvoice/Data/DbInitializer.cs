using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Placements.InteractiveInvoice.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Placements.InteractiveInvoice.Data
{
    public static class DbInitializer
    {
        public static void Initialize(InteractiveInvoiceContext context)
        {
            //context.Database.EnsureCreated();

            //context.Campaigns.RemoveRange(context.Campaigns);
            //context.Lineitems.RemoveRange(context.Lineitems);
            //context.SaveChanges();

            // Seed Campaign and Lineitem from JSON file
            if (context.Lineitems.Any()) // search for existing items
            {
                return;
            }

            var jsonString = File.ReadAllText(@"Data/placements_teaser_data.json");
            var lineItems = JsonSerializer.Deserialize<Lineitem[]>(jsonString);

            var campaignDict = new Dictionary<int, Campaign>();

            foreach (Lineitem item in lineItems)
            {
                if (!campaignDict.ContainsKey(item.CampaignID))
                {
                  campaignDict.Add(item.CampaignID, new Campaign { CampaignID = item.CampaignID, CampaignName = item.CampaignName });
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

            // seed user
            var users = new User[]
            {
                new User { UserName = "JohnSmith", Password="password"},
                new User { UserName = "JingjingDong", Password="password"},
                new User { UserName = "JoeHanks", Password="password"},
                new User { UserName = "JacksonWang", Password="password"},
                new User { UserName = "AliceLee", Password="password"}
            };

            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

            // seed invoice
            var invoices = new Invoice[]
            {
                new Invoice { InvoiceName = "InvoiceAwesome",CreatedDate = DateTime.Parse("2020-01-15"), UserID = users.Single(u => u.UserName == "JingjingDong").UserID},
                new Invoice { InvoiceName = "InvoiceErgonomicConcrete", CreatedDate = DateTime.Parse("2020-01-21"), UserID = users.Single(u => u.UserName == "JingjingDong").UserID},
                new Invoice { InvoiceName = "InvoiceforHat", CreatedDate = DateTime.Parse("2019-12-30"), UserID = users.Single(u => u.UserName == "JacksonWang").UserID},
                new Invoice { InvoiceName = "InvoiceforCar", CreatedDate = DateTime.Parse("2019-12-20"), UserID = users.Single(u => u.UserName == "JacksonWang").UserID},
                new Invoice { InvoiceName = "InvoiceforShoe", CreatedDate = System.DateTime.Now, UserID = users.Single(u => u.UserName == "JohnSmith").UserID}
            };

            foreach (Invoice i in invoices)
            {
                context.Invoices.Add(i);
            }
            context.SaveChanges();

            // seed invoicelineitem
            var invoicelineitems = new InvoiceLineitem[]
            {
                new InvoiceLineitem
                {
                    InvoiceID = invoices.Single(i => i.InvoiceName == "InvoiceAwesome").InvoiceID,
                    LineitemID = lineItems.Single(l => l.LineitemName == "Awesome Concrete Computer - 019f").LineitemID
                },
                new InvoiceLineitem
                {
                    InvoiceID = invoices.Single(i => i.InvoiceName == "InvoiceAwesome").InvoiceID,
                    LineitemID = lineItems.Single(l => l.LineitemName == "Awesome Concrete Computer - 253e").LineitemID
                },
                new InvoiceLineitem
                {
                    InvoiceID = invoices.Single(i => i.InvoiceName == "InvoiceAwesome").InvoiceID,
                    LineitemID = lineItems.Single(l => l.LineitemName == "Awesome Concrete Computer - 2c42").LineitemID
                },
                new InvoiceLineitem
                {
                    InvoiceID = invoices.Single(i => i.InvoiceName == "InvoiceAwesome").InvoiceID,
                    LineitemID = lineItems.Single(l => l.LineitemName == "Awesome Concrete Car - 07ef").LineitemID
                },
                new InvoiceLineitem
                {
                    InvoiceID = invoices.Single(i => i.InvoiceName == "InvoiceAwesome").InvoiceID,
                    LineitemID = lineItems.Single(l => l.LineitemName == "Awesome Concrete Car - 13fa").LineitemID
                },
                new InvoiceLineitem
                {
                    InvoiceID = invoices.Single(i => i.InvoiceName == "InvoiceAwesome").InvoiceID,
                    LineitemID = lineItems.Single(l => l.LineitemName == "Awesome Concrete Car - 7048").LineitemID
                },
                new InvoiceLineitem
                {
                    InvoiceID = invoices.Single(i => i.InvoiceName == "InvoiceErgonomicConcrete").InvoiceID,
                    LineitemID = lineItems.Single(l => l.LineitemName == "Ergonomic Concrete Car - 66ed").LineitemID
                },
                new InvoiceLineitem
                {
                    InvoiceID = invoices.Single(i => i.InvoiceName == "InvoiceErgonomicConcrete").InvoiceID,
                    LineitemID = lineItems.Single(l => l.LineitemName == "Ergonomic Concrete Car - 77df").LineitemID
                }
            };

            foreach (InvoiceLineitem il in invoicelineitems)
            {
                context.InvoiceLineitems.Add(il);
            }
            context.SaveChanges();
        }
    }
}