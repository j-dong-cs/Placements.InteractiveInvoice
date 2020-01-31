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
        [DataType(DataType.Currency)]
        [JsonPropertyName("booked_amount")]
        public decimal BookedAmount { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        [JsonPropertyName("actual_amount")]
        public decimal ActualAmount { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        [JsonPropertyName("adjustments")]
        public decimal Adjustments { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        [Display(Name = "Billable Amount")]
        public decimal BillableAmount
        {
            get
            {
                return ActualAmount + Adjustments;
            }
        }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        // one lineitem only belong to one campaign
        public virtual Campaign Campaign { get; set; }

        // one lineitem has many comments
        public virtual ICollection<Comment> Comments { get; set; }

        // one lineitem exist in many invoices
        public virtual ICollection<InvoiceLineitem> InvoiceLineitems { get; set; }
    }
}