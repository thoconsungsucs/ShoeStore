using ShoeStore.DataAccess.Data;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models;

namespace ShoeStore.DataAccess.Repository
{
    public class ImageShoeRepository : Repository<ImageShoe>, IImageShoeRepository
    {
        private readonly ApplicationDbContext _db;
        public ImageShoeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ImageShoe ImageShoe)
        {
            _db.Update(ImageShoe);
        }
    }
}
