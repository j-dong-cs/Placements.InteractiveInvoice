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
            /*
            context.Database.EnsureCreated();

            context.Campaigns.RemoveRange(context.Campaigns);
            context.Lineitems.RemoveRange(context.Lineitems);
            context.Invoices.RemoveRange(context.Invoices);
            context.InvoiceLineitems.RemoveRange(context.InvoiceLineitems);
            context.Users.RemoveRange(context.Users);
            context.SaveChanges();
            */

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

            context.Users.AddRange(users);
            context.SaveChanges();

            // seed invoice
            var invoiceNames = new string[]
            {
                "Awesome", "Ergonomic", "Fantastic", "Gorgeous", "Incredible", "Intelligent", "Practical", "Sleek", "Small",
                "Computer", "Hat", "Shoes", "Gloves", "Car", "Chair", "Pants", "Shirt", "Table"
            };

            var invoices = new List<Invoice>();
            int year = 2019;
            int month = 10;
            int day = 1;
            int count = 0;
            foreach (string name in invoiceNames)
            {
                if (day > 30)
                {
                    month++;
                    day = 1;
                }
                var invoice = new Invoice { InvoiceName = $"Invoice-{name}", CreatedDate = DateTime.Parse($"{year}-{month}-{day}"), UserID = users[count/5].UserID};
                invoices.Add(invoice);
                day += 5;
                count++;
            }

            context.Invoices.AddRange(invoices);
            context.SaveChanges();

            // seed invoicelineitem
            var invoicelineitems = new List<InvoiceLineitem>();

            foreach (string name in invoiceNames)
            {
                var lineitems = context.Lineitems.Where(l => l.LineitemName.Contains(name));
                foreach (Lineitem lineitem in lineitems)
                {
                    var iljoin = new InvoiceLineitem { InvoiceID=invoices.Single(i => i.InvoiceName == $"Invoice-{name}").InvoiceID, LineitemID=lineitem.LineitemID };
                    invoicelineitems.Add(iljoin);
                }
            }

            context.InvoiceLineitems.AddRange(invoicelineitems);
            context.SaveChanges();
        }
    }
}