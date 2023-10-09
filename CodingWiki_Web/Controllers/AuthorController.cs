using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodingWiki_Web.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CategoryController> _logger;

        public AuthorController(ApplicationDbContext context, ILogger<CategoryController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            IEnumerable<Author> authors = _context.Authors.ToList();
            return View(authors);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Author author = new Author();
            if (id == null || id == 0)
            { //create
                return View(author);
            }
            //Update
            author = _context.Authors.FirstOrDefault(u => u.Author_Id == id);
            if (author == null)
                return NotFound();
            return View(author);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Author authors)
        {
            if (ModelState.IsValid)
            {
                if (authors.Author_Id == 0)
                    await _context.Authors.AddAsync(authors); //create
                else
                    _context.Authors.Update(authors); // update all object event CategoryId

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(authors);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Author author = new Author();

            author = _context.Authors.FirstOrDefault(u => u.Author_Id == id);
            if (author == null)
                return NotFound();

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
