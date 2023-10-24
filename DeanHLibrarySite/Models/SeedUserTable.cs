using DeanHLibrarySite.Data;
using DeanHLibrarySite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DeanHLibrarySite.Models
{
    public static class SeedUserTable
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DeanHLibrarySiteContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DeanHLibrarySiteContext>>()))
            {
                if (context == null || context.UserTable == null)
                {
                    throw new ArgumentNullException("Null RazorPagesMovieContext");
}


                if (context.UserTable.Any())
                {
                    return;   // DB has been seeded
                }

                context.UserTable.AddRange(
                new UserTable
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    UserName = "Admin",
                    Password = "Admin",
                    HasAdministrator = true,
                },
                new UserTable
                {
                    FirstName = "Dean",
                    LastName = "Holland",
                    UserName = "Dean",
                    Password = "Holland",
                    HasAdministrator = false,
                }
                );
                context.SaveChanges();
            }
        }
    }
}