using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    public class ShoeImage
    {
        [Key]
        public int ShoeImageId { get; set; }
        public string ImageUrl { get; set; }
        public int ShoeId { get; set; }
        [ForeignKey("ShoeId")]
        public Shoe Shoe { get; set; }
        public int ColorId { get; set; }
        [ForeignKey("ColorId")]
        public Color Color { get; set; }
        public bool IsMain { get; set; }
    }
}
