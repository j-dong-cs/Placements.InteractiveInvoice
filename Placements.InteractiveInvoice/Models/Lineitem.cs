using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Placements.InteractiveInvoice.Models
{
    public class Lineitem
    {
        [JsonPropertyName("id")]
        public int LineitemID { get; set; }

        [JsonPropertyName("campaign_id")]
        public int CampaignID { get; set; }

        [JsonPropertyName("campaign_name")]
        public string CampaignName { get; set; }

        [Required]
        [JsonPropertyName("line_item_name")]
        public string LineitemName { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [JsonPropertyName("booked_amount")]
        public decimal BookedAmount { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [JsonPropertyName("actual_amount")]
        public decimal ActualAmount { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [JsonPropertyName("adjustments")]
        public decimal Adjustments { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Billable Amount")]
        public decimal BillableAmount
        {
            get
            {
                return ActualAmount + Adjustments;
            }
        }

        // one lineitem only belong to one campaign
        public virtual Campaign Campaign { get; set; }

        // one lineitem has many comments
        public virtual ICollection<Comment> Comments { get; set; }

        // one lineitem exist in many invoices
        public virtual ICollection<InvoiceLineitem> InvoiceLineitems { get; set; }
    }
}