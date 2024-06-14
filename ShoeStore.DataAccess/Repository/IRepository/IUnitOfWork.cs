namespace ShoeStore.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IShoeRepository Shoe { get; }
        ICategoryRepository Category { get; }
        IColorRepository Color { get; }
        IShoeImageRepository ShoeImage { get; }
        /*        IGenderRepository Gender { get; }
                ISizeRepository Size { get; }*/
        IDiscountRepository Discount { get; }
        ISpecificShoeRepository SpecificShoe { get; }
        IColorShoeRepository ColorShoe { get; }
        void Save();
    }
}
