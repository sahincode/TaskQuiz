using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using TaskPronia.Data;
using TaskPronia.Models;
using TaskPronia.Service;
using static System.Net.Mime.MediaTypeNames;
using Image = TaskPronia.Models.Image;

namespace TaskProject01._01._2023.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;

        public ProductController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this._context = context;
            this._webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> products = _context.Products.ToList();
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }
        public IActionResult Create()
        {
            ViewBag.Colors = _context.Colors.ToList();
            ViewBag.Categories = _context.Categories.ToList();

            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {

            string rootPath = _webHostEnvironment.WebRootPath;
            string stagePath = "image/product";

            bool IsColor = true;
            ViewBag.Colors = _context.Colors.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            if (product.HoverImage==null)
            {
                ModelState.AddModelError("HoverImage", " can not be null  ");
                return View();
            }
            if (product.CardImage == null)
            {
                ModelState.AddModelError("CardImage", " can not be null  ");
                return View();
            }
            if (product.SlideImages == null)
            {
                ModelState.AddModelError("SlideImages", " can not be null  ");
                return View();
            }



            if (product.HoverImage.ContentType != "image/png" && product.HoverImage.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("Image", " please download png or jpeg ");
                return View();
            }
            if (product.CardImage.ContentType != "image/png" && product.CardImage.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("Image", " please download png or jpeg");
                return View();

            }
            foreach (var slideimage in product.SlideImages)
            {
                if (slideimage.ContentType != "image/png" && slideimage.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("Image", " please download png or jpeg");
                    return View();

                }


            }
            if (product.ColorIds != null)
            {
                foreach (var tagId in product.ColorIds)
                {
                    if (!_context.Colors.Any(t => t.Id == tagId))
                    {
                        IsColor = false;

                        break;
                    }
                }
            }
            if (IsColor)
            {
                Image bookImage = new Image()
                {
                    Product = product,
                    Link = Helper.SaveFile(rootPath, stagePath, product.HoverImage),
                    IsHover=true
                };
                _context.Images.Add(bookImage);
                foreach (var colorId in product.ColorIds)
                {
                    ProductColor color = new ProductColor()
                    {
                        Product = product,
                        ColorId = colorId

                    };
                    _context.ProductColors.Add(color);
                }


                Image image = new Image()
                {
                    Product = product,
                    Link = Helper.SaveFile(rootPath, stagePath, product.CardImage),
                    IsHover=false
                };
                _context.Images.Add(image);
                foreach (var simage in product.SlideImages)
                {
                    Image slideImage = new Image()
                    {
                        Product = product,
                        Link = Helper.SaveFile(rootPath, stagePath, simage),
                        IsHover=null
                    };
                    _context.Images.Add(slideImage);



                }
                _context.Products.Add(product);
                _context.SaveChanges();


                return RedirectToAction("Index");

            }
            else
            {
                ModelState.AddModelError("TagIds", " Tag  does not  exist");
                return View();




            }

        }
        public IActionResult Update(int id)
        {

            ViewBag.Colors = _context.Colors.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            Product product = _context.Products.Include(p => p.Colors).Include(p=>p.Images).FirstOrDefault(x => x.Id == id);
            if (product == null)
                return NotFound();
            product.ColorIds = product.Colors.Select(x => x.Id).ToList();
            product.ImageIds = product.Images.Where(i => i.IsHover == null).Select(x => x.Id).ToList();

            return View(product);
        }
        [HttpPost]
        public IActionResult Update(Product product)
        {

            string rootPath = _webHostEnvironment.WebRootPath;
            string stagePath = "image/product";

            bool IsColor = true;
            ViewBag.Colors = _context.Colors.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            Product oldProduct = _context.Products.Include(b => b.Images).Include(b => b.Colors).FirstOrDefault(x => x.Id == product.Id);
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            if (product.ColorIds != null)
            {
                foreach (var colorId in product.ColorIds)
                {
                    if (!_context.Colors.Any(t => t.Id == colorId))
                    {
                        IsColor = false;

                        break;
                    }
                }
            }
            if (IsColor)
            {
                oldProduct.Colors.RemoveAll(ot => ! product.ColorIds.Any(ti => ti == ot.ColorId));
                foreach (var colorId in product.ColorIds.Where(ti => !oldProduct.Colors.Any(x => ti == x.ColorId)))
                {
                    ProductColor color = new ProductColor()
                    {
                        ColorId = colorId
                    };
                    oldProduct.Colors.Add(color);

                }


                List<Image> deleteImage = oldProduct.Images.FindAll(i => !product.ImageIds.
                Any(imgId => imgId == i.Id) && i.IsHover == null);
                oldProduct.Images.RemoveAll(i => !product.ImageIds.Any(imgId => imgId == i.Id) && i.IsHover == null);
                foreach (var image in deleteImage)
                {
                    System.IO.File.Delete(rootPath + "/" + stagePath + "/" + image.Link);
                }
                if (IsColor)
                {
                    if (product.HoverImage != null)
                    {
                        if (product.HoverImage.ContentType != "image/png" && product.HoverImage.ContentType != "image/jpeg")
                        {
                            ModelState.AddModelError("Image", " please download png or jpeg ");
                            return View();
                        }
                        Image deletedHoverImage = oldProduct.Images.FirstOrDefault(i => i.IsHover == true);
                        _context.Images.Remove(deletedHoverImage);

                        System.IO.File.Delete(rootPath + "/" + stagePath + "/" + deletedHoverImage.Link);

                        Image hoverImage = new Image()
                        {
                            ProductId = oldProduct.Id,
                            Link = Helper.SaveFile(rootPath, stagePath, product.HoverImage),
                            IsHover = true
                        };
                        _context.Images.Add(hoverImage);

                    }

                    if (product.CardImage != null)
                    {
                        if (product.CardImage.ContentType != "image/png" && product.CardImage.ContentType != "image/jpeg")
                        {
                            ModelState.AddModelError("Image", " please download png or jpeg ");
                            return View();
                        }

                        Image deletedCardImage = oldProduct.Images.FirstOrDefault(i => i.IsHover == false);
                        _context.Images.Remove(deletedCardImage);

                        System.IO.File.Delete(rootPath + "/" + stagePath + "/" + deletedCardImage.Link);
                        Image cardImage = new Image()
                        {
                            ProductId = oldProduct.Id,
                            Link = Helper.SaveFile(rootPath, stagePath, product.CardImage),
                            IsHover=false
                        };
                        _context.Images.Add(cardImage);

                    }
                    if (product.SlideImages != null)
                    {
                        foreach (var slideimage in product.SlideImages)
                        {
                            if (slideimage.ContentType != "image/png" && slideimage.ContentType != "image/jpeg")
                            {
                                ModelState.AddModelError("Image", " please download png or jpeg");
                                return View();

                            }




                        }
                        foreach (var slideimage in product.SlideImages)
                        {
                            Image slideImage = new Image()
                            {
                                ProductId = oldProduct.Id,
                                Link = Helper.SaveFile(rootPath, stagePath, slideimage) ,
                                IsHover=null
                            };
                            _context.Images.Add(slideImage);




                        }
                    }

                    foreach (var colorId in product.ColorIds)
                    {
                        ProductColor color = new ProductColor()
                        {
                            ProductId =  oldProduct.Id,
                            ColorId = colorId,

                        };
                        _context.ProductColors.Add(color);
                    }









                }
                else
                {
                    ModelState.AddModelError("TagIds", " Tag  does not  exist");
                    return View();




                }
               

            }
            Helper.UpdateProduct(oldProduct, product);

            _context.SaveChanges();
            return RedirectToAction("index");

        }
        public IActionResult Delete(int id)
        {
            string rootPath = _webHostEnvironment.WebRootPath;
            string stagePath = "image\\product";


            Product deletedProduct = _context.Products.Include(b => b.Colors).
                Include(b => b.Images).FirstOrDefault(b => b.Id == id);


            if (deletedProduct == null) return NotFound();


            if (deletedProduct.Colors != null)
            {
                foreach (var productColor in deletedProduct.Colors)
                {
                    _context.ProductColors.Remove(productColor);

                }
                _context.SaveChanges();
            }
            if (deletedProduct.Images != null)
            {
                foreach (var image in deletedProduct.Images)
                {
                    _context.Images.Remove(image);
                    System.IO.File.Delete(rootPath + "/" + stagePath + "/" + image.Link);

                }
                _context.SaveChanges();
            }





            _context.Products.Remove(deletedProduct);

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
