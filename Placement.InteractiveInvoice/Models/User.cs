using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Placement.InteractiveInvoice.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        // one user can create many comments
        public virtual ICollection<Comment> Comments { get; set; }

        // one user can create many invoices
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
