using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Placements.InteractiveInvoice.Data;
using Placements.InteractiveInvoice.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;

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

            return View("Index", await PaginatedList<Lineitem>.CreateAsync(lineitems.AsNoTracking(), pageNumber ?? 1, PageSize));
        }

        // ~/Lineitem/Details/{id}
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = new Comment { LineitemID = id.Value };

            return View("Details", comment);
        }

        // ~/Lineitem/Details/{id}
        [HttpPost]
        public async Task<IActionResult> AddComment(int id, [Bind("Body")] Comment comment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    comment.LineitemID = id;
                    comment.UserName = "Demo";
                    comment.CreatedDate = DateTime.Now;
                    _context.Comments.Add(comment);
                    await _context.SaveChangesAsync();

                    return View("Details", comment);
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return View("Details", comment);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineitem = await _context.Lineitems
                            .Include(l => l.InvoiceLineitems)
                                .ThenInclude(l => l.Invoice)
                            .FirstOrDefaultAsync(l => l.LineitemID == id);

            if (lineitem == null)
            {
                return NotFound();
            }

            return View(lineitem);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, int? invoiceID)
        {
            ViewData["invoiceID"] = invoiceID;

            if (id == null)
            {
                return NotFound();
            }

            var lineitemToUpdate = await _context.Lineitems
                                    .Include(l => l.InvoiceLineitems)
                                        .ThenInclude(l => l.Invoice)
                                    .FirstOrDefaultAsync(l => l.LineitemID == id);

            if (await TryUpdateModelAsync<Lineitem>(lineitemToUpdate, "", l => l.Adjustments))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    // log error
                    ModelState.AddModelError("","Unable o save changes to edit lineitem adjustments. Try again, if the problem persists, see system administrator.");
                }

                return RedirectToAction(nameof(Details), nameof(Invoice), new { id=invoiceID });
            }

            return View(lineitemToUpdate);
        }
    }
}