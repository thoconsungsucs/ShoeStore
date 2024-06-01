namespace ShoeStore.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IShoeRepository Shoe { get; }
        ICategoryRepository Category { get; }
        void Save();
    }
}
