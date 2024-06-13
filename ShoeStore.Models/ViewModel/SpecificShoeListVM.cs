namespace ShoeStore.Models.ViewModel
{
    public class SpecificShoeListVM
    {
        public string ShoeName { get; set; }
        public Gender Gender { get; set; }
        public int TotalColors { get; set; }
        public double DiscountMax { get; set; }
        public int ShoeId { get; set; }
        public Dictionary<int, string>? ImageList { get; set; }
    }
}
