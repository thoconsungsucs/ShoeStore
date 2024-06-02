using ShoeStore.Models;

namespace ShoeStore.DataAccess.Repository.IRepository
{
    public interface IImageShoeRepository : IRepository<ImageShoe>
    {
        void Update(ImageShoe ImageShoe);
    }
}
