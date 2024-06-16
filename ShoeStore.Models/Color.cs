using System.ComponentModel.DataAnnotations;

namespace ShoeStore.Models
{
    public class Color
    {
        [Key]
        public int ColorId { get; set; }
        [Required]
        [Display(Name = "Color Name")]
        [StringLength(50)]
        public string ColorName { get; set; }
        public bool Active { get; set; }
        [DisplayFormat(DataFormatString = "{0:dddd, dd/MM/yyyy }", ApplyFormatInEditMode = true)]
        public DateOnly DateUpdated { get; set; }
    }
}
