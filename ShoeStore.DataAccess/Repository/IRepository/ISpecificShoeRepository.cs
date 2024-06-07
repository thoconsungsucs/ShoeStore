using ShoeStore.Models;

namespace ShoeStore.DataAccess.Repository.IRepository
{
    public interface ISpecificShoeRepository : IRepository<SpecificShoe>
    {
        void Update(SpecificShoe specificShoe);
    }
}
