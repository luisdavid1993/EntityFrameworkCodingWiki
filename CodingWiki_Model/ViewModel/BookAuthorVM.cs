using CodingWiki_Model.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_Model.ViewModel
{
    public class BookAuthorVM
    {
        public BookAuthor bookAuthor { get; set; }
        public Book book{ get; set; }

        public IEnumerable<BookAuthor> bookAuthorList { get; set; }
        public IEnumerable<SelectListItem> authorList { get; set; }
    }
}
