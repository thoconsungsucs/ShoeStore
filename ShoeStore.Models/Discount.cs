using System.ComponentModel.DataAnnotations;

namespace ShoeStore.Models
{
    public class Discount
    {
        [Key]
        public int DiscountId { get; set; }
        [Display(Name = "Discount Name")]
        public string DiscountName { get; set; }
        [Range(0, 1)]
        [Display(Name = "Discount Value")]
        public double DiscountValue { get; set; }
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateOnly StartDate { get; set; }
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateOnly EndDate { get; set; }
        public bool Active { get; set; }
    }
}
