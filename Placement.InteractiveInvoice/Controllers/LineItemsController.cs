using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Placement.InteractiveInvoice.Data;

namespace Placement.InteractiveInvoice.Controllers
{
    public class LineItemsController : Controller
    {
        private readonly InteractiveInvoiceContext _context;

        public LineItemsController(InteractiveInvoiceContext context)
        {
            _context = context;
        }

        // GET: LineItems
        public ActionResult Index()
        {
            return View();
        }

        // GET: LineItems/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LineItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LineItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LineItems/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LineItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LineItems/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LineItems/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}