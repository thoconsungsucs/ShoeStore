using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    public class Shoe
    {
        [Key]
        public int ShoeId { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "Shoe Name")]
        public string ShoeName { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(1, 10000)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Category Id")]
        public int CategoryId { get; set; }
        // Define a foreign key relationship
        // The CategoryId property is a foreign key to the Category table
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
    }
}
