using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DeanHLibrarySite.Models;

namespace DeanHLibrarySite.Data
{
    public class DeanHLibrarySiteContext : DbContext
    {
        public DeanHLibrarySiteContext (DbContextOptions<DeanHLibrarySiteContext> options)
            : base(options)
        {
        }

        public DbSet<DeanHLibrarySite.Models.BookTable> BookTable { get; set; } = default!;
    }
}
