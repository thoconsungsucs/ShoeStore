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
        public Gender Gender { get; set; }

        [Required]
        public int Size { get; set; }

        /*        [Required]
                [Display(Name = "Gender")]*/
        /*        public int GenderId { get; set; }
                [ValidateNever]
                public int Gender { get; set; }*/

        [Required]
        [Display(Name = "Color")]
        public int ColorId { get; set; }
        [ValidateNever]
        public virtual Color Color { get; set; }

        /*        public int SizeId { get; set; }*/
        /*        public virtual Size Size { get; set; }*/

        [Required]
        [Display(Name = "Discount")]
        public int DiscountId { get; set; }
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
        public ICollection<ShoeImage>? ImageShoes { get; set; }



    }
}
