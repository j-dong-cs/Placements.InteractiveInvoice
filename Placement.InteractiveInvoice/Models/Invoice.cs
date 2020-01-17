using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Placement.InteractiveInvoice.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set;}

        [DataType(DataType.DateTime)]
        [DisplayName("Created Date")]
        [DisplayFormat(DataFormatString = "{0:G}")]
        public DateTime CreatedDate { get; set; }

        public int UserID { get; set; }

        // one invoice has many lineitems
        public virtual ICollection<InvoiceLineItem> InvoiceLineItems { get; set; }

        // one invoice can only be created by one user
        public virtual User User { get; set; }
    }
}
