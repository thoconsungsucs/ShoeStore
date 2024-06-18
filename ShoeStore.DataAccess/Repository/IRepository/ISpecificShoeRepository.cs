using ShoeStore.Models;
using ShoeStore.Models.ViewModel;

namespace ShoeStore.DataAccess.Repository.IRepository
{
    public interface ISpecificShoeRepository : IRepository<SpecificShoe>
    {
        void Update(SpecificShoe specificShoe);
        /*        IEnumerable<SpecificShoeListVM> GetAllGroupByShoeAndGender();*/
        List<SpecificShoeWithImage> Test();
        List<SpecificShoe> GetSpecificShoeListForSize(int colorShoeId, Gender gender);
    }
}
