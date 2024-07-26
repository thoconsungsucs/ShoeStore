using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        [Required]
        public int OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }
        [Required]
        public int SpecificShoeId { get; set; }
        [ForeignKey("SpecificShoeId")]
        [ValidateNever]
        public SpecificShoe SpecificShoe { get; set; }
        public int Quantity { get; set; }

    }
}