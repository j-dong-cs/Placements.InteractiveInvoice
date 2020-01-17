using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Placement.InteractiveInvoice.Models
{
    public class InvoiceLineItem
    {
        public int LineItemID { get; set; }
        public LineItem LineItem { get; set; }
        public int InvoiceID { get; set; }
        public Invoice Invoice { get; set; }
    }
}
