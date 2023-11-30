using Microsoft.AspNetCore.Mvc;
using TaskPronia.Data;
using TaskPronia.Models;

namespace TaskPronia.Areas.Manage.Controllers
{

    [Area("Manage")]

    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            List<Category> categories = _context.Categories.ToList();
            if (categories == null)
            {
                return NotFound();
            }
            return View(categories);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Category categorie)
        {
            if (!ModelState.IsValid)
                return View();
            if (_context.Categories.Any(x => (x.Name == categorie.Name)))
            {
                ModelState.AddModelError("Name", "this categorie is already exist");
                return View();
            }

                ;
            if (categorie == null) return NotFound();
            _context.Categories.Add(categorie);
            _context.SaveChanges();
            return RedirectToAction("Index");






        }
        public IActionResult Update(int id)
        {

            Category categorie = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (categorie == null) return NotFound();

            return View(categorie);

        }
        [HttpPost]
        public IActionResult Update(Category categorie)
        {
            if (!ModelState.IsValid)
                return View();
            Category odlcategorie = _context.Categories.FirstOrDefault(t => t.Id == categorie.Id);
            if (odlcategorie == null)
            {
                return NotFound();
            }
            if (_context.Categories.Any(x => x.Id != categorie.Id && x.Name.Trim().ToLower() == categorie.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", "this categorie is already exist");
                return View();
            }

            odlcategorie.Name = categorie.Name;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public IActionResult Delete(int id)
        {

            Category categorie = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (categorie == null) return NotFound();
            _context.Categories.Remove(categorie);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

