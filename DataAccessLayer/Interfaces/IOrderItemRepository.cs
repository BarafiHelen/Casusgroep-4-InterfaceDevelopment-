using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IOrderItemRepository
    {
        IEnumerable<OrderItem> GetAll();
        IEnumerable<OrderItem> GetByOrderId(int orderId);
        OrderItem? GetById(int id);
        void Add(OrderItem orderItem);
        void Update(OrderItem orderItem);
        void Delete(OrderItem orderItem);
    }
}
