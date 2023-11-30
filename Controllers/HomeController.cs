using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TaskPronia.Data;
using TaskPronia.Models;
using TaskPronia.ViewModel;

namespace TaskPronia.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {

            HomeViewModel model = new HomeViewModel()
            {
                ProductsIsBestSeller = _context.Products.Include(p=>p.Images).Where(p => p.IsBestSeller == true).ToList(),
                ProductsIsFeatured = _context.Products.Include(p => p.Images).Where(p => p.IsFeatured == true).ToList(),

<<<<<<< HEAD
                ProductsISNew = _context.Products.Include(p => p.Images).Where(p => p.IsNew == true).ToList()
=======
                ProductsISNew = _context.Products.Where(p => p.IsNew == true).ToList()
>>>>>>> 0b4a00e8167a0927c8c336d2386b078a47ed82c4
            };
            return View(model);
        }

      
    }
}
