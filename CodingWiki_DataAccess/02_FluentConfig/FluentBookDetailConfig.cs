using CodingWiki_Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_DataAccess._02_FluentConfig
{
    public class FluentBookDetailConfig : IEntityTypeConfiguration<Fluent_BookDetail>
    {
        public void Configure(EntityTypeBuilder<Fluent_BookDetail> modelBuilder)
        {
            //Table Name
            modelBuilder.ToTable("Fluent_BookDetail"); // Table name in dabase

            //Primary Key
            modelBuilder.Property(u => u.BookDetail_Id).IsRequired(); // Requiered
            modelBuilder.HasKey(u => u.BookDetail_Id); // Primary key

            //Other validatios
            modelBuilder.Property(u => u.NumberOfChapters).HasColumnName("NoOfChapters"); // Column name

            //Relation ONE to ONE -------------------------------------
            modelBuilder.HasOne(u => u.Fluent_Book).WithOne(u => u.Fluent_BookDetail)
                .HasForeignKey<Fluent_BookDetail>(u => u.BookId);
        }
    }
}
