using ECommerce.Services.DTO;
using ECommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        #region Fields
        private readonly IOrderService _orderService;
        #endregion

        #region Constructors
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        #endregion

        #region Methods
        [HttpGet]
        public IActionResult GetOrders()
        {
            return Ok(_orderService.GetOrders());
        }

        [HttpGet("{userId}")]
        public IActionResult GetOrderByUserId(int userId)
        {
            return Ok(_orderService.GetOrderByUserId(userId));
        }

        [HttpPost]
        public IActionResult PlaceOrder(List<AddOrderDTO> order)
        {
            return Ok(_orderService.PlaceOrder(order));
        }

        [HttpPut("{orderId}/cancel")]
        public IActionResult CancelOrder(int orderId)
        {
            return Ok(_orderService.CancelOrder(orderId));
        }

        [HttpPut("{orderId}/complete")]
        public IActionResult CompleteOrder(int orderId)
        {
            return Ok(_orderService.CompleteOrder(orderId));
        }

        [HttpDelete("{orderId}")]
        public IActionResult DeleteOrder(int orderId)
        {
            return Ok(_orderService.DeleteOrder(orderId));
        }
        #endregion

    }
}
