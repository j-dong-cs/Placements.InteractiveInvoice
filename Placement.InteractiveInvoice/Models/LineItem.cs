using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Placement.InteractiveInvoice.Models
{
    public class LineItem
    {
        [JsonPropertyName("id")]
        public int LineItemID { get; set; }

        [JsonPropertyName("campaign_id")]
        public int CampaignID { get; set; }

        [JsonPropertyName("campaign_name")]
        public string CampaignName { get; set; }

        [Required]
        [JsonPropertyName("line_item_name")]
        public string LineItemName { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [JsonPropertyName("booked_amount")]
        public double BookedAmount { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [JsonPropertyName("actual_amount")]
        public double ActualAmount { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [JsonPropertyName("adjustments")]
        public double Adjustments { get; set; }

        // one lineitem only belong to one campaign
        public virtual Campaign Campaign { get; set; }

        // one lineitem has many comments
        public virtual ICollection<Comment> Comments { get; set; }

        // one lineitem exist in many invoices
        public virtual ICollection<InvoiceLineItem> InvoiceLineItems { get; set; }
    }
}
