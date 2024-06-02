using ShoeStore.Models;

namespace ShoeStore.DataAccess.Repository.IRepository
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        void Update(Discount Discount);
    }
}
