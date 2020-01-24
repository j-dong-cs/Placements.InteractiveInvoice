using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Placements.InteractiveInvoice.Data;
using Placements.InteractiveInvoice.Models;
using Microsoft.EntityFrameworkCore;

namespace Placements.InteractiveInvoice.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly InteractiveInvoiceContext _context;

        public InvoiceController(InteractiveInvoiceContext context)
        {
            _context = context;
        }

        // ~/Invoice
        public async Task<IActionResult> Index()
        {
            return View("Index", await _context.Invoices.ToListAsync());
        }
    }
}