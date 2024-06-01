using System.ComponentModel.DataAnnotations;

namespace ShoeStore.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        public int DisplayOrder { get; set; }
        public bool Active { get; set; }
        public DateOnly DateUpdated { get; set; }
        public ICollection<Shoe> Shoes { get; set; }
    }
}
