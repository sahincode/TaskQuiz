using Microsoft.AspNetCore.Mvc;
using TaskPronia.Data;
using TaskPronia.Models;

namespace TaskPronia.Areas.Manage.Controllers
{

    [Area("Manage")]

    public class ColorController : Controller
    {
        private readonly AppDbContext _context;

        public ColorController(AppDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            List<Color> colors = _context.Colors.ToList();
            if (colors == null)
            {
                return NotFound();
            }
            return View(colors);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Color color)
        {
            if (!ModelState.IsValid)
                return View();
            if (_context.Colors.Any(x => (x.Name == color.Name)))
            {
                ModelState.AddModelError("Name", "this categorie is already exist");
                return View();
            }

                ;
            if (color == null) return NotFound();
            _context.Colors.Add(color);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {

            Color color = _context.Colors.FirstOrDefault(x => x.Id == id);

            if (color == null) return NotFound();

            return View(color);

        }
        [HttpPost]
        public IActionResult Update(Color color)
        {
            if (!ModelState.IsValid)
                return View();
            Color oldcolor = _context.Colors.FirstOrDefault(t => t.Id == color.Id);
            if (oldcolor == null)
            {
                return NotFound();
            }
            if (_context.Categories.Any(x => x.Id != color.Id && x.Name.Trim().ToLower() == color.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", "this categorie is already exist");
                return View();
            }

            oldcolor.Name = color.Name;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Color color = _context.Colors.FirstOrDefault(x => x.Id == id);
            if (color == null) return NotFound();
            return View(color);
        }
        [HttpPost]
        public IActionResult Delete(Color color)
        {

            _context.Colors .Remove(color);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
