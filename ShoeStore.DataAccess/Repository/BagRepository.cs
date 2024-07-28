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
            var dateNow = DateOnly.FromDateTime(DateTime.Now);
            var bags = new BagVM
            {
                Bags = _db.Bags
                .Where(b => b.ApplicationUserId == userId)
                .Select(b => new KeyValuePair<Bag, bool>(
                    new Bag
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
                                DiscountValue = b.SpecificShoe.Discount.StartDate <= dateNow && b.SpecificShoe.Discount.EndDate >= dateNow && b.SpecificShoe.Discount.Active ?
                                    b.SpecificShoe.Discount.DiscountValue : 0
                            },
                            Size = b.SpecificShoe.Size,
                            ImageShoes = _db.ShoeImages.Where(i => i.IsMain && i.ColorShoeId == b.SpecificShoe.ColorShoeId).ToList()
                        },
                        Count = b.Count,
                        SpecificShoeId = b.SpecificShoeId
                    },
                    b.Count < b.SpecificShoe.Quantity
                )).ToList(),
                OrderHeader = new OrderHeader
                {
                    OrderTotal = _db.Bags
                            .Where(b => b.ApplicationUserId == userId)
                            .Sum(b => b.Count < b.SpecificShoe.Quantity ?
                                (1 - (b.SpecificShoe.Discount.StartDate <= dateNow && b.SpecificShoe.Discount.EndDate >= dateNow && b.SpecificShoe.Discount.Active ? b.SpecificShoe.Discount.DiscountValue : 0)) * b.SpecificShoe.Price * b.Count : 0)
                }
            };

            return bags;
        }
    }
}
