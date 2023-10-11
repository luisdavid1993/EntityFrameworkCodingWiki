using CodingWiki_DataAccess._02_FluentConfig;
using CodingWiki_Model.Models;
using CodingWiki_Model.Models.FluentModel;
using CodingWiki_Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //This create tables 
        //IF you remove some of these properties and make a migration it is going to delete the table 
        public DbSet<Book> Books { get; set; }
        public DbSet<BookView> BookViews { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }

        public DbSet<Fluent_BookDetail> BookDetail_Fluent { get; set; }
        public DbSet<Fluent_Book> Fluent_Book { get; set; }
        public DbSet<Fluent_Author> Fluent_Author { get; set; }
        public DbSet<Fluent_Publisher> Fluent_Publisher { get; set; }
        public DbSet<Fluent_BookAuthor> Fluent_BookAuthor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //  options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CodeWiki;Integrated Security=True;TrustServerCertificate=True;Trusted_Connection = True")
            //      .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //---------------- Fluent Api Start ---------------//
            modelBuilder.Entity<Book>().Property(u => u.Price).HasPrecision(20, 2); //decimal(20,2)
            modelBuilder.Entity<BookAuthor>().HasKey(u => new { u.BookId, u.AuthorId }); // Primary key


            //modelBuilder.Entity<Fluent_BookDetail>().ToTable("Fluent_BookDetail"); // Table name in dabase
            //modelBuilder.Entity<Fluent_BookDetail>().Property(u => u.NumberOfChapters).HasColumnName("NoOfChapters"); // Column name
            //modelBuilder.Entity<Fluent_BookDetail>().Property(u => u.BookDetail_Id).IsRequired(); // Requiered
            //modelBuilder.Entity<Fluent_BookDetail>().HasKey(u => u.BookDetail_Id ); // Primary key
            ////Relation ONE to ONE -------------------------------------
            //modelBuilder.Entity<Fluent_BookDetail>().HasOne(u => u.Fluent_Book).WithOne(u => u.Fluent_BookDetail)
            //    .HasForeignKey<Fluent_BookDetail>(u=> u.BookId); 

            modelBuilder.ApplyConfiguration(new FluentBookDetailConfig());


            //modelBuilder.Entity<Fluent_Book>().ToTable("Fluent_Book"); // Table name in dabase
            //modelBuilder.Entity<Fluent_Book>().Property(u => u.BookId).IsRequired(); // Requiered
            //modelBuilder.Entity<Fluent_Book>().HasKey(u => u.BookId); // Primary key
            //modelBuilder.Entity<Fluent_Book>().Property(u => u.ISBN).IsRequired(); // Requiered
            //modelBuilder.Entity<Fluent_Book>().Property(u => u.ISBN).HasMaxLength(50); // MaxLength
            //modelBuilder.Entity<Fluent_Book>().Ignore(u => u.PriceRange); // Do not mappeped in Database
            ////Relation ONE to MANY -------------------------------------
            //modelBuilder.Entity<Fluent_Book>().HasOne(u => u.Fluent_Publisher).WithMany(u => u.Fluent_Book)
            //    .HasForeignKey(u=> u.Publisher_Id);

            modelBuilder.ApplyConfiguration(new FluentBookConfig());


            modelBuilder.Entity<Fluent_Author>().ToTable("Fluent_Author"); // Table name in dabase
            modelBuilder.Entity<Fluent_Author>().Property(u => u.Author_Id).IsRequired(); // Requiered
            modelBuilder.Entity<Fluent_Author>().HasKey(u => u.Author_Id); // Primary key
            modelBuilder.Entity<Fluent_Author>().Property(u => u.FirstName).IsRequired(); // Requiered
            modelBuilder.Entity<Fluent_Author>().Property(u => u.FirstName).HasMaxLength(50); // MaxLength
            modelBuilder.Entity<Fluent_Author>().Property(u => u.LastName).IsRequired(); // Requiered
            modelBuilder.Entity<Fluent_Author>().Ignore(u => u.FullName); // Do not mappeped in Database


            modelBuilder.Entity<Fluent_Publisher>().ToTable("Fluent_Publisher"); // Table name in dabase
            modelBuilder.Entity<Fluent_Publisher>().Property(u => u.Publisher_Id).IsRequired(); // Requiered
            modelBuilder.Entity<Fluent_Publisher>().HasKey(u => u.Publisher_Id); // Primary key
            modelBuilder.Entity<Fluent_Publisher>().Property(u => u.Name).IsRequired(); // Requiered



            modelBuilder.Entity<Fluent_BookAuthor>().ToTable("Fluent_BookAuthor"); // Table name in dabase
            modelBuilder.Entity<Fluent_BookAuthor>().HasKey(u => new { u.BookId, u.AuthorId }); // Primary key
            //Relation MANY to MANY -------------------------------------
            modelBuilder.Entity<Fluent_BookAuthor>().HasOne(u => u.Fluent_Book).WithMany(u => u.Fluent_BookAuthor)
                .HasForeignKey(u => u.BookId);
            modelBuilder.Entity<Fluent_BookAuthor>().HasOne(u => u.Fluent_Author).WithMany(u => u.Fluent_BookAuthor)
               .HasForeignKey(u => u.AuthorId);

            //---------------- Fluent Api Ended ---------------//



            //---------------- Seed Data Start ---------------//
            modelBuilder.Entity<Book>().HasData(new Book(1, "Luis David", "LD", 500, 1), new Book(2, "Mariangelis", "LD", 870, 1));
            Book[] books = new Book[] { new Book(3, "Pro C#", "JR Martin", 7562, 1), new Book(4, "Clean Code", "Dr jr", 870, 1) };
            modelBuilder.Entity<Book>().HasData(books);
            modelBuilder.Entity<Publisher>().HasData(new Publisher() { Publisher_Id = 1, Name = "Rober J Martin", Location = "USA" });

            modelBuilder.Entity<BookView>().HasNoKey().ToView("GetOnlyBookDetails"); // for not create a table in database

            //---------------- Seed Data Ended ---------------//

        }
    }
}
