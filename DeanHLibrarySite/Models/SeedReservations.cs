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

                int BookId(string seedKey)
                {
                    var id = context.BookTable
                        .Where(b => b.Title == seedKey)
                        .Select(b => b.Id)
                        .SingleOrDefault();

                    if (id == 0)
                        throw new InvalidOperationException($"SeedReservations: No Book found with SeedKey='{seedKey}'");

                    return id;
                }
                context.BookReservations.AddRange(
                    new BookReservations
                    {
                        BookID = BookId("To Kill a Mockingbird"),
                        UserID = SeedUsers.UserOne,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-8).AddDays(8)
                    },
                    new BookReservations
                    {
                        BookID = BookId("To Kill a Mockingbird"),
                        UserID = SeedUsers.UserTwo,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-3).AddDays(1)
                    },
                    new BookReservations
                    {
                        BookID = BookId("To Kill a Mockingbird"),
                        UserID = SeedUsers.UserOne,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(1).AddDays(12)
                    },

                    new BookReservations
                    {
                        BookID = BookId("The Great Gatsby"),
                        UserID = SeedUsers.UserOne,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-10).AddDays(25)
                    },
                    new BookReservations
                    {
                        BookID = BookId("The Great Gatsby"),
                        UserID = SeedUsers.UserOne,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(-1)
                    },
                    new BookReservations
                    {
                        BookID = BookId("The Great Gatsby"),
                        UserID = SeedUsers.UserThree,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-5).AddDays(2)
                    },

                    new BookReservations
                    {
                        BookID = BookId("Harry Potter and the Sorcerer's Stone"),
                        UserID = SeedUsers.UserOne,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-7).AddDays(3)
                    },
                    new BookReservations
                    {
                        BookID = BookId("Harry Potter and the Sorcerer's Stone"),
                        UserID = SeedUsers.UserTwo,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(1).AddDays(11)
                    },

                    new BookReservations
                    {
                        BookID = BookId("Pride and Prejudice"),
                        UserID = SeedUsers.UserThree,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-9).AddDays(12)
                    },
                    new BookReservations
                    {
                        BookID = BookId("Pride and Prejudice"),
                        UserID = SeedUsers.UserFour,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(1).AddDays(12)
                    },

                    new BookReservations
                    {
                        BookID = BookId("The Lord of the Rings"),
                        UserID = SeedUsers.UserOne,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-5).AddDays(12)
                    },
                    new BookReservations
                    {
                        BookID = BookId("The Lord of the Rings"),
                        UserID = SeedUsers.UserTwo,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(1).AddDays(23)
                    },

                    new BookReservations
                    {
                        BookID = BookId("The Alchemist"),
                        UserID = SeedUsers.UserThree,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-4).AddDays(15)
                    },
                    new BookReservations
                    {
                        BookID = BookId("The Alchemist"),
                        UserID = SeedUsers.UserFour,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(1).AddDays(13)
                    },

                    new BookReservations
                    {
                        BookID = BookId("The Catcher in the Rye"),
                        UserID = SeedUsers.UserOne,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-3).AddDays(7)
                    },
                    new BookReservations
                    {
                        BookID = BookId("The Catcher in the Rye"),
                        UserID = SeedUsers.UserTwo,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(1).AddDays(9)
                    },

                    new BookReservations
                    {
                        BookID = BookId("The Shining"),
                        UserID = SeedUsers.UserThree,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-6).AddDays(11)
                    },
                    new BookReservations
                    {
                        BookID = BookId("The Shining"),
                        UserID = SeedUsers.UserFour,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(1).AddDays(14)
                    },

                    new BookReservations
                    {
                        BookID = BookId("The Girl with the Dragon Tattoo"),
                        UserID = SeedUsers.UserOne,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-5).AddDays(12)
                    },
                    new BookReservations
                    {
                        BookID = BookId("The Girl with the Dragon Tattoo"),
                        UserID = SeedUsers.UserTwo,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(1).AddDays(11)
                    },

                    new BookReservations
                    {
                        BookID = BookId("The Chronicles of Narnia"),
                        UserID = SeedUsers.UserThree,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-4).AddDays(19)
                    },
                    new BookReservations
                    {
                        BookID = BookId("The Chronicles of Narnia"),
                        UserID = SeedUsers.UserFour,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(1).AddDays(16)
                    },

                    new BookReservations
                    {
                        BookID = BookId("The Shawshank Redemption"),
                        UserID = SeedUsers.UserOne,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-4).AddDays(4)
                    },
                    new BookReservations
                    {
                        BookID = BookId("The Shawshank Redemption"),
                        UserID = SeedUsers.UserTwo,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(1).AddDays(12)
                    },

                    new BookReservations
                    {
                        BookID = BookId("Pulp Fiction"),
                        UserID = SeedUsers.UserThree,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-7).AddDays(11)
                    },
                    new BookReservations
                    {
                        BookID = BookId("Pulp Fiction"),
                        UserID = SeedUsers.UserOne,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(1).AddDays(9)
                    },

                    new BookReservations
                    {
                        BookID = BookId("Schindler's List"),
                        UserID = SeedUsers.UserOne,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-6).AddDays(17)
                    },
                    new BookReservations
                    {
                        BookID = BookId("Schindler's List"),
                        UserID = SeedUsers.UserTwo,
                        Booked = true,
                        ReturnDate = DateTime.Now.AddMonths(1).AddDays(12)
                    },

                    new BookReservations
                    {
                        BookID = BookId("Thriller"),
                        UserID = SeedUsers.UserThree,
                        Booked = false,
                        ReturnDate = DateTime.Now.AddMonths(-5).AddDays(2)
                    },
                    new BookReservations
                    {
                        BookID = BookId("Thriller"),
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