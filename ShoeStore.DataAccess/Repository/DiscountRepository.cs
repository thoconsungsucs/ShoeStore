using ShoeStore.DataAccess.Data;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models;

namespace ShoeStore.DataAccess.Repository
{
    public class DiscountRepository : Repository<Discount>, IDiscountRepository
    {
        private readonly ApplicationDbContext _db;
        public DiscountRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Discount Discount)
        {
            _db.Update(Discount);
        }
    }
}
