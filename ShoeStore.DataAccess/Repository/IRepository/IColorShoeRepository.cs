using ShoeStore.Models;
using ShoeStore.Models.ViewModel;

namespace ShoeStore.DataAccess.Repository.IRepository
{
    public interface IColorShoeRepository : IRepository<ColorShoe>
    {
        void Update(ColorShoe colorShoe);

        List<ColorShoeImage> GetColorShoeIdWithImage(int shoeId, Gender gender);

    }
}
