using System;
using System.ComponentModel.DataAnnotations;

namespace Placements.InteractiveInvoice.Models.ViewModels
{
    public class CampaignGroup
    {
        public string CampaignName { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Subtotal { get; set; }
    }
}
