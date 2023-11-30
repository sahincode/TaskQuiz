using TaskPronia.Models;

namespace TaskPronia.Service
{
    public  static class Helper
    {
        public static string SaveFile (string rootPath ,string stagePath , IFormFile file)
        {
           string filepath = Path.Combine (rootPath , stagePath);
            string imageName = file.FileName;
            if (imageName.Length > 64)
            {
                imageName= imageName.Substring(imageName.Length-64 ,64);

            }
            imageName = Guid.NewGuid().ToString()+imageName;
            string fullPath =Path.Combine (filepath, imageName);
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return imageName;


        }
         public static void  UpdateProduct(Product oldProduct ,Product product)
        {

            oldProduct.Name = product.Name;
            oldProduct.Desc = product.Desc;
            oldProduct.Cost = product.Cost;
            oldProduct.Size = product.Size;
            oldProduct.IsNew = product.IsNew;
            oldProduct.IsFeatured = product.IsFeatured;
            oldProduct.Code= product.Code;


            oldProduct.IsBestSeller = product.IsBestSeller;
        }
     }
}
