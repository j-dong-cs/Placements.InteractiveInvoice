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
    public class CommentController : Controller
    {
        private readonly InteractiveInvoiceContext _context;

        public CommentController(InteractiveInvoiceContext context)
        {
            _context = context;
        }

        // GET: ~/Invoice/Create
        public IActionResult Create()
        {
            return View();
        }
    }
}