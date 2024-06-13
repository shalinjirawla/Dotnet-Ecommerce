using ECommerce.Models.Interfaces;
using ECommerce.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Models.Repository
{
    public class OrderRepository : IOrderRepository
    {
        #region fields
        private readonly AppDbContext _context;
        #endregion

        #region constructors
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public IEnumerable<Order> GetOrders()
        {
            return _context.Orders.Include(o=>o.User).Include(o=>o.Product).ToList();
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == id);
        }
        public IEnumerable<Order> GetOrderByUserId(int userId)
        {
            return _context.Orders.Include(o=>o.User).Include(o=>o.Product).Where(o => o.UserId == userId).ToList();
        }
        public List<Order> PlaceOrder(List<Order> orders)
        {
            List<Order> placedOrders = new List<Order>();

            foreach (Order order in orders)
            {
                var product = _context.Products.Find(order.ProductId);
                if (product == null || product.Quantity < order.Quantity)
                {
                    throw new Exception($"Product with ID {order.ProductId} is not available or insufficient quantity.");
                }

                order.OrderDate = DateTime.Now;
                order.Status = "InProgress";
                _context.Orders.Add(order);

                product.Quantity -= order.Quantity;
                product.TimesSold += order.Quantity;
                _context.Products.Update(product);

                _context.SaveChanges();
                placedOrders.Add(order);
            }

            return placedOrders;
        }

        public bool CancelOrder(Order order)
        {

            var orders = _context.Orders.Find(order.Id);
            if (orders != null)
            {
                var product = _context.Products.Find(orders.ProductId);
                if (product != null)
                {
                    product.Quantity += orders.Quantity;
                    product.TimesSold -= orders.Quantity;
                    _context.Entry(product).State = EntityState.Modified;
                }
                orders.Status = "Cancelled";
                _context.Entry(orders).State = EntityState.Modified;
                return _context.SaveChanges() > 0;
            }
            return false;
        }

        public bool CompleteOrder(Order order)
        {
            var orders = _context.Orders.Find(order.Id);
            if(orders != null)
            {
                orders.Status = "Completed";
                _context.Entry(orders).State = EntityState.Modified;
            }
            return _context.SaveChanges() > 0;
        }

        public bool DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return true;
        }

      
    }
    #endregion
}
