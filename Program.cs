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

      /*  public static void SeedData()
        {
            var db = new DataContext();
            
            if(!db.Books.Any())
            {
                var initialBooks = new List<BookEntity> 
                {
                    new BookEntity
                    { 
                            author = "George Orwell", 
                            ISBN = "978-0451524935", 
                            title = "1984", 
                            year = 1949, 
                            numberOfPages = 328, 
                            rating = 4.5, 
                            description = "Written in 1948, 1984 was George Orwell’s chilling prophecy about the future. And while 1984 has come and gone, his dystopian vision of a government that will do anything to control the narrative is timelier than ever... Nominated as one of America’s best-loved novels by PBS’s The Great American Read• “The Party told you to reject the evidence of your eyes and ears. It was their final, most essential command.” Winston Smith toes the Party line, rewriting history to satisfy the demands of the Ministry of Truth. With each lie he writes, Winston grows to hate the Party that seeks power for its own sake and persecutes those who dare to commit thoughtcrimes. But as he starts to think for himself, Winston can’t escape the fact that Big Brother is always watching... A startling and haunting vision of the world, 1984 is so powerful that it is completely convincing from start to finish. No one can deny the influence of this novel, its hold on the imaginations of multiple generations of readers, or the resiliency of its admonitions—a legacy that seems only to grow with the passage of time.", 
                            country = "United Kingdom",
                            language = "English",
                            publisher = "Secker & Warburg",
                            price = 6, 
                            category = "Dystopian", 
                            noOfSoldUnits = 0, 
                            noOfCopiesAvailable = 100, 
                            discount = 1
                    }
                };

                db.AddRange(initialBooks);
                db.SaveChanges();
            }
        }*/
    }
}
