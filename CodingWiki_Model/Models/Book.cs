using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_Model.Models
{
    /// <summary>
    /// Primary Key Set
    /// If the table only has one integer property with name ID
    /// with [Key]
    /// If the table only has one integer property THAT ENDS IN ID
    /// </summary>
    public class Book
    {
        public Book(int bookId, string title, string iSBN, decimal price)
        {
            BookId = bookId;
            Title = title;
            ISBN = iSBN;
            Price = price;
        }
        public Book() { }   

        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        [MaxLength(20)]
        [Required]
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        [NotMapped]
        public string PriceRange { get; set; }
    }
}
