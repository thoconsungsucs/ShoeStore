using ShoeStore.DataAccess.Data;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models;

namespace ShoeStore.DataAccess.Repository
{
    public class GenderRepository : Repository<Gender>, IGenderRepository
    {
        private readonly ApplicationDbContext _db;
        public GenderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


    }
}
