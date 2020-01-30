using System;
using Placements.InteractiveInvoice.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Placements.InteractiveInvoice.Models;

namespace Placements.InteractiveInvoice.ViewComponents
{
    public class CommentListViewComponent: ViewComponent
    {
        private readonly InteractiveInvoiceContext _context;

        public CommentListViewComponent(InteractiveInvoiceContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int lineitemID)
        {
            var lineitem = await _context.Lineitems
                            .Include(l => l.Comments)
                            .AsNoTracking()
                            .SingleOrDefaultAsync(l => l.LineitemID == lineitemID);
            return View(lineitem);
        }
    }
}
