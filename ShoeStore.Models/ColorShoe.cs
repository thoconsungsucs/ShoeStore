using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    public class ColorShoe
    {
        public int ColorShoeId { get; set; }
        public int ShoeId { get; set; }
        [ForeignKey("ShoeId")]
        public Shoe Shoe { get; set; }
        public int ColorId { get; set; }
        [ForeignKey("ColorId")]
        public Color Color { get; set; }
        [ValidateNever]
        public List<ShoeImage> Images { get; set; }
    }
}
