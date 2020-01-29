using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Placements.InteractiveInvoice.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }

        public string InvoiceName { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Created Date")]
        [DisplayFormat(DataFormatString = "{0:G}")]
        public DateTime CreatedDate { get; set; }

        public string UserName { get; set; }

        // one invoice has many lineitems
        public virtual ICollection<InvoiceLineitem> InvoiceLineitems { get; set; }
    }
}