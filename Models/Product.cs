using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskPronia.Models
{
    public class Product :BaseEntity
    {
        [Required]
        [StringLength(maximumLength: 55)]
        public string Name { get;set; }
        [Required]
        [StringLength(maximumLength: 16)]
        public string Code { get; set; }
        public double Cost { get; set; }
        [Required]
        [StringLength(maximumLength: 15)]
        public string Size { get; set; }
        [Required]
        [StringLength(maximumLength: 355)]
        public string Desc { get; set; }
        public bool IsNew { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsBestSeller { get; set; }
        public int CategoryId { get; set; }
        public Category ? Category { get; set; }
        public List<Image>? Images { get; set; }
        public List<ProductColor> ? Colors { get; set; }
        [NotMapped]
        public IFormFile? HoverImage { get; set; }
        [NotMapped]

        public IFormFile ?  CardImage { get; set; }
        [NotMapped]
        public List< IFormFile> ?  SlideImages { get; set; }

        [NotMapped]
        public List<int> ? ImageIds { get; set; }
        [NotMapped]
        public List<int> ColorIds { get; set; }



    }
}
