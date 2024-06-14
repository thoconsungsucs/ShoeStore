using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ShoeStore.Models
{
    public class ColorShoe
    {
        public int ColorShoeId { get; set; }
        public int ShoeId { get; set; }
        public int ColorId { get; set; }
        [ValidateNever]
        public List<ShoeImage> Images { get; set; }
    }
}
