using ShoeStore.Models;
using ShoeStore.Models.ViewModel;

namespace ShoeStore.DataAccess.Repository.IRepository
{
    public interface ISpecificShoeRepository : IRepository<SpecificShoe>
    {
        void Update(SpecificShoe specificShoe);
        /*        IEnumerable<SpecificShoeListVM> GetAllGroupByShoeAndGender();*/
        List<SpecificShoeWithImage> GetSpecificShoeWithImage(List<int>? categories = null, List<Gender>? genders = null, List<string>? prices = null, List<int>? sizes = null, List<int>? colors = null);
        List<SpecificShoe> GetSpecificShoeListForSize(int colorShoeId, Gender gender);
    }
}
