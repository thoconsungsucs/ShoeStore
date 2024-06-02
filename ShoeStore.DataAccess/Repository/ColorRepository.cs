using ShoeStore.DataAccess.Data;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models;

namespace ShoeStore.DataAccess.Repository
{
    public class ColorRepository : Repository<Color>, IColorRepository
    {
        private readonly ApplicationDbContext _db;
        public ColorRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Color Color)
        {
            _db.Update(Color);
        }
    }
}
