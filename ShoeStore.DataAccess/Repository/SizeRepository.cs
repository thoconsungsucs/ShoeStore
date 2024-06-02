using ShoeStore.DataAccess.Data;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models;

namespace ShoeStore.DataAccess.Repository
{
    public class SizeRepository : Repository<Size>, ISizeRepository
    {
        private readonly ApplicationDbContext _db;
        public SizeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
