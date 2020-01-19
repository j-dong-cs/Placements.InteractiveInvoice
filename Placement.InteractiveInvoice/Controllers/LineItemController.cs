using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Placement.InteractiveInvoice.Data;
using Placement.InteractiveInvoice.Models;

namespace Placement.InteractiveInvoice.Controllers
{
    public class LineItemController : Controller
    {
        private readonly int PageSize = 50;
        private readonly InteractiveInvoiceContext _context;

        public LineItemController(InteractiveInvoiceContext context)
        {
            _context = context;
        }

        // ~/LineItem
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

            var lineitems = from li in _context.LineItems
                            select li;

            if (!string.IsNullOrEmpty(searchString))
            {
                lineitems = lineitems.Where(li => li.LineItemName.Contains(searchString) || li.CampaignName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    lineitems = lineitems.OrderByDescending(li => li.LineItemName);
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
                    lineitems = lineitems.OrderBy(li => li.LineItemName);
                    break;
            }

            return View("Index", await LineItemPaginatedList<LineItem>.CreateAsync(lineitems.AsNoTracking(), pageNumber ?? 1, PageSize));
        }

        // ~/LineItem/Details/{LineItemID}
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