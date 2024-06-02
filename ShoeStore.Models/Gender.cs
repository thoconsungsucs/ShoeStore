using System.ComponentModel.DataAnnotations;

namespace ShoeStore.Models
{
    public class Gender
    {
        [Key]
        public int GenderId { get; set; }
        [Display(Name = "Gender")]
        public string GenderName { get; set; }
    }
}
