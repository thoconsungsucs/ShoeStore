namespace ShoeStore.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IShoeRepository Shoe { get; }
        ICategoryRepository Category { get; }
        IColorRepository Color { get; }
        IShoeImageRepository ShoeImage { get; }
        IBagRepository Bag { get; }
        IDiscountRepository Discount { get; }
        ISpecificShoeRepository SpecificShoe { get; }
        IColorShoeRepository ColorShoe { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailRepository OrderDetail { get; }
        void Save();
    }
}
