using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    public class ShoeImage
    {
        [Key]
        public int ShoeImageId { get; set; }
        public int ColorShoeId { get; set; }
        [ForeignKey("ColorShoeId")]
        public ColorShoe ColorShoe { get; set; }
        public string ImageUrl { get; set; }

        public bool IsMain { get; set; }
    }
}
