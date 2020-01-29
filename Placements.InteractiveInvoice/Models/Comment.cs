using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Placements.InteractiveInvoice.Models
{
    public class Comment
    {
        public int CommentID { get; set; }

        public int LineitemID { get; set; }

        public string UserName { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Created Date")]
        [DisplayFormat(DataFormatString = "{0:G}")]
        public DateTime CreatedDate { get; set; }

        // one comment only belongs to one lineitem
        public virtual Lineitem Lineitem { get; set; }
    }
}