using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using CodingWiki_Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CodingWiki_Web.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BookController> _logger;

        public BookController(ApplicationDbContext context, ILogger<BookController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Book> books = _context.Books
                .Include(u => u.Publisher)
                .Include(x => x.BooksAuthor)
                .ThenInclude(i => i.Author) // to include sub objects (Author) in another sub object (BooksAuthor)
                .ToList();
            //List<Book> books = _context.Books.Include(u=> u.Publisher).ToList();
            //List<Book> books = _context.Books.ToList();
            //foreach (var item in books)
            //{
            //    // lest efficient 
            //    //item.Publisher = _context.Publishers.FirstOrDefault(u => u.Publisher_Id == item.Publisher_Id);

            //    //best efficient
            //    _context.Entry(item).Reference(u => u.Publisher).Load(); // this is for objects
            //    _context.Entry(item).Collection(u => u.BooksAuthor).Load(); // this is for collections 
            //    foreach (var row in item.BooksAuthor)
            //    {
            //        _context.Entry(row).Reference(u => u.Author).Load(); // populating Author object inside BooksAuthor
            //    }
            //}
            return View(books);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            BookVM book = new BookVM();

            book.PublisherList = _context.Publishers
                .Select(u=> new SelectListItem() 
                { 
                    Text = u.Name, 
                    Value = u.Publisher_Id.ToString()
                }).ToList();

            if(id == null || id ==0)
                return View(book);


            book.BookInformation = _context.Books.FirstOrDefault(u => u.BookId == id);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(BookVM bookVM)
        {
            if (bookVM.BookInformation.BookId == 0)
                _context.Books.Add(bookVM.BookInformation);

            else
                _context.Books.Update(bookVM.BookInformation);

            _context.SaveChanges();

            return  RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {

            if (id == null || id == 0)
                return NotFound();

            BookDetail detail = new BookDetail();

            //detail.Book = _context.Books.FirstOrDefault(x => x.BookId == id);
            //detail = _context.BookDetails.FirstOrDefault(y => y.BookId == id);

            detail = _context.BookDetails.Include(u=> u.Book).FirstOrDefault(y => y.BookId == id);

            if (detail == null)
                return NotFound();


            return View(detail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(BookDetail detail)
        {

            if (detail.BookDetail_Id == 0)
                _context.BookDetails.Add(detail);

            else
                _context.BookDetails.Update(detail);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Book book = new Book();
            book = _context.Books.FirstOrDefault(u => u.BookId == id);
            if(book == null)
                return NotFound();

            _context.Books.Remove(book);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult ManageAuthors(int id)
        { 
            BookAuthorVM bookAuthor = new BookAuthorVM();
            bookAuthor.bookAuthorList = _context.BookAuthors.Include(u=> u.Book).Include(u=> u.Author).Where(u => u.BookId == id).ToList();
            bookAuthor.bookAuthor = new BookAuthor();
            bookAuthor.bookAuthor.BookId= id;
            bookAuthor.book = _context.Books.FirstOrDefault(u => u.BookId == id);

            List<int> tempListOfAssignedAuthor = bookAuthor.bookAuthorList.Select(u=> u.AuthorId).ToList();
            //Authors not in this book; not in tempListOfAssignedAuthor
            List<Author> tempList = _context.Authors.Where(u=> !tempListOfAssignedAuthor.Contains(u.Author_Id)).ToList();
            bookAuthor.authorList = tempList.Select(i => new SelectListItem() { Text = i.FullName, Value = i.Author_Id.ToString() });

            return View(bookAuthor);
        }
        [HttpPost]
        public IActionResult ManageAuthors(BookAuthorVM bookAuthorVM)
        {
            if (bookAuthorVM.book.BookId != 0 && bookAuthorVM.bookAuthor.AuthorId != 0)
            {
                _context.BookAuthors.Add(bookAuthorVM.bookAuthor);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(ManageAuthors), new { @id = bookAuthorVM.bookAuthor.BookId });
        }

        public IActionResult RemoveAuthors(int authorId, BookAuthorVM bookAuthorVM)
        {
            int bookId = bookAuthorVM.book.BookId;
            BookAuthor bookAuthor = _context.BookAuthors.FirstOrDefault(u=> u.BookId == bookId && u.AuthorId == authorId);
            _context.BookAuthors.Remove(bookAuthor);
            _context.SaveChanges();
            return RedirectToAction(nameof(ManageAuthors), new { @id = bookId });
        }
        public IActionResult PlayGround()
        {
            // calling a view
            List<BookView> bookViews= _context.BookViews.ToList();

            // executing sql raw
            var booksRaw = _context.Books.FromSqlRaw("select * from Books").ToList(); // it only work with select *

            // executing sql interpolated (this is with parameters) you have to use variable in where condition
            int id = 1;
            var booksRaw1 = _context.Books.FromSqlInterpolated($"select * from Books where Bookid = {id}").ToList(); // it only work with select *

            //calling a store procedure
            var storeProcedure = _context.Books.FromSqlInterpolated($"exec getAllBookDetails {id}").ToList();

            var bookdetail = _context.BookDetails.Include(_u => _u.Book).FirstOrDefault();
            bookdetail.NumberOfChapters = 222;
            bookdetail.Book.Price = 2222;
            _context.BookDetails.Update(bookdetail);
            _context.SaveChanges();


            var bookdetail2 = _context.BookDetails.Include(_u => _u.Book).FirstOrDefault();
            bookdetail2.NumberOfChapters = 111;
            bookdetail2.Book.Price = 1111;
            _context.BookDetails.Attach(bookdetail2); // just update properties changed 
            _context.SaveChanges();

            //var bookTemp = _context.Books.FirstOrDefault();
            //bookTemp.Price = 100;

            //var bookCollection = _context.Books;
            //decimal totalPrice = 0;

            //foreach (var book in bookCollection)
            //{
            //    totalPrice += book.Price;
            //}

            //var bookList = _context.Books.ToList();
            //foreach (var book in bookList)
            //{
            //    totalPrice += book.Price;
            //}

            //var bookCollection2 = _context.Books;
            //var bookCount1 = bookCollection2.Count();

            //var bookCount2 = _context.Books.Count();

            return RedirectToAction(nameof(Index));
        }
    }
}
