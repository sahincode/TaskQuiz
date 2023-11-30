using TaskPronia.Models;

namespace TaskPronia.ViewModel
{
    public class HomeViewModel
    {
        public List<Product> ProductsIsBestSeller { get; set; }
        public List<Product> ProductsIsFeatured{ get; set; }

        public List<Product> ProductsISNew { get; set; }
    }
}
