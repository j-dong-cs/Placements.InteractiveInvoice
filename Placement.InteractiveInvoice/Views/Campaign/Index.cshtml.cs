using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Placement.InteractiveInvoice.Data;
using Placement.InteractiveInvoice.Models;

namespace Placement.InteractiveInvoice.Views.Campaign
{
    public class IndexModel : PageModel
    {
        private readonly Placement.InteractiveInvoice.Data.InteractiveInvoiceContext _context;

        public IndexModel(Placement.InteractiveInvoice.Data.InteractiveInvoiceContext context)
        {
            _context = context;
        }

        public IList<Campaign> Campaign { get;set; }

        public async Task OnGetAsync()
        {
            Campaign = await _context.Campaigns.ToListAsync();
        }
    }
}
