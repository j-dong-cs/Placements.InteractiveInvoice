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
    public class LineitemController : Controller
    {
        private readonly int PageSize = 50;
        private readonly InteractiveInvoiceContext _context;

        public LineitemController(InteractiveInvoiceContext context)
        {
            _context = context;
        }

        // ~/Lineitem
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            // default sort in ascending order by LineItemName
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["BookedAmtParam"] = sortOrder == "BookedAmount" ? "booked_desc" : "BookedAmount";
            ViewData["ActualAmtParam"] = sortOrder == "ActualAmount" ? "actual_desc" : "ActualAmount";
            ViewData["AdjustParam"] = sortOrder == "Adjustments" ? "adj_desc" : "Adjustments";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var lineitems = from li in _context.Lineitems
                            select li;

            if (!string.IsNullOrEmpty(searchString))
            {
                lineitems = lineitems.Where(li => li.LineitemName.Contains(searchString) || li.CampaignName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    lineitems = lineitems.OrderByDescending(li => li.LineitemName);
                    break;
                case "BookedAmount":
                    lineitems = lineitems.OrderBy(li => li.BookedAmount);
                    break;
                case "booked_desc":
                    lineitems = lineitems.OrderByDescending(li => li.BookedAmount);
                    break;
                case "ActualAmount":
                    lineitems = lineitems.OrderBy(li => li.ActualAmount);
                    break;
                case "actual_desc":
                    lineitems = lineitems.OrderByDescending(li => li.ActualAmount);
                    break;
                case "Adjustments":
                    lineitems = lineitems.OrderBy(li => li.Adjustments);
                    break;
                case "adj_desc":
                    lineitems = lineitems.OrderByDescending(li => li.Adjustments);
                    break;
                default:
                    lineitems = lineitems.OrderBy(li => li.LineitemName);
                    break;
            }

            return View("Index", await LineitemPaginatedList<Lineitem>.CreateAsync(lineitems.AsNoTracking(), pageNumber ?? 1, PageSize));
        }

        // ~/Lineitem/Details/{LineitemID}
        public async Task<IActionResult> Details(int? LineItemID)
        {
            if (LineItemID == null)
            {
                return NotFound();
            }

            var lineitem = await _context.Lineitems
                .Include(item => item.Comments)
                .AsNoTracking()
                .FirstOrDefaultAsync(item => item.LineitemID == LineItemID);

            if (lineitem == null)
            {
                return NotFound();
            }

            return View("Details", lineitem);
        }
    }
}