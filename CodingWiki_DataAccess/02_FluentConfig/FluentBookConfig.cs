using CodingWiki_Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_DataAccess._02_FluentConfig
{
    public class FluentBookConfig : IEntityTypeConfiguration<Fluent_Book>
    {
        public void Configure(EntityTypeBuilder<Fluent_Book> modelBuilder)
        {
            //Table Name
            modelBuilder.ToTable("Fluent_Book"); // Table name in dabase

            //Primary Key
            modelBuilder.Property(u => u.BookId).IsRequired(); // Requiered
            modelBuilder.HasKey(u => u.BookId); // Primary key

            //Other validatios
            modelBuilder.Property(u => u.ISBN).IsRequired(); // Requiered
            modelBuilder.Property(u => u.ISBN).HasMaxLength(50); // MaxLength
            modelBuilder.Ignore(u => u.PriceRange); // Do not mappeped in Database


            //Relation ONE to MANY -------------------------------------
            modelBuilder.HasOne(u => u.Fluent_Publisher).WithMany(u => u.Fluent_Book)
                .HasForeignKey(u => u.Publisher_Id);
        }
    }
}
