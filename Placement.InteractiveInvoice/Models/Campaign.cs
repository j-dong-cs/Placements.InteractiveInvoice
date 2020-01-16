using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Placement.InteractiveInvoice.Models
{
    public class Campaign
    {
        [JsonPropertyName("campaign_id")]
        public int CampaignID { get; set; }

        [Required]
        [JsonPropertyName("campaign_name")]
        public string CampaignName { get; set; }

        // one campaign has many lineitems
        public virtual ICollection<LineItem> LineItems { get; set; }
    }
}
