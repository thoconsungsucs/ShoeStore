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
        public int ShoeId { get; set; }
        [ValidateNever]
        public Shoe Shoe { get; set; }
        [Required]
        public int GenderId { get; set; }
        [ValidateNever]
        public int Gender { get; set; }
        [Required]
        public int ColorId { get; set; }
        [ValidateNever]
        public virtual Color Color { get; set; }
        [Required]
        public int SizeId { get; set; }
        [ValidateNever]
        public virtual Size Size { get; set; }
        [Required]
        public int DiscountId { get; set; }
        [ValidateNever]
        public Discount Discount { get; set; }
        public double Price { get; set; }
        [NotMapped]
        public ICollection<ImageShoe> ImageShoes { get; set; }
        [ForeignKey("GenderId, ColorId, SizeId, DiscountId")]
        [Required]
        public int Quantity { get; set; }
    }
}
