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
        public Book(int bookId, string title, string iSBN, decimal price, int publisher_Id)
        {
            BookId = bookId;
            Title = title;
            ISBN = iSBN;
            Price = price;
            Publisher_Id = publisher_Id;
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

        /* ONE TO ONE ***************************** BooK property in BookDetail */
        public BookDetail BookDetail { get; set; } //navegation property



        /* ONE TO MANY *****************************  ICollection<Book> Books in  Publisher*/
        //This is the foreing key property
        // And Publisher (property name) is the tablerelationship whit the foreing key
        [ForeignKey("Publisher")]
        public int Publisher_Id { get; set; }
        public Publisher Publisher { get; set; } //navegation property



        /* MANY TO MANY *****************************  ICollection<Book> Books in  Author AUTOMATIC*/

        //Books can have multiples Authors 
        //public ICollection<Author> Authors { get; set; }//navegation property

        /* MANY TO MANY ***************************** creating BookAuthor table*/
        public ICollection<BookAuthor> BooksAuthor { get;}

    }
}
