using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShoeStore.Models.ViewModel
{
    public class SpecificShoeDetailsVM
    {
        public List<ColorShoeImage> ColorShoeIdWithImage { get; set; }
        public List<SpecificShoe> SpecificShoeListForSize { get; set; }
        public ColorShoe ColorShoe { get; set; }
        public SpecificShoe SpecificShoe { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> GenderList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> DiscountList { get; set; }
    }
}
