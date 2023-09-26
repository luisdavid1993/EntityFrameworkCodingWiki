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
    public class Fluent_Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public string PriceRange { get; set; }
        public Fluent_BookDetail Fluent_BookDetail { get; set; } //navegation property
        public int Publisher_Id { get; set; }
        public Fluent_Publisher Fluent_Publisher { get; set; } //navegation property
        public ICollection<Fluent_BookAuthor> Fluent_BookAuthor { get; }


    }
}
