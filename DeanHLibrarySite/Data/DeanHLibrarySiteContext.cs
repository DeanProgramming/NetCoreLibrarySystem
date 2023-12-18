using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DeanHLibrarySite.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DeanHLibrarySite.Pages;

namespace DeanHLibrarySite.Data
{
    public class DeanHLibrarySiteContext : IdentityDbContext<ApplicationUser>
    {
        public DeanHLibrarySiteContext(DbContextOptions<DeanHLibrarySiteContext> options)
            : base(options)
        {
        }

        public DbSet<DeanHLibrarySite.Models.BookTable> BookTable { get; set; } = default!;
        public DbSet<DeanHLibrarySite.Models.BookReservations> BookReservations { get; set; } = default!;
    }
}