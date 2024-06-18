using ShoeStore.DataAccess.Data;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models;
using ShoeStore.Models.ViewModel;

namespace ShoeStore.DataAccess.Repository
{
    public class ColorShoeRepository : Repository<ColorShoe>, IColorShoeRepository
    {
        private readonly ApplicationDbContext _db;
        public ColorShoeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ColorShoe colorShoe)
        {
            _db.Update(colorShoe);
        }

        public List<ColorShoeImage> GetColorShoeIdWithImage(int shoeId, Gender gender)
        {
            var colorShoeIdWithImage = _db.ColorShoes
                .Where(cs => cs.ShoeId == shoeId)
                .Join(_db.SpecificShoes.Where(ss => ss.Gender == gender),
                      cs => cs.ColorShoeId,
                      ss => ss.ColorShoeId,
                      (cs, ss) => new { cs, ss })
                            .GroupBy(result => result.cs.ColorShoeId)
                            .Select(result => new ColorShoeImage
                            {
                                ColorShoeId = result.Key,
                                ImageUrl = _db.ShoeImages.First(si => si.IsMain && si.ColorShoeId == result.Key).ImageUrl
                            }).ToList();
            return colorShoeIdWithImage;
        }



    }
}
