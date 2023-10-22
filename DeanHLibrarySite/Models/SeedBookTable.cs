using Microsoft.EntityFrameworkCore;
using DeanHLibrarySite.Data;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DeanHLibrarySite.Models
{
    public static class SeedBookTable
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DeanHLibrarySiteContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DeanHLibrarySiteContext>>()))
            {
                if (context == null || context.BookTable == null)
                {
                    throw new ArgumentNullException("Null RazorPagesMovieContext");
                }


                if (context.BookTable.Any())
                {
                    return;   // DB has been seeded
                }

                context.BookTable.AddRange( 
                new BookTable
                {
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    Genre = "Fiction",
                    PublicationYear = DateTime.Parse("1960-07-11"),
                    Type = BookTable.BookType.Book,
                    Description = "A classic novel set in the American South that addresses issues of racism and morality.",
                },
                new BookTable
                {
                    Title = "1984",
                    Author = "George Orwell",
                    Genre = "Dystopian Fiction",
                    PublicationYear = DateTime.Parse("1949-06-08"),
                    Type = BookTable.BookType.Book,
                    Description = "A dystopian novel that explores themes of totalitarianism and surveillance.",
                },
                new BookTable
                {
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    Genre = "Classic Fiction",
                    PublicationYear = DateTime.Parse("1925-04-10"),
                    Type = BookTable.BookType.Book,
                    Description = "A story of wealth, love, and the American Dream during the Roaring Twenties.",
                },
                new BookTable
                {
                    Title = "Harry Potter and the Sorcerer's Stone",
                    Author = "J.K. Rowling",
                    Genre = "Fantasy",
                    PublicationYear = DateTime.Parse("1997-06-26"),
                    Type = BookTable.BookType.Book,
                    Description = "The first book in the beloved Harry Potter series about a young wizard's adventures.",
                }, 
                new BookTable
                {
                    Title = "Pride and Prejudice",
                    Author = "Jane Austen",
                    Genre = "Classic Fiction",
                    PublicationYear = DateTime.Parse("1813-01-28"),
                    Type = BookTable.BookType.Book,
                    Description = "A classic novel exploring love, class, and societal expectations in 19th-century England.",
                },
                new BookTable
                {
                    Title = "The Hobbit",
                    Author = "J.R.R. Tolkien",
                    Genre = "Fantasy",
                    PublicationYear = DateTime.Parse("1937-09-21"),
                    Type = BookTable.BookType.Book,
                    Description = "An adventure tale that introduces readers to the world of Middle-earth.",
                },
                new BookTable
                {
                    Title = "The Lord of the Rings",
                    Author = "J.R.R. Tolkien",
                    Genre = "Fantasy",
                    PublicationYear = DateTime.Parse("1954-07-29"),
                    Type = BookTable.BookType.Book,
                    Description = "A fantasy epic that follows the quest to destroy the One Ring and save Middle-earth.",
                },
                new BookTable
                {
                    Title = "The Da Vinci Code",
                    Author = "Dan Brown",
                    Genre = "Mystery",
                    PublicationYear = DateTime.Parse("2003-03-18"),
                    Type = BookTable.BookType.Book,
                    Description = "A fast-paced mystery thriller involving art, history, and conspiracy.",
                },
                new BookTable
                {
                    Title = "The Alchemist",
                    Author = "Paulo Coelho",
                    Genre = "Fiction",
                    PublicationYear = DateTime.Parse("1988-01-01"),
                    Type = BookTable.BookType.Book,
                    Description = "A philosophical novel about a shepherd's journey to discover his personal legend.",
                }, new BookTable
                {
                    Title = "The Hunger Games",
                    Author = "Suzanne Collins",
                    Genre = "Young Adult",
                    PublicationYear = DateTime.Parse("2008-09-14"),
                    Type = BookTable.BookType.Book,
                    Description = "The first book in a dystopian series about a deadly competition in a post-apocalyptic world.",
                },
                new BookTable
                {
                    Title = "Moby-Dick",
                    Author = "Herman Melville",
                    Genre = "Adventure",
                    PublicationYear = DateTime.Parse("1851-10-18"),
                    Type = BookTable.BookType.Book,
                    Description = "An epic novel about Captain Ahab's obsessive quest to kill the white whale, Moby-Dick.",
                },
                new BookTable
                {
                    Title = "The Catcher in the Rye",
                    Author = "J.D. Salinger",
                    Genre = "Fiction",
                    PublicationYear = DateTime.Parse("1951-07-16"),
                    Type = BookTable.BookType.Book,
                    Description = "A novel following the experiences of a disenchanted teenager in New York City.",
                },
                new BookTable
                {
                    Title = "Little Women",
                    Author = "Louisa May Alcott",
                    Genre = "Classic Fiction",
                    PublicationYear = DateTime.Parse("1868-09-30"),
                    Type = BookTable.BookType.Book,
                    Description = "A heartwarming story about the lives of four sisters during the American Civil War.",
                },
                new BookTable
                {
                    Title = "The Shining",
                    Author = "Stephen King",
                    Genre = "Horror",
                    PublicationYear = DateTime.Parse("1977-01-28"),
                    Type = BookTable.BookType.Book,
                    Description = "A psychological horror novel about a family's terrifying experiences at an isolated hotel.",
                },
                new BookTable
                {
                    Title = "The Road",
                    Author = "Cormac McCarthy",
                    Genre = "Post-Apocalyptic",
                    PublicationYear = DateTime.Parse("2006-09-26"),
                    Type = BookTable.BookType.Book,
                    Description = "A post-apocalyptic novel following a father and son's journey through a desolate world.",
                },
                new BookTable
                {
                    Title = "Brave New World",
                    Author = "Aldous Huxley",
                    Genre = "Dystopian Fiction",
                    PublicationYear = DateTime.Parse("1932-10-27"),
                    Type = BookTable.BookType.Book,
                    Description = "A dystopian vision of a society controlled by technology and conditioning.",
                },
                new BookTable
                {
                    Title = "The Girl with the Dragon Tattoo",
                    Author = "Stieg Larsson",
                    Genre = "Mystery",
                    PublicationYear = DateTime.Parse("2005-08-19"),
                    Type = BookTable.BookType.Book,
                    Description = "A gripping mystery involving a journalist and a hacker investigating a decades-old disappearance.",
                },
                new BookTable
                {
                    Title = "Frankenstein",
                    Author = "Mary Shelley",
                    Genre = "Gothic Fiction",
                    PublicationYear = DateTime.Parse("1818-01-01"),
                    Type = BookTable.BookType.Book,
                    Description = "A classic Gothic novel about a scientist who creates a monstrous being.",
                },
                new BookTable
                {
                    Title = "The Kite Runner",
                    Author = "Khaled Hosseini",
                    Genre = "Fiction",
                    PublicationYear = DateTime.Parse("2003-05-29"),
                    Type = BookTable.BookType.Book,
                    Description = "A story of friendship, betrayal, and redemption in Afghanistan.",
                },
                new BookTable
                {
                    Title = "The Chronicles of Narnia",
                    Author = "C.S. Lewis",
                    Genre = "Fantasy",
                    PublicationYear = DateTime.Parse("1950-10-16"),
                    Type = BookTable.BookType.Book,
                    Description = "A series of seven fantasy novels set in the magical land of Narnia.",
                },
                new BookTable
                {
                    Title = "The Fault in Our Stars",
                    Author = "John Green",
                    Genre = "Young Adult",
                    PublicationYear = DateTime.Parse("2012-01-10"),
                    Type = BookTable.BookType.Book,
                    Description = "A heart-wrenching story of two teenagers with cancer who fall in love.",
                },
                new BookTable
                {
                    Title = "The Grapes of Wrath",
                    Author = "John Steinbeck",
                    Genre = "Historical Fiction",
                    PublicationYear = DateTime.Parse("1939-04-14"),
                    Type = BookTable.BookType.Book,
                    Description = "A novel about the Joad family's struggles during the Great Depression.",
                },
                new BookTable
                {
                    Title = "A Tale of Two Cities",
                    Author = "Charles Dickens",
                    Genre = "Historical Fiction",
                    PublicationYear = DateTime.Parse("1859-04-30"),
                    Type = BookTable.BookType.Book,
                    Description = "A historical novel set during the French Revolution with the famous opening line, 'It was the best of times, it was the worst of times.'",
                },
                new BookTable
                {
                    Title = "The Road Less Traveled",
                    Author = "M. Scott Peck",
                    Genre = "Self-Help",
                    PublicationYear = DateTime.Parse("1978-05-23"),
                    Type = BookTable.BookType.Book,
                    Description = "A self-help book that explores the path to spiritual growth and psychological development.",
                },
                new BookTable
                {
                    Title = "Fahrenheit 451",
                    Author = "Ray Bradbury",
                    Genre = "Dystopian Fiction",
                    PublicationYear = DateTime.Parse("1953-10-19"),
                    Type = BookTable.BookType.Book,
                    Description = "A dystopian novel about a future society where books are banned and burned.",
                },
                new BookTable
                {
                    Title = "The Hitchhiker's Guide to the Galaxy",
                    Author = "Douglas Adams",
                    Genre = "Science Fiction",
                    PublicationYear = DateTime.Parse("1979-10-12"),
                    Type = BookTable.BookType.Book,
                    Description = "A humorous science fiction series about an unwitting space traveler and his guidebook.",
                },
                new BookTable
                {
                    Title = "The Shawshank Redemption",
                    Author = "Frank Darabont",
                    Genre = "Drama",
                    PublicationYear = DateTime.Parse("1994-09-23"),
                    Type = BookTable.BookType.DVD,
                    Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                },
                new BookTable
                {
                    Title = "The Godfather",
                    Author = "Francis Ford Coppola",
                    Genre = "Crime",
                    PublicationYear = DateTime.Parse("1972-03-24"),
                    Type = BookTable.BookType.DVD,
                    Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
                },
                new BookTable
                {
                    Title = "Pulp Fiction",
                    Author = "Quentin Tarantino",
                    Genre = "Crime",
                    PublicationYear = DateTime.Parse("1994-10-14"),
                    Type = BookTable.BookType.DVD,
                    Description = "The lives of two mob hitmen, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                },
                new BookTable
                {
                    Title = "The Dark Knight",
                    Author = "Christopher Nolan",
                    Genre = "Action",
                    PublicationYear = DateTime.Parse("2008-07-18"),
                    Type = BookTable.BookType.DVD,
                    Description = "When the menace known as The Joker emerges, Batman must confront him to bring justice to Gotham City.",
                },
                new BookTable
                {
                    Title = "Forrest Gump",
                    Author = "Robert Zemeckis",
                    Genre = "Drama",
                    PublicationYear = DateTime.Parse("1994-07-06"),
                    Type = BookTable.BookType.DVD,
                    Description = "The presidencies of Kennedy and Johnson, the Vietnam War, the Watergate scandal, and other historical events unfold through the perspective of an Alabama man with an IQ of 75.",
                },
                new BookTable
                {
                    Title = "Schindler's List",
                    Author = "Steven Spielberg",
                    Genre = "Biography",
                    PublicationYear = DateTime.Parse("1993-12-15"),
                    Type = BookTable.BookType.DVD,
                    Description = "In Poland during World War II, Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.",
                },
                new BookTable
                {
                    Title = "The Lord of the Rings: The Return of the King",
                    Author = "Peter Jackson",
                    Genre = "Adventure",
                    PublicationYear = DateTime.Parse("2003-12-17"),
                    Type = BookTable.BookType.DVD,
                    Description = "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.",
                },
                new BookTable
                {
                    Title = "The Dark Side of the Moon",
                    Author = "Pink Floyd",
                    Genre = "Progressive Rock",
                    PublicationYear = DateTime.Parse("1973-03-01"),
                    Type = BookTable.BookType.CD,
                    Description = "One of the most iconic albums in the history of rock music.",
                },
                new BookTable
                {
                    Title = "Thriller",
                    Author = "Michael Jackson",
                    Genre = "Pop",
                    PublicationYear = DateTime.Parse("1982-11-30"),
                    Type = BookTable.BookType.CD,
                    Description = "The best-selling album of all time, featuring hits like 'Billie Jean' and 'Beat It'.",
                },
                new BookTable
                {
                    Title = "Abbey Road",
                    Author = "The Beatles",
                    Genre = "Rock",
                    PublicationYear = DateTime.Parse("1969-09-26"),
                    Type = BookTable.BookType.CD,
                    Description = "A legendary album known for its iconic cover and classic songs like 'Come Together' and 'Here Comes the Sun'.",
                },
                new BookTable
                {
                    Title = "Rumours",
                    Author = "Fleetwood Mac",
                    Genre = "Rock",
                    PublicationYear = DateTime.Parse("1977-02-04"),
                    Type = BookTable.BookType.CD,
                    Description = "A critically acclaimed album with hits like 'Go Your Own Way' and 'Dreams'.",
                },
                new BookTable
                {
                    Title = "The Wall",
                    Author = "Pink Floyd",
                    Genre = "Progressive Rock",
                    PublicationYear = DateTime.Parse("1979-11-30"),
                    Type = BookTable.BookType.CD,
                    Description = "A rock opera and one of Pink Floyd's most famous albums.",
                } 
                );
                context.SaveChanges();
            }
        }
    }
}