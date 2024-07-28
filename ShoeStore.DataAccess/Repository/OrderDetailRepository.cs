using ShoeStore.DataAccess.Data;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models;
namespace ShoeStore.DataAccess.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderDetail orderDetail)
        {
            _db.Update(orderDetail);
        }

        public List<OrderDetail> GetAllSpecificShoe(int orderHeaderId)
        {
            var purchasedShoes = _db.OrderDetails
                .Where(od => od.OrderHeaderId == orderHeaderId)
                .Select(od => new OrderDetail
                {
                    SpecificShoe = new SpecificShoe
                    {
                        ColorShoe = new ColorShoe
                        {
                            Color = new Color
                            {
                                ColorName = od.SpecificShoe.ColorShoe.Color.ColorName
                            },
                            Shoe = new Shoe
                            {
                                ShoeName = od.SpecificShoe.ColorShoe.Shoe.ShoeName
                            },
                        },
                        Gender = od.SpecificShoe.Gender,
                        Price = od.SpecificShoe.Price,
                        Discount = new Discount
                        {
                            DiscountValue = od.SpecificShoe.Discount.DiscountValue
                        },
                        Size = od.SpecificShoe.Size,
                        ImageShoes = _db.ShoeImages.Where(i => i.IsMain && i.ColorShoeId == od.SpecificShoe.ColorShoeId).ToList()
                    },
                    Price = od.Price,
                    Quantity = od.Quantity,
                    SpecificShoeId = od.SpecificShoeId
                });
            return purchasedShoes.ToList();
        }
    }
}
