using TaskPronia.Models;

namespace TaskPronia.Service
{
    public static class Helper
    {
        public static string SaveFile(string rooPath, string stagePath, IFormFile file)
        {
            string fileName = file.FileName;
            if (fileName.Length > 64)
            {
                fileName = fileName.Substring(fileName.Length - 64, 64);
            }
            fileName = Guid.NewGuid().ToString() + fileName;
            string fullPath = Path.Combine(rooPath, stagePath, fileName);
            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return fileName;



        }

        public static void UpdateProduct(Product oldProduct, Product product)
        {

            oldProduct.Name = product.Name;
            oldProduct.Desc = product.Desc;
            oldProduct.Cost = product.Cost;
            oldProduct.Size = product.Size;
            oldProduct.IsNew = product.IsNew;
            oldProduct.IsFeatured = product.IsFeatured;
            oldProduct.Code = product.Code;


            oldProduct.IsBestSeller = product.IsBestSeller;
        }
    }
}
