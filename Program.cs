using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookCave.Data;
using BookCave.Models.EntityModels;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookCave
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            //SeedData();
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        public static void SeedData()
        {
            var db = new DataContext();
            
            if(!db.Books.Any())
            {
                
                var initialBooks = new List<BookEntity> 
                {
                   /* new BookEntity
                    { 
                            author = "George Orwell", 
                            ISBN = "978-0451524935", 
                            title = "1984", 
                            year = 1949, 
                            numberOfPages = 328, 
                            rating = 4.5, 
                            noOfRatings = 1,
                            description = "Written in 1948, 1984 was George Orwell’s chilling prophecy about the future. And while 1984 has come and gone, his dystopian vision of a government that will do anything to control the narrative is timelier than ever... Nominated as one of America’s best-loved novels by PBS’s The Great American Read• “The Party told you to reject the evidence of your eyes and ears. It was their final, most essential command.” Winston Smith toes the Party line, rewriting history to satisfy the demands of the Ministry of Truth. With each lie he writes, Winston grows to hate the Party that seeks power for its own sake and persecutes those who dare to commit thoughtcrimes. But as he starts to think for himself, Winston can’t escape the fact that Big Brother is always watching... A startling and haunting vision of the world, 1984 is so powerful that it is completely convincing from start to finish. No one can deny the influence of this novel, its hold on the imaginations of multiple generations of readers, or the resiliency of its admonitions—a legacy that seems only to grow with the passage of time.", 
                            country = "United Kingdom",
                            language = "English",
                            publisher = "Secker & Warburg",
                            price = 6, 
                            category = "Dystopian", 
                            noOfSoldUnits = 0, 
                            noOfCopiesAvailable = 100, 
                            discount = 1,
                            image = "https://upload.wikimedia.org/wikipedia/en/c/c3/1984first.jpg"
                    },
                    new BookEntity
                    { 
                            author = "Stephen Hawking", 
                            ISBN = "978-0-553-10953-5", 
                            title = "A Brief History of Time", 
                            year = 1988, 
                            numberOfPages = 258, 
                            rating = 4.5, 
                            noOfRatings = 1,
                            description = "A landmark volume in science writing by one of the great minds of our time, Stephen Hawking’s book explores such profound questions as: How did the universe begin—and what made its start possible? Does time always flow forward? Is the universe unending—or are there boundaries? Are there other dimensions in space? What will happen when it all ends? Told in language we all can understand, A Brief History of Time plunges into the exotic realms of black holes and quarks, of antimatter and “arrows of time,” of the big bang and a bigger God—where the possibilities are wondrous and unexpected. With exciting images and profound imagination, Stephen Hawking brings us closer to the ultimate secrets at the very heart of creation.", 
                            country = "United Kingdom",
                            language = "English",
                            publisher = "Bantam Dell Publishing Group",
                            price = 9, 
                            category = "Cosmology", 
                            noOfSoldUnits = 0, 
                            noOfCopiesAvailable = 100, 
                            discount = 1,
                            image = "https://upload.wikimedia.org/wikipedia/en/a/a3/BriefHistoryTime.jpg"
                    },
                    new BookEntity
                    { 
                            author = "Lewis Carroll", 
                            ISBN = "978-0553213454",
                            title = "Alice's Adventures in Wonderland & Through the Looking Glass", 
                            year = 1865, 
                            numberOfPages = 272, 
                            rating = 4.5, 
                            noOfRatings = 1,
                            description = "In 1862 Charles Lutwidge Dodgson, a shy Oxford mathematician with a stammer, created a story about a little girl tumbling down a rabbit hole. Thus began the immortal adventures of Alice, perhaps the most popular heroine in English literature. Countless scholars have tried to define the charm of the Alice books–with those wonderfully eccentric characters the Queen of Hearts, Tweedledum, and Tweedledee, the Cheshire Cat, Mock Turtle, the Mad Hatter et al.–by proclaiming that they really comprise a satire on language, a political allegory, a parody of Victorian children’s literature, even a reflection of contemporary ecclesiastical history. Perhaps, as Dodgson might have said, Alice is no more than a dream, a fairy tale about the trials and tribulations of growing up–or down, or all turned round–as seen through the expert eyes of a child.", 
                            country = "United Kingdom",
                            language = "English",
                            publisher = "Bantam Classics",
                            price = 10, 
                            category = "Fantasy", 
                            noOfSoldUnits = 0, 
                            noOfCopiesAvailable = 100, 
                            discount = 0.85,
                            image = "https://upload.wikimedia.org/wikipedia/en/3/3f/Alice_in_Wonderland%2C_cover_1865.jpg"
                    },
                    new BookEntity
                    { 
                            author = "Ray Bradbury", 
                            ISBN = "978-1451673319",
                            title = "Fahrenheit 451", 
                            year = 1953, 
                            numberOfPages = 158, 
                            rating = 4.5, 
                            noOfRatings = 1,
                            description = "Ray Bradbury’s internationally acclaimed novel Fahrenheit 451 is a masterwork of twentieth-century literature set in a bleak, dystopian future. Guy Montag is a fireman. In his world, where television rules and literature is on the brink of extinction, firemen start fires rather than put them out. His job is to destroy the most illegal of commodities, the printed book, along with the houses in which they are hidden. Montag never questions the destruction and ruin his actions produce, returning each day to his bland life and wife, Mildred, who spends all day with her television “family.” But then he meets an eccentric young neighbor, Clarisse, who introduces him to a past where people didn’t live in fear and to a present where one sees the world through the ideas in books instead of the mindless chatter of television. When Mildred attempts suicide and Clarisse suddenly disappears, Montag begins to question everything he has ever known. He starts hiding books in his home, and when his pilfering is discovered, the fireman has to run for his life.", 
                            country = "United States",
                            language = "English",
                            publisher = "Ballantine Books",
                            price = 6, 
                            category = "Dystopian", 
                            noOfSoldUnits = 0, 
                            noOfCopiesAvailable = 1, 
                            discount = 1,
                            image = "https://upload.wikimedia.org/wikipedia/en/d/db/Fahrenheit_451_1st_ed_cover.jpg"
                    }*/
                };

                db.AddRange(initialBooks);
                db.SaveChanges();
            }
        }
    }
}
