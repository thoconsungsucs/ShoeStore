using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShoeStore.Models.ViewModel
{
    public class SpecificShoeVM
    {
        public SpecificShoe SpecificShoe { get; set; }
        [ValidateNever]
        public Shoe Shoe { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> GenderList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> SizeList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ColorList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> DiscountList { get; set; }
        public IEnumerable<int> SizeSelected { get; set; }
    }
}
