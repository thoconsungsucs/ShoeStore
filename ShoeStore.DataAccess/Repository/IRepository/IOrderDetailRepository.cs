﻿using ShoeStore.Models;

namespace ShoeStore.DataAccess.Repository.IRepository
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        void Update(OrderDetail orderDetail);
        List<OrderDetail> GetAllSpecificShoe(int orderHeaderId);
    }
}
