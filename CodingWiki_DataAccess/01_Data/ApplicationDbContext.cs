using CodingWiki_Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {

        //This create tables 
        //IF you remove some of these properties and make a migration it is going to delete the table 
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CodeWiki;Integrated Security=True;TrustServerCertificate=True;Trusted_Connection = True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().Property(u => u.Price).HasPrecision(20, 2); //decimal(20,2)
            modelBuilder.Entity<Book>().HasData
                (
                new Book(1, "Luis David", "LD", 500),
                new Book(2, "Mariangelis", "LD", 870)
                );

            Book[] books = new Book[] { new Book(3, "Pro C#", "JR Martin", 7562), new Book(4,"Clean Code", "Dr jr", 870) };

            modelBuilder.Entity<Book>().HasData(books);
        }
    }
}
