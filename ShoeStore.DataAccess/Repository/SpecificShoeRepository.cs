using ShoeStore.DataAccess.Data;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models;
using ShoeStore.Models.ViewModel;

namespace ShoeStore.DataAccess.Repository
{
    public class SpecificShoeRepository : Repository<SpecificShoe>, ISpecificShoeRepository
    {
        private readonly ApplicationDbContext _db;
        public SpecificShoeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(SpecificShoe specificShoe)
        {
            _db.Update(specificShoe);
        }

        public IEnumerable<SpecificShoeListVM> GetAllGroupByShoeAndGender()
        {
            // Get all specific shoes with number of color, max discount and image url each color
            // Get all shoe images
            var imgList = _db.ShoeImages;
            var specificShoeList = GetAll(includeProperties: "Shoe,Discount")
                        .GroupBy(s => new { s.Shoe.ShoeName, s.Gender, s.ShoeId })
                        .Select(group => new
                        {
                            ShoeName = group.Key.ShoeName,
                            Gender = group.Key.Gender,
                            TotalColor = group.Select(s => s.ColorId).Distinct().Count(),
                            DiscountMax = group.Select(s => s.Discount.DiscountValue).Max(),
                            ShoeId = group.Key.ShoeId,
                            ColorIds = group.Select(s => s.ColorId).Distinct()
                        }).ToList();

            // Get image url for each color
            var specificShoeListWithImg = specificShoeList
                        .Select(item => new SpecificShoeListVM
                        {
                            ShoeName = item.ShoeName,
                            Gender = item.Gender,
                            TotalColors = item.TotalColor,
                            DiscountMax = item.DiscountMax,
                            ShoeId = item.ShoeId,
                            ImageList = item.ColorIds.ToDictionary(
                                colorId => colorId,
                                colorId => imgList.FirstOrDefault(i => i.IsMain && i.ShoeId == item.ShoeId && colorId == i.ColorId)?.ImageUrl
                            )
                        }).ToList();
            return specificShoeListWithImg;
        }
    }
}
