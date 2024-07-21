using ShoeStore.DataAccess.Data;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models;
using ShoeStore.Models.ViewModel;
namespace ShoeStore.DataAccess.Repository
{
    public class BagRepository : Repository<Bag>, IBagRepository
    {
        private readonly ApplicationDbContext _db;
        public BagRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Bag Bag)
        {
            _db.Update(Bag);
        }

        public BagVM GetAll(string userId)
        {

            var bags = new BagVM
            {
                Bags = _db.Bags.Where(b => b.ApplicationUserId == userId).Select(b => new Bag
                {
                    BagId = b.BagId,
                    SpecificShoe = new SpecificShoe
                    {
                        ColorShoe = new ColorShoe
                        {
                            Color = new Color
                            {
                                ColorName = b.SpecificShoe.ColorShoe.Color.ColorName
                            },
                            Shoe = new Shoe
                            {
                                ShoeName = b.SpecificShoe.ColorShoe.Shoe.ShoeName
                            },
                        },
                        Gender = b.SpecificShoe.Gender,
                        Price = b.SpecificShoe.Price,
                        Discount = new Discount
                        {
                            DiscountValue = b.SpecificShoe.Discount.DiscountValue
                        },
                        Size = b.SpecificShoe.Size,
                        ImageShoes = _db.ShoeImages.Where(i => i.IsMain && i.ColorShoeId == b.SpecificShoe.ColorShoeId).ToList()
                    },
                    Count = b.Count
                }).ToList(),
                OrderHeader = new OrderHeader
                {
                    OrderTotal = _db.Bags.Where(b => b.ApplicationUserId == userId).Sum(b => (1 - b.SpecificShoe.Discount.DiscountValue) * b.SpecificShoe.Price * b.Count)
                }
            };
            return bags;
        }
    }
}
