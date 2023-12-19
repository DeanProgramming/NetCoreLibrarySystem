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
                        UserID = SeedUsers.UserOne,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-8).AddDays(8)
                    },
                    // UserTwo took BookID 1 out 3 months ago and has returned it
                    new BookReservations
                    {
                        BookID = 1,
                        UserID = SeedUsers.UserTwo,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-3).AddDays(1)
                    },
                    // UserThree has recently booked BookID 1
                    new BookReservations
                    {
                        BookID = 1,
                        UserID = SeedUsers.UserOne,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(1).AddDays(12)
                    },
                    new BookReservations
                    {
                        BookID = 3,
                        UserID = SeedUsers.UserOne,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-10).AddDays(25)
                    },
                    // UserTwo has recently booked BookID 3
                    new BookReservations
                    {
                        BookID = 3,
                        UserID = SeedUsers.UserOne,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(-1)
                    },
                    // UserThree took BookID 3 out 5 months ago and has returned it
                    new BookReservations
                    {
                        BookID = 3,
                        UserID = SeedUsers.UserThree,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-5).AddDays(2)
                    }, new BookReservations
                    {
                        BookID = 4,
                        UserID = SeedUsers.UserOne,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-7).AddDays(3) // Add 3 days for randomness
                    },
                    // UserTwo has recently booked BookID 4
                    new BookReservations
                    {
                        BookID = 4,
                        UserID = SeedUsers.UserTwo,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(1).AddDays(11) // Add 11 days for randomness
                    },
                    // UserThree took BookID 5 out 9 months ago and has returned it
                    new BookReservations
                    {
                        BookID = 5,
                        UserID = SeedUsers.UserThree,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-9).AddDays(12) // Add 12 days for randomness
                    },
                    // UserFour has recently booked BookID 5
                    new BookReservations
                    {
                        BookID = 5,
                        UserID = SeedUsers.UserFour,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(1).AddDays(12) // Add 12 days for randomness
                    },
                    // UserOne took BookID 7 out 5 months ago and has returned it
                    new BookReservations
                    {
                        BookID = 7,
                        UserID = SeedUsers.UserOne,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-5).AddDays(12) // Add 12 days for randomness
                    },
                    // UserTwo has recently booked BookID 7
                    new BookReservations
                    {
                        BookID = 7,
                        UserID = SeedUsers.UserTwo,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(1).AddDays(23) // Add 23 days for randomness
                    },
                    // UserThree took BookID 9 out 4 months ago and has returned it
                    new BookReservations
                    {
                        BookID = 9,
                        UserID = SeedUsers.UserThree,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-4).AddDays(15) // Add 15 days for randomness
                    },
                    // UserFour has recently booked BookID 9
                    new BookReservations
                    {
                        BookID = 9,
                        UserID = SeedUsers.UserFour,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(1).AddDays(13) // Add 13 days for randomness
                    }, new BookReservations
                    {
                        BookID = 11,
                        UserID = SeedUsers.UserOne,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-3).AddDays(7) // Add 7 days for randomness
                    },
                    // UserTwo has recently booked BookID 11
                    new BookReservations
                    {
                        BookID = 11,
                        UserID = SeedUsers.UserTwo,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(1).AddDays(9) // Add 9 days for randomness
                    },
                    // UserThree took BookID 13 out 6 months ago and has returned it
                    new BookReservations
                    {
                        BookID = 13,
                        UserID = SeedUsers.UserThree,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-6).AddDays(11) // Add 11 days for randomness
                    },
                    // UserFour has recently booked BookID 13
                    new BookReservations
                    {
                        BookID = 13,
                        UserID = SeedUsers.UserFour,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(1).AddDays(14) // Add 14 days for randomness
                    },
                    // UserOne took BookID 16 out 5 months ago and has returned it
                    new BookReservations
                    {
                        BookID = 16,
                        UserID = SeedUsers.UserOne,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-5).AddDays(12) // Add 12 days for randomness
                    },
                    // UserTwo has recently booked BookID 16
                    new BookReservations
                    {
                        BookID = 16,
                        UserID = SeedUsers.UserTwo,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(1).AddDays(11) // Add 11 days for randomness
                    },
                    // UserThree took BookID 19 out 4 months ago and has returned it
                    new BookReservations
                    {
                        BookID = 19,
                        UserID = SeedUsers.UserThree,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-4).AddDays(19) // Add 19 days for randomness
                    },
                    // UserFour has recently booked BookID 19
                    new BookReservations
                    {
                        BookID = 19,
                        UserID = SeedUsers.UserFour,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(1).AddDays(16) // Add 16 days for randomness
                    },
                    // UserOne took BookID 27 out 4 months ago and has returned it
                    new BookReservations
                    {
                        BookID = 27,
                        UserID = SeedUsers.UserOne,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-4).AddDays(4)
                    },
                    // UserTwo has recently booked BookID 27
                    new BookReservations
                    {
                        BookID = 27,
                        UserID = SeedUsers.UserTwo,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(1).AddDays(12)
                    },
                    // UserThree took BookID 29 out 7 months ago and has returned it
                    new BookReservations
                    {
                        BookID = 29,
                        UserID = SeedUsers.UserThree,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-7).AddDays(11)
                    },
                    // UserFour has recently booked BookID 29
                    new BookReservations
                    {
                        BookID = 29,
                        UserID = SeedUsers.UserOne,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(1).AddDays(9)
                    },
                    // UserOne took BookID 32 out 6 months ago and has returned it
                    new BookReservations
                    {
                        BookID = 32,
                        UserID = SeedUsers.UserOne,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-6).AddDays(17)
                    },
                    // UserTwo has recently booked BookID 32
                    new BookReservations
                    {
                        BookID = 32,
                        UserID = SeedUsers.UserTwo,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(1).AddDays(12)
                    },
                    // UserThree took BookID 35 out 5 months ago and has returned it
                    new BookReservations
                    {
                        BookID = 35,
                        UserID = SeedUsers.UserThree,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-5).AddDays(2)
                    },
                    // UserFour has recently booked BookID 35
                    new BookReservations
                    {
                        BookID = 35,
                        UserID = SeedUsers.UserOne,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(-1)
                    }
                );

                context.SaveChanges();
            }
        }
    }
}