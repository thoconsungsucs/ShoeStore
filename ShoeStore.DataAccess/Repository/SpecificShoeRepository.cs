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

        /*public IEnumerable<SpecificShoeListVM> GetAllGroupByShoeAndGender()
        {
            // Get all specific shoes with number of color, max discount and image url each color
            // Get all shoe images
            var imgList = _db.ShoeImages;
            var specificShoeList = GetAll(includeProperties: "Shoe,Discount")
                        .GroupBy(s => new { s.Shoe.ShoeName, s.Gender, s.ShoeId })
                        .Select(group => new
                        {
                            ShoeName = group.Key.ShoeName,
                            Gender = group.Key.Gender,
                            TotalColor = group.Select(s => s.ColorId).Distinct().Count(),
                            DiscountMax = group.Select(s => s.Discount.DiscountValue).Max(),
                            ShoeId = group.Key.ShoeId,
                            ColorIds = group.Select(s => s.ColorId).Distinct()
                        }).ToList();

            // Get image url for each color
            var specificShoeListWithImg = specificShoeList
                        .Select(item => new SpecificShoeListVM
                        {
                            ShoeName = item.ShoeName,
                            Gender = item.Gender,
                            TotalColors = item.TotalColor,
                            DiscountMax = item.DiscountMax,
                            ShoeId = item.ShoeId,
                            ImageList = item.ColorIds.ToDictionary(
                                colorId => colorId,
                                colorId => imgList.FirstOrDefault(i => i.IsMain && i.ShoeId == item.ShoeId && colorId == i.ColorId)?.ImageUrl
                            )
                        }).ToList();
            return specificShoeListWithImg;
        }*/

        public List<SpecificShoeWithImage> GetSpecificShoeWithImage(List<int>? categories = null, List<Gender>? genders = null, List<string>? prices = null, List<int>? sizes = null, List<int>? colors = null)
        {

            var shoeColorShoeList = _db.Set<Shoe>()
                .Join(
                      _db.Set<ColorShoe>().Where(ss => colors == null || colors.Contains(ss.ColorId)),
                      s => s.ShoeId,
                      cs => cs.ShoeId,
                      (s, cs) => new { s.ShoeId, s.ShoeName, s.CategoryId, s.Price, cs.ColorShoeId }
                 );
            // Where category 
            if (categories != null)
            {
                shoeColorShoeList = shoeColorShoeList.Where(s => categories.Contains(s.CategoryId));
            }



            var specificShoeList = _db.SpecificShoes
                .Include("Discount")
                .Where(ss => genders == null || genders.Contains(ss.Gender))
                .Where(ss => sizes == null || sizes.Contains(ss.Size));

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
                   DiscountMax = group.Max(specificShoeShoeColorShoe => specificShoeShoeColorShoe.SpecificShoe.Discount.DiscountValue),
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
            var sizeList = _db.SpecificShoes.Where(ss => ss.ColorShoeId == colorShoeId && ss.Gender == gender).Include("Discount").Select(ss => new SpecificShoe
            {
                SpecificShoeId = ss.SpecificShoeId,
                Size = ss.Size,
                Quantity = ss.Quantity,
                Price = ss.Price,
                Discount = ss.Discount
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
