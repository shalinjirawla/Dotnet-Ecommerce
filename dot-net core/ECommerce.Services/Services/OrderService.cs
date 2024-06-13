using AutoMapper;
using ECommerce.Models.Interfaces;
using ECommerce.Models.Models;
using ECommerce.Models.Repository;
using ECommerce.Services.DTO;
using ECommerce.Services.Interfaces;

namespace ECommerce.Services.Services
{
    public class OrderService : IOrderService
    {
        #region Fields 
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public OrderService(IOrderRepository orderRepository, IMapper mapper, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _cartRepository = cartRepository;
        }
        #endregion

        #region Methods
        public ResponseDTO GetOrders()
        {
            var response = new ResponseDTO();
            try
            {
                var data = _mapper.Map<List<Order>>(_orderRepository.GetOrders().ToList());
                response.Status = 200;
                response.Message = "Ok";
                response.Data = data;
            }
            catch (Exception e)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = e.Message;
            }
            return response;
        }
        public ResponseDTO GetOrderById(int id)
        {
            var response = new ResponseDTO();
            try
            {
                var order = _orderRepository.GetOrderById(id);
                if (order == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Order not found";
                    return response;
                }
                var result = _mapper.Map<Order>(order);

                response.Status = 200;
                response.Message = "Ok";
                response.Data = result;
            }
            catch (Exception e)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = e.Message;
            }
            return response;
        }
        public ResponseDTO GetOrderByUserId(int userId)
        {
            var response = new ResponseDTO();
            try
            {
                var orders = _orderRepository.GetOrderByUserId(userId);
                if (orders == null || !orders.Any())
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "No orders found for the user";
                    return response;
                }

                response.Status = 200;
                response.Message = "Ok";
                response.Data = orders.Reverse();
            }
            catch (Exception e)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = e.Message;
            }
            return response;
        }
        public ResponseDTO CancelOrder(int orderId)
        {
            var response = new ResponseDTO();
            try
            {
                var order = _orderRepository.GetOrderById(orderId); 
                if (order == null)
                {
                    response.Status = 404;
                    response.Message = "Order not found";
                    return response;
                }
                var cancelOrder = _orderRepository.CancelOrder(_mapper.Map<Order>(order));
                if (cancelOrder)
                {
                    response.Status = 204;
                    response.Message = "Order cancelled successfully";
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Not Updated";
                    response.Error = "Could not cancel order"; ;

                }

            }
            catch(Exception e)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = e.Message;
            }
            return response;
        }

        public ResponseDTO CompleteOrder(int orderId)
        {
            var response = new ResponseDTO();
            try
            {
                var order = _orderRepository.GetOrderById(orderId);
                if (order == null)
                {
                    response.Status = 404;
                    response.Message = "Order not found";
                    return response;
                }
                var completeOrder = _orderRepository.CompleteOrder(_mapper.Map<Order>(order));
                if (completeOrder)
                {
                    response.Status = 204;
                    response.Message = "Order Completed successfully";
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Not Updated";
                    response.Error = "Could not complete order"; ;

                }

            }
            catch (Exception e)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = e.Message;
            }
            return response;
        }

        public ResponseDTO DeleteOrder(int orderId)
        {
            var response = new ResponseDTO();
            try
            {
                var orderById = _orderRepository.GetOrderById(orderId);
                if (orderById == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Order not found";
                    return response;
                }
                var deleteFlag = _orderRepository.DeleteOrder(orderById);
                if (deleteFlag)
                {
                    response.Status = 204;
                    response.Message = "Deleted";
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Not Deleted";
                    response.Error = "Could not delete order";
                }
            }
            catch (Exception e)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = e.Message;
            }
            return response;
        }

        public ResponseDTO PlaceOrder(List<AddOrderDTO> orders)
        {
            var response = new ResponseDTO();
            try
            {
                List<Order> newOrders = new List<Order>();

                foreach (var orderDTO in orders)
                {
                    var newOrder = _mapper.Map<Order>(orderDTO);
                    newOrders.Add(newOrder);
                }

                var placedOrders = _orderRepository.PlaceOrder(newOrders);

                if (placedOrders != null && placedOrders.Any())
                {
                    response.Status = 200;
                    response.Message = "Orders placed successfully";
                    response.Data = placedOrders;
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Failed to place orders";
                }
            }
            catch (Exception e)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = e.Message;
            }
            return response;
        }


        #endregion
    }
}
