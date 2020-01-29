using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Placements.InteractiveInvoice.Data;
using Placements.InteractiveInvoice.Models;
using Microsoft.EntityFrameworkCore;
using Placements.InteractiveInvoice.Models.ViewModels;

namespace Placements.InteractiveInvoice.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly int InvoiceListPageSize = 10;
        private readonly int InvoiceDetailsPageSize = 30;
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
            ViewData["DateSortParam"] = string.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewData["IDParam"] = sortOrder == "InvoiceID" ? "ID_desc" : "InvoiceID";
            ViewData["NameParam"] = sortOrder == "InvoiceName" ? "name_desc" : "InvoiceName";
            ViewData["UserParam"] = sortOrder == "UserName" ? "user_desc" : "UserName";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var invoices = _context.Invoices
                            .Include(i => i.InvoiceLineitems)
                                .ThenInclude(i => i.Lineitem)
                            .AsNoTracking();

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
                case "UserName":
                    invoices = invoices.OrderBy(i => i.UserName);
                    break;
                case "user_desc":
                    invoices = invoices.OrderByDescending(i => i.UserName);
                    break;
                default:
                    invoices = invoices.OrderBy(i => i.CreatedDate);
                    break;
            }

            return View("Index", await PaginatedList<Invoice>.CreateAsync(invoices, pageNumber ?? 1, InvoiceListPageSize));
        }

        //~/Invoice/Details/{invoiceID}
        public async Task<IActionResult> Details(int? id, string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            if (id == null)
            {
                return NotFound();
            }

            // default sort in ascending order by LineitemID
            ViewData["CurrentSort"] = sortOrder;
            ViewData["IDSortParam"] = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["NameSortParam"] = sortOrder == "LineitemName" ? "name_desc" : "LineitemName";
            ViewData["BookedAmtParam"] = sortOrder == "BookedAmount" ? "booked_desc" : "BookedAmount";
            ViewData["ActualAmtParam"] = sortOrder == "ActualAmount" ? "actual_desc" : "ActualAmount";
            ViewData["BillableAmtParam"] = sortOrder == "BillableAmount" ? "subtotal_desc" : "BillableAmount";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["invoiceID"] = id;
            ViewData["CurrentFilter"] = searchString;

            // eager loading
            var viewModel = new InvoiceDetailsData();
            viewModel.Invoice = await _context.Invoices
                                .Include(i => i.InvoiceLineitems)
                                    .ThenInclude(i => i.Lineitem)
                                .AsNoTracking()
                                .FirstOrDefaultAsync(i => i.InvoiceID == id);

            var lineitems = viewModel.Invoice.InvoiceLineitems.Select(i => i.Lineitem);

            if (!string.IsNullOrEmpty(searchString)) // search by invoicename
            {
                lineitems = lineitems.Where(i => i.LineitemName.Contains(searchString) || i.CampaignName.Contains(searchString));
            }

            viewModel.groupData = from lineitem in lineitems
                                  group lineitem by lineitem.CampaignName into campaignGroup
                                  select new CampaignGroup()
                                  {
                                    CampaignName = campaignGroup.Key,
                                    Subtotal = campaignGroup.Sum(l => l.BillableAmount)
                                  };
            viewModel.groupData = viewModel.groupData.OrderBy(c => c.CampaignName);

            switch (sortOrder)
            {
                case "id_desc":
                    lineitems = lineitems.OrderByDescending(l => l.LineitemID);
                    break;
                case "LineitemName":
                    lineitems = lineitems.OrderBy(l => l.LineitemName);
                    break;
                case "name_desc":
                    lineitems = lineitems.OrderByDescending(l => l.LineitemName);
                    break;
                case "BookedAmount":
                    lineitems = lineitems.OrderBy(l => l.BookedAmount);
                    break;
                case "booked_desc":
                    lineitems = lineitems.OrderByDescending(l => l.BookedAmount);
                    break;
                case "ActualAmount":
                    lineitems = lineitems.OrderBy(l => l.ActualAmount);
                    break;
                case "actual_desc":
                    lineitems = lineitems.OrderByDescending(l => l.ActualAmount);
                    break;
                case "BillableAmount":
                    lineitems = lineitems.OrderBy(l => l.BillableAmount);
                    break;
                case "subtotal_desc":
                    lineitems = lineitems.OrderByDescending(l => l.BillableAmount);
                    break;
                default:
                    lineitems = lineitems.OrderBy(l => l.LineitemID);
                    break;
            }

            viewModel.lineitems = lineitems;
            viewModel.pagedLineitems = PaginatedList<Lineitem>.Create(lineitems.AsQueryable(), pageNumber ?? 1, InvoiceDetailsPageSize);

            return View("Details", viewModel);
        }

        public void ExportToExcel(int? id)
        {

        }
    }
}