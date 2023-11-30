namespace TaskPronia.Models
{
    public class Image:BaseEntity
    {
         public string Link { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
         public bool ? IsHover { get; set; }
    }
}
