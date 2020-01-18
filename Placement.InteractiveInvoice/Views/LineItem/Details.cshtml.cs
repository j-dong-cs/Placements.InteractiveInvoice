using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Placement.InteractiveInvoice.Data;
using Placement.InteractiveInvoice.Models;

namespace Placement.InteractiveInvoice.Views.LineItem
{
    public class DetailsModel : PageModel
    {
        private readonly Placement.InteractiveInvoice.Data.InteractiveInvoiceContext _context;

        public DetailsModel(Placement.InteractiveInvoice.Data.InteractiveInvoiceContext context)
        {
            _context = context;
        }

        public Comment Comment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Comment = await _context.Comments
                .Include(c => c.LineItem)
                .Include(c => c.User).FirstOrDefaultAsync(m => m.CommentID == id);

            if (Comment == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
