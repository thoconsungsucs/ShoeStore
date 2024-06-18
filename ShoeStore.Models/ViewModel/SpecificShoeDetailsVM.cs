namespace ShoeStore.Models.ViewModel
{
    public class SpecificShoeDetailsVM
    {
        public List<ColorShoeImage> ColorShoeIdWithImage { get; set; }
        public List<SpecificShoe> SpecificShoeListForSize { get; set; }
        public ColorShoe ColorShoe { get; set; }
    }
}
