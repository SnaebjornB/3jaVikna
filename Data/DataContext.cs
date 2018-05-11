using BookCave.Models.EntityModels;
using BookCave.Models.InputModels;
using BookCave.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BookCave.Data
{
    public class DataContext : DbContext
    {
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<AddressEntity> Addresses { get; set; }
        public DbSet<OrderItemEntity> OrderItems { get; set; }
        public DbSet<ReviewEntity> Reviews { get; set; }
        public DbSet<CCardEntity> Cards { get; set; }
        public DbSet<ReviewViewModel> CurrentOrder { get; set; }
        public DbSet<ReviewBookViewModel> CurrentOrderBooks { get; set; }
        public DbSet<OrderHistoryEntity> OrderHistory { get; set; } 
        public DbSet<OrderHistoryBookViewModel> OrderHistoryBooks { get; set; }

        public DbSet<JobApplicationEntity> JobApplications { get; set; }
        //Hér kemur allt sem fer í gagnagrunninn.
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(
                    "Server=tcp:verklegt2.database.windows.net,1433;Initial Catalog=VLN2_2018_H13;Persist Security Info=False;User ID=VLN2_2018_H13_usr;Password=pal3Pear34;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30"
                );
        }
    }
}