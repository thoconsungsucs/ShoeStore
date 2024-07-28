using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    public class Bag
    {
        public int BagId { get; set; }
        public int SpecificShoeId { get; set; }
        [ForeignKey("SpecificShoeId")]
        [ValidateNever]
        public SpecificShoe? SpecificShoe { get; set; }
        public int Count { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
