using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Placements.InteractiveInvoice.Models
{
    public class InvoiceLineitem
    {
        public int LineitemID { get; set; }
        public Lineitem Lineitem { get; set; }
        public int InvoiceID { get; set; }
        public Invoice Invoice { get; set; }
    }
}