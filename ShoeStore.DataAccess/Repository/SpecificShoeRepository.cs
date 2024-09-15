using Microsoft.EntityFrameworkCore;
using ShoeStore.DataAccess.Data;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models;
using ShoeStore.Models.ViewModel;
using System.Linq.Expressions;

namespace ShoeStore.DataAccess.Repository
{
    public class SpecificShoeRepository : Repository<SpecificShoe>, ISpecificShoeRepository
    {
        private readonly ApplicationDbContext _db;
        public SpecificShoeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(SpecificShoe specificShoe)
        {
            _db.Update(specificShoe);
        }

        public List<SpecificShoeWithImage> GetSpecificShoeWithImage(List<int>? categories = null, List<Gender>? genders = null, List<string>? prices = null, List<int>? sizes = null, List<int>? colors = null)
        {

            var shoeColorShoeList = _db.Set<Shoe>()
                .Join(
                      _db.Set<ColorShoe>().Where(ss => colors == null || colors.Contains(ss.ColorId)),
                      s => s.ShoeId,
                      cs => cs.ShoeId,
                      (s, cs) => new { s.ShoeId, s.ShoeName, s.CategoryId, s.Price, cs.ColorShoeId }
                 );
            // Filter category
            if (categories != null)
            {
                shoeColorShoeList = shoeColorShoeList.Where(s => categories.Contains(s.CategoryId));
            }

            var dateNow = DateOnly.FromDateTime(DateTime.Now);
            var discountList = _db.Discounts.Where(d => d.StartDate <= dateNow && d.EndDate >= dateNow && d.Active);

            var specificShoeList = _db.SpecificShoes
                .Where(ss => genders == null || genders.Contains(ss.Gender)) // Filter by gender
                .Where(ss => sizes == null || sizes.Contains(ss.Size)) // Filter by size
                                                                       //Left join
                .GroupJoin(
                    discountList,
                    ss => ss.DiscountId,
                    d => d.DiscountId,
                    (ss, d) => new { ss, d }
                )
                .SelectMany(
                    temp => temp.d.DefaultIfEmpty(),
                    (temp, d) => new SpecificShoe
                    {
                        SpecificShoeId = temp.ss.SpecificShoeId,
                        ColorShoeId = temp.ss.ColorShoeId,
                        Gender = temp.ss.Gender,
                        Size = temp.ss.Size,
                        Quantity = temp.ss.Quantity,
                        Price = temp.ss.Price,
                        Discount = d,
                        DiscountId = temp.ss.DiscountId,
                    }
                );

            //Filter by range of prices
            if (prices != null && prices.Any())
            {
                var predicate = PredicateBuilder.False<SpecificShoe>();
                foreach (var price in prices)
                {
                    var parts = price.Split('-');
                    var left = double.Parse(parts[0]);
                    var right = double.Parse(parts[1]);

                    predicate = predicate.Or(ss => ss.Price >= left && ss.Price <= right);
                }

                specificShoeList = specificShoeList.Where(predicate);
            }

            var list = specificShoeList
                .Join(
                      shoeColorShoeList,
                      specificShoe => specificShoe.ColorShoeId,
                      shoeColorShoe => shoeColorShoe.ColorShoeId,
                      (specificShoe, shoeColorShoe) => new
                      {
                          SpecificShoe = specificShoe,
                          ShoeColorShoe = shoeColorShoe
                      }
               ).GroupBy(specificShoeShoeColorShoe => new
               {
                   specificShoeShoeColorShoe.SpecificShoe.Gender,
                   specificShoeShoeColorShoe.ShoeColorShoe.ShoeId
               }
               ).Select(group => new SpecificShoeWithImage
               {
                   ShoeId = group.Key.ShoeId,
                   ShoeName = group.First().ShoeColorShoe.ShoeName,
                   Gender = group.Key.Gender,
                   Price = group.First().ShoeColorShoe.Price,
                   TotalColors = group.GroupBy(specificShoeShoeColorShoe => specificShoeShoeColorShoe.SpecificShoe.ColorShoeId).Count(),
                   DiscountMax = group.Max(specificShoeShoeColorShoe => specificShoeShoeColorShoe.SpecificShoe.Discount != null ? specificShoeShoeColorShoe.SpecificShoe.Discount.DiscountValue : 0),
                   ImageList = group.GroupBy(specificShoeShoeColorShoe => specificShoeShoeColorShoe.SpecificShoe.ColorShoeId).Select(x => new ColorShoeImage
                   {
                       ColorShoeId = x.Key,
                       ImageUrl = _db.ShoeImages.First(i => i.ColorShoeId == x.Key && i.IsMain).ImageUrl
                   })
               })
                .ToList();
            return list;
        }

        public List<SpecificShoe> GetSpecificShoeListForSize(int colorShoeId, Gender gender)
        {
            var dateNow = DateOnly.FromDateTime(DateTime.Now);

            var sizeList = _db.SpecificShoes.Include("Discount")

                .Where(ss => ss.ColorShoeId == colorShoeId && ss.Gender == gender).Select(ss => new SpecificShoe
                {
                    SpecificShoeId = ss.SpecificShoeId,
                    Gender = ss.Gender,
                    Size = ss.Size,
                    Quantity = ss.Quantity,
                    Price = ss.Price,
                    Discount = new Discount
                    {
                        DiscountId = ss.Discount.DiscountId,
                        DiscountValue = ss.Discount.StartDate <= dateNow && ss.Discount.EndDate >= dateNow && ss.Discount.Active ?
                            ss.Discount.DiscountValue : 0
                    }
                }
            ).ToList();
            return sizeList;
        }



    }
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            Console.WriteLine(invokedExpr.ToString());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}
