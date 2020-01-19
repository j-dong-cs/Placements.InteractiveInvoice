using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Placement.InteractiveInvoice.Data;

namespace Placement.InteractiveInvoice.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly InteractiveInvoiceContext _context;

        public InvoiceController(InteractiveInvoiceContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Filter data and affact grand total

            // Show billable amount = actual amount + adjustments

            // Show the invoice grand total (sum of all lineitems' billable amount)

            // browse/filter/sort invoice history

            return View(await _context.Invoices.ToListAsync());
        }

        public async Task<IActionResult> Edit(int? LineItemID)
        {
            // Edit lineitem adjustments
            return View();
        }
    }
}