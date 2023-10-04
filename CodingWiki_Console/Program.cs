using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.CompilerServices;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        #region check database and migrations 
        // check if database is created in case of no, this command create it
        //using (ApplicationDbContext context = new ApplicationDbContext())
        //{
        //    context.Database.EnsureCreated();
        //    if (context.Database.GetPendingMigrations().Count() > 0)
        //    {
        //        context.Database.Migrate();
        //    }
        //}
        #endregion

       // AddBook();
        //List<Book> books = GetAllBooks();
        //foreach (var item in books)
        //{
        //    Console.WriteLine(item.FormatToString());
        //}

        //Console.WriteLine("Get One Book");
        //Console.WriteLine(GetBookById(6).FormatToString());

        #region pagination
        //int take = 2;
        //int count = BooksCount();

        //for (int skip = 0; skip < count; skip = skip + take)
        //{
        //    Console.WriteLine($"Books from {skip} to {skip + take} ----------------");
        //    List<Book> booksPagination = Pagination(take, skip);
        //    foreach (var item in booksPagination)
        //    {
        //        Console.WriteLine(item.FormatToString());
        //    }
        //}
        #endregion

        //  UpdateBook(1, new Book() { Title = "Martinez", Price = 100 });
        DeletBook(9);
    }

    private static List<Book> GetAllBooks()
    {
        List<Book> books = new List<Book>();
        using (ApplicationDbContext context = new ApplicationDbContext())
        {
            books = context.Books.ToList();
        }

        return books;
    }

    /// <summary>
    /// If I pass information or text direct in where condition
    /// Where(x=> x.Title == "Test")
    /// Ef Pass the text to database, that is bad because of Database injection
    /// ALWAYS USE VARIABLE 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private static Book GetBookByTitle(string title)
    {
        using (ApplicationDbContext context = new ApplicationDbContext())
            return context.Books.FirstOrDefault(u => u.Title == title);
    }

    /// <summary>
    ///  .Find only work on Primary Key
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private static Book GetBookById(int id = 1)
    {
        using (ApplicationDbContext context = new ApplicationDbContext())
            return context.Books.Find(id);
    }
    private static void AddBook()
    {

        using (ApplicationDbContext context = new ApplicationDbContext())
        {
            Book book = new Book()
            {
                Title = "Crack code interview",
                Price = 570.5m,
                ISBN = "code interview",
                Publisher_Id = 1
            };
            context.Add(book);

            Book book2 = new Book()
            {
                Title = "Amazon interview",
                Price = 570.5m,
                ISBN = "Amazon interview",
                Publisher_Id = 1
            };
            context.Add(book2);
           context.SaveChanges();
        }
    }

    private static List<Book> BookTitleContains(string prefix)
    {
        //Normal Like way "%prefix%"
        //using (ApplicationDbContext context = new ApplicationDbContext())
        //    return context.Books.Where(u => u.Title.Contains(prefix)).ToList();

        using (ApplicationDbContext context = new ApplicationDbContext())
            return context.Books.Where(u => EF.Functions.Like(u.Title, $"{prefix}%")).ToList();
    }
    private static int BooksCount()
    {
        using (ApplicationDbContext context = new ApplicationDbContext())
            return context.Books.Count();
    }

    private static List<Book> Pagination(int take = 2, int skip = 0)
    {
        using (ApplicationDbContext context = new ApplicationDbContext())
            return context.Books.Skip(skip).Take(take).ToList();
    }

    private static void UpdateBook(int id, Book book)
    {
        using (ApplicationDbContext context = new ApplicationDbContext())
        {
            Book temp = context.Books.Find(id);
            temp.Title = book.Title;
            temp.Price = book.Price;
            context.SaveChanges();
        }
          
    }

    private static void DeletBook(int id)
    {
        using (ApplicationDbContext context = new ApplicationDbContext())
        {
            Book temp = context.Books.Find(id);
            context.Books.Remove(temp);
            context.SaveChanges();
        }

    }

}


public static class Extensions
{
    public static string FormatToString(this Book item)
    {
        return $"{item.BookId} : {item.Title}, {item.BooksAuthor}, {item.Price}";
    }
}