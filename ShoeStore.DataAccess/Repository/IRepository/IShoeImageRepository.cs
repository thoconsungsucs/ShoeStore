using ShoeStore.Models;

namespace ShoeStore.DataAccess.Repository.IRepository
{
    public interface IShoeImageRepository : IRepository<ShoeImage>
    {
        void Update(ShoeImage ImageShoe);
    }
}
