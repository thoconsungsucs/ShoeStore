using ShoeStore.DataAccess.Data;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models;

namespace ShoeStore.DataAccess.Repository
{
    public class BagRepository : Repository<Bag>, IBagRepository
    {
        private readonly ApplicationDbContext _db;
        public BagRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Bag Bag)
        {
            _db.Update(Bag);
        }
    }
}
