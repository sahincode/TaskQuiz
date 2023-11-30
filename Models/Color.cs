using System.ComponentModel.DataAnnotations;

namespace TaskPronia.Models 
{ 
    public class Color :BaseEntity
    {
        [Required]
        [StringLength(maximumLength: 55)]
         public string Name { get; set; }
        public List<ProductColor>? Products { get; set; }

    }
}
