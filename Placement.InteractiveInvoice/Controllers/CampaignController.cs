using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Placement.InteractiveInvoice.Data;

namespace Placement.InteractiveInvoice.Controllers
{
    public class CampaignController : Controller
    {
        private readonly InteractiveInvoiceContext _context;

        public CampaignController(InteractiveInvoiceContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View("Index", _context.Campaigns.ToList());
        }
    }
}