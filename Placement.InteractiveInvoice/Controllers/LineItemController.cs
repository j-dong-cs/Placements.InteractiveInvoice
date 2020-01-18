using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Placement.InteractiveInvoice.Data;

namespace Placement.InteractiveInvoice.Controllers
{
    public class LineItemController : Controller
    {
        private readonly InteractiveInvoiceContext _context;

        public LineItemController(InteractiveInvoiceContext context)
        {
            _context = context;
        }

        // ~/LineItem
        public async Task<IActionResult> Index()
        {
            return View("Index", await _context.LineItems.ToListAsync());
        }

        // ~/LineItem/Details/id
        public async Task<IActionResult> Details(int? LineItemID)
        {
            if (LineItemID == null)
            {
                return NotFound();
            }

            var lineitem = await _context.LineItems
                .Include(item => item.Comments)
                .AsNoTracking()
                .FirstOrDefaultAsync(item => item.LineItemID == LineItemID);

            if(lineitem == null)
            {
                return NotFound();
            }

            return View("Details", lineitem);
        }
    }
}