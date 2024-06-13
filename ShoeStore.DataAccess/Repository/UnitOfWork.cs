using ShoeStore.DataAccess.Data;
using ShoeStore.DataAccess.Repository.IRepository;

namespace ShoeStore.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public IShoeRepository Shoe { get; private set; }
        public IColorRepository Color { get; private set; }
        public IShoeImageRepository ShoeImage { get; private set; }
        /*        public IGenderRepository Gender { get; private set; }
                public ISizeRepository Size { get; private set; }*/
        public IDiscountRepository Discount { get; private set; }
        public ISpecificShoeRepository SpecificShoe { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Shoe = new ShoeRepository(_db);
            Color = new ColorRepository(_db);
            ShoeImage = new ShoeImageRepository(_db);
            /*            Gender = new GenderRepository(_db);
                        Size = new SizeRepository(_db);*/
            Discount = new DiscountRepository(_db);
            SpecificShoe = new SpecificShoeRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
