using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Placements.InteractiveInvoice.Models
{
    public class InvoicePaginatedList
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public InvoicePaginatedList()
        {
        }
    }
}
