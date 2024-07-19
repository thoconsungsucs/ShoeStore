using ShoeStore.Models;

namespace ShoeStore.DataAccess.Repository.IRepository
{
    public interface IBagRepository : IRepository<Bag>
    {
        void Update(Bag bag);
    }
}
