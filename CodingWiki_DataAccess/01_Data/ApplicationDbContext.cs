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
        public DbSet<BookDetail> BookDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CodeWiki;Integrated Security=True;TrustServerCertificate=True;Trusted_Connection = True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent Api
            modelBuilder.Entity<Book>().Property(u => u.Price).HasPrecision(20, 2); //decimal(20,2)
            modelBuilder.Entity<BookAuthor>().HasKey(u => new { u.BookId, u.AuthorId });


            //Seed Data
            modelBuilder.Entity<Book>().HasData(new Book(1, "Luis David", "LD", 500,1),new Book(2, "Mariangelis", "LD", 870,1));
            Book[] books = new Book[] { new Book(3, "Pro C#", "JR Martin", 7562,1), new Book(4,"Clean Code", "Dr jr", 870, 1) };
            modelBuilder.Entity<Book>().HasData(books);
            modelBuilder.Entity<Publisher>().HasData(new Publisher() { Publisher_Id=1, Name ="Rober J Martin", Location = "USA"});


           
        }
    }
}
