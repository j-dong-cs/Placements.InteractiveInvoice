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
        private readonly int PageSize = 10;
        private readonly InteractiveInvoiceContext _context;

        public InvoiceController(InteractiveInvoiceContext context)
        {
            _context = context;
        }

        // ~/Invoice
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            // default sort in ascending order by Invoice CreatedDate
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DateSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["IDParam"] = sortOrder == "InvoiceID" ? "ID_desc" : "InvoiceID";
            ViewData["NameParam"] = sortOrder == "InvoiceName" ? "name_desc" : "InvoiceName";
            ViewData["UserParam"] = sortOrder == "UserID" ? "user_desc" : "UserID";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var invoices = from invoice in _context.Invoices
                           select invoice;

            if (!string.IsNullOrEmpty(searchString)) // search by invoicename
            {
                invoices = invoices.Where(i => i.InvoiceName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "date_desc":
                    invoices = invoices.OrderByDescending(i => i.CreatedDate);
                    break;
                case "InvoiceID":
                    invoices = invoices.OrderBy(i => i.InvoiceID);
                    break;
                case "ID_desc":
                    invoices = invoices.OrderByDescending(i => i.InvoiceID);
                    break;
                case "InvoiceName":
                    invoices = invoices.OrderBy(i => i.InvoiceName);
                    break;
                case "name_desc":
                    invoices = invoices.OrderByDescending(i => i.InvoiceName);
                    break;
                case "UserID":
                    invoices = invoices.OrderBy(i => i.UserID);
                    break;
                case "user_desc":
                    invoices = invoices.OrderByDescending(i => i.UserID);
                    break;
                default:
                    invoices = invoices.OrderBy(i => i.CreatedDate);
                    break;
            }

            invoices = invoices
                .Include(i => i.User)
                .Include(i => i.InvoiceLineitems)
                    .ThenInclude(i => i.Lineitem)
                .AsNoTracking();

            return View("Index", await PaginatedList<Invoice>.CreateAsync(invoices, pageNumber ?? 1, PageSize));
        }
    }
}