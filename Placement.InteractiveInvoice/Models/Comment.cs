using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Placement.InteractiveInvoice.Models
{
    public class Comment
    {
        public int CommentID { get; set; }

        public int LineItemID { get; set; }

        public int UserID { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        // one comment only belongs to one lineitem
        public virtual LineItem LineItem { get; set; }

        // one comment only can be created by one user
        public virtual User User { get; set; }
    }
}
