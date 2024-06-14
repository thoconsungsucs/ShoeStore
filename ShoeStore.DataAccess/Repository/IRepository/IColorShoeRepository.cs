using ShoeStore.Models;

namespace ShoeStore.DataAccess.Repository.IRepository
{
    public interface IColorShoeRepository : IRepository<ColorShoe>
    {
        void Update(ColorShoe colorShoe);
    }
}
