using ShoeStore.DataAccess.Data;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models;

namespace ShoeStore.DataAccess.Repository
{
    public class ShoeImageRepository : Repository<ShoeImage>, IShoeImageRepository
    {
        private readonly ApplicationDbContext _db;
        public ShoeImageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ShoeImage ImageShoe)
        {
            _db.Update(ImageShoe);
        }
    }
}
