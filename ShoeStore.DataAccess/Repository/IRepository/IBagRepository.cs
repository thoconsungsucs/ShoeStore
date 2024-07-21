using ShoeStore.Models;
using ShoeStore.Models.ViewModel;

namespace ShoeStore.DataAccess.Repository.IRepository
{
    public interface IBagRepository : IRepository<Bag>
    {
        void Update(Bag bag);

        BagVM GetAll(string userId);
    }
}
