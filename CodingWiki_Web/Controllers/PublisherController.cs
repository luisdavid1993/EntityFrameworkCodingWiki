using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodingWiki_Web.Controllers
{
    public class PublisherController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PublisherController> _logger;

        public PublisherController(ApplicationDbContext context, ILogger<PublisherController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            IEnumerable<Publisher> publishers = _context.Publishers.ToList();
            return View(publishers);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Publisher publisher = new Publisher();
            if (id == null || id == 0)
            { //create
                return View(publisher);
            }
            //Update
            publisher = _context.Publishers.FirstOrDefault(u => u.Publisher_Id == id);
            if (publisher == null)
                return NotFound();
            return View(publisher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                if (publisher.Publisher_Id == 0)
                    await _context.Publishers.AddAsync(publisher); //create
                else
                    _context.Publishers.Update(publisher); // update all object event CategoryId

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(publisher);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Publisher publisher = new Publisher();

            publisher = _context.Publishers.FirstOrDefault(u => u.Publisher_Id == id);
            if (publisher == null)
                return NotFound();

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
