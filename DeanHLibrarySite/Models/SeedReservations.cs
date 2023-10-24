using DeanHLibrarySite.Data;
using DeanHLibrarySite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DeanHLibrarySite.Models
{
    public static class SeedReservations
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DeanHLibrarySiteContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DeanHLibrarySiteContext>>()))
            {
                if (context == null || context.BookReservations == null)
                {
                    throw new ArgumentNullException("Null RazorPagesMovieContext");
                }


                if (context.BookReservations.Any())
                {
                    return;   // DB has been seeded
                }

                context.BookReservations.AddRange(
                new BookReservations
                {
                    BookID = 1,
                    UserID = 4,
                    Booked = true,
                    ReturnDate = new DateTime(2024, 5, 2)
                },
                new BookReservations
                {
                    BookID = 2,
                    UserID = 4,
                    Booked = false,
                    ReturnDate = new DateTime(2022, 5, 2)
                },
                new BookReservations
                {
                    BookID = 3,
                    UserID = 4,
                    Booked = true,
                    ReturnDate = new DateTime(2021, 5, 2)
                }
                );
                context.SaveChanges();
            }
        }
    }
}