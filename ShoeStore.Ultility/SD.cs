using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShoeStore.Ultility
{
    public static class SD
    {
        public static List<SelectListItem> SizeList = new List<SelectListItem>
        {
            new SelectListItem {Text = "36", Value = "36"},
            new SelectListItem {Text = "37", Value = "37"},
            new SelectListItem {Text = "38", Value = "38"},
            new SelectListItem {Text = "39", Value = "39"},
            new SelectListItem {Text = "40", Value = "40"},
            new SelectListItem {Text = "41", Value = "41"},
            new SelectListItem {Text = "42", Value = "42"},
            new SelectListItem {Text = "43", Value = "43"},
            new SelectListItem {Text = "44", Value = "44"},
            new SelectListItem {Text = "45", Value = "45"},
        };

        public static List<SelectListItem> GenderList = new List<SelectListItem>
        {
            new SelectListItem {Text = "Men", Value = "0"},
            new SelectListItem {Text = "Women", Value = "1"},
            new SelectListItem {Text = "Kids", Value = "2"},
            new SelectListItem {Text = "Unisex", Value = "3"},
        };

        public static List<SelectListItem> PriceList = new List<SelectListItem>
        {
            new SelectListItem {Text = "0-100", Value = "0-100"},
            new SelectListItem {Text = "100-200", Value = "100-200"},
            new SelectListItem {Text = "200-300", Value = "200-300"},
            new SelectListItem {Text = "Over 300", Value = "300-100000"},
        };


    }
}
