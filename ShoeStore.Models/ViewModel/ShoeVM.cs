using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShoeStore.Models.ViewModel
{
    public class ShoeVM
    {
        public Shoe Shoe { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> ColorShoeList { get; set; }
    }
}
