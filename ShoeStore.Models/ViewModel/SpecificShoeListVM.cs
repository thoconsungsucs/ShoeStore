using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShoeStore.Models.ViewModel
{
    public class SpecificShoeListVM
    {
        public List<SpecificShoeWithImage> SpecificShoeList { get; set; }
        public IEnumerable<SelectListItem> ColorList { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }

    public class SpecificShoeWithImage
    {
        public string ShoeName { get; set; }
        public Gender Gender { get; set; }
        public double Price { get; set; }
        public int TotalColors { get; set; }
        public double DiscountMax { get; set; }
        public int ShoeId { get; set; }
        public IEnumerable<ColorImage> ImageList { get; set; }
    }

    public class ColorImage
    {
        public int ColorId { get; set; }
        public string ImageUrl { get; set; }
    }

    public class ColorList
    {
        public int ColorId { get; set; }
        public string ColorName { get; set; }
    }
}
