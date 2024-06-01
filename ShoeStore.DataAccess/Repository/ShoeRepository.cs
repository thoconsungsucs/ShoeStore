using ShoeStore.DataAccess.Data;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models;

namespace ShoeStore.DataAccess.Repository
{
    public class ShoeRepository : Repository<Shoe>, IShoeRepository
    {
        private readonly ApplicationDbContext _db;
        public ShoeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Shoe shoe)
        {
            _db.Update(shoe);
        }
    }
}
