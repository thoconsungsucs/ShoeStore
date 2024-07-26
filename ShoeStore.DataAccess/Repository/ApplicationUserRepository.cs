using ShoeStore.DataAccess.Data;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models;

namespace ShoeStore.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
