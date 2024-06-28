using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    public class SpecificShoe
    {
        [Key]
        public int SpecificShoeId { get; set; }

        [Required]
        public int ColorShoeId { get; set; }
        [ForeignKey("ColorShoeId")]
        [ValidateNever]
        public ColorShoe ColorShoe { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public int Size { get; set; }

        [Required]
        [Display(Name = "Discount")]
        public int? DiscountId { get; set; }
        [ValidateNever]
        public Discount Discount { get; set; }

        [Required]
        [Display(Name = "Specific Price")]
        public double Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [ForeignKey("ColorId, DiscountId")]
        [NotMapped]
        [ValidateNever]
        public List<ShoeImage>? ImageShoes { get; set; }



    }
}
