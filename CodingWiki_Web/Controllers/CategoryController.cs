using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodingWiki_Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ApplicationDbContext context, ILogger<CategoryController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Category> categories = _context.Categories.AsNoTracking().ToList(); //Adding AsNotTracking command, you cannot see _context Tracking in database
            return View(categories);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Category category = new Category();
            if (id == null || id == 0)
            { //create
             return View(category);
            }
            //Update
            category = _context.Categories.FirstOrDefault(u=> u.CategoryId== id);
            if(category == null)
                return NotFound();
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.CategoryId == 0)
                    await _context.Categories.AddAsync(category); //create
               else
                    _context.Categories.Update(category); // update all object event CategoryId

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Category category = new Category();

            category = _context.Categories.FirstOrDefault(u => u.CategoryId == id);
            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);
             await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CreateMultiple2()
        {
            for (int i = 0; i < 2; i++)
            {
                _context.Add(new Category() { Name = Guid.NewGuid().ToString() });
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> CreateMultiple5()
        {
            for (int i = 0; i < 5; i++)
            {
                _context.Add(new Category() { Name = Guid.NewGuid().ToString() });
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveMultiple2()
        {
            IEnumerable<Category> categories = _context.Categories.OrderByDescending(u=> u.CategoryId).Take(2);

            _context.RemoveRange(categories);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> RemoveMultiple5()
        {
            IEnumerable<Category> categories = _context.Categories.OrderByDescending(u => u.CategoryId).Take(5);

            _context.RemoveRange(categories);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
