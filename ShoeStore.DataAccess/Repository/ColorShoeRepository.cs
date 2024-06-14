using ShoeStore.DataAccess.Data;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models;

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
    }
}
