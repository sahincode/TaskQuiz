using Microsoft.AspNetCore.Mvc;
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
                ProductsIsBestSeller = _context.Products.Where(p => p.IsBestSeller == true).ToList(),
                ProductsIsFeatured = _context.Products.Where(p => p.IsFeatured == true).ToList(),

                ProductsISNew = _context.Products.Where(p => p.IsNew == true).ToList()
            };
            return View(model);
        }

      
    }
}
