using ShoeStore.DataAccess.Data;
using ShoeStore.DataAccess.Repository.IRepository;

namespace ShoeStore.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public IShoeRepository Shoe { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Shoe = new ShoeRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
