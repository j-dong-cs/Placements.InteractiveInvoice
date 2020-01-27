using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Placements.InteractiveInvoice.Models.ViewModels
{
    public class InvoiceDetailsData
    {
        public Invoice Invoice { get; set; }

        public IEnumerable<Lineitem> lineitems { get; set; }

        public PaginatedList<Lineitem> pagedLineitems{ get; set; }

        public IEnumerable<CampaignGroup> groupData { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal GrandTotal
        {
            get
            {
                return lineitems.Sum(l => l.BillableAmount);
            }
        }
    }
}
