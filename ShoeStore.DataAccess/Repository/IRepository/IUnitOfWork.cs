namespace ShoeStore.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IShoeRepository Shoe { get; }
        ICategoryRepository Category { get; }
        IColorRepository Color { get; }
        IImageShoeRepository ImageShoe { get; }
        /*        IGenderRepository Gender { get; }
                ISizeRepository Size { get; }*/
        IDiscountRepository Discount { get; }
        ISpecificShoeRepository SpecificShoe { get; }
        void Save();
    }
}
