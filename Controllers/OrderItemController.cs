using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationAPI.Entities;
using RestaurantReservationAPI.Models;
using RestaurantReservationAPI.Services;

namespace RestaurantReservationAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/reservations/{reservationId}/orders/{orderId}/order-items")]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;

        public OrderItemController(IOrderItemRepository orderItemRepository, IMapper mapper, IOrderRepository orderRepository, IReservationRepository reservationRepository, IMenuItemRepository menuItemRepository)
        {
            _orderItemRepository = orderItemRepository ?? throw new ArgumentNullException(nameof(orderItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(_orderRepository));
            _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(_reservationRepository));
            _menuItemRepository = menuItemRepository ?? throw new ArgumentNullException(nameof(_menuItemRepository));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetOrderItems(int reservationId, int orderId)
        {
            if (!await _reservationRepository.ReservationExistsAsync(reservationId))
            {
                return NotFound($"Reservation with ID {reservationId} was not found.");
            }

            if (!await _orderRepository.OrderExistsAsync(orderId))
            {
                return NotFound($"Order with ID {orderId} was not found.");
            }

            var orderItems = await _orderItemRepository.GetAllOrderItemsAsync(reservationId, orderId);

            var orderItemsDto = _mapper.Map<IEnumerable<OrderItemDto>>(orderItems);
            return Ok(orderItemsDto);
        }

        [HttpGet("{orderItemId}", Name = "GetOrderItem")]
        public async Task<ActionResult<OrderItemDto>> GetOrderItem(int reservationId, int orderId, int orderItemId)
        {
            if (!await _reservationRepository.ReservationExistsAsync(reservationId))
            {
                return NotFound($"Reservation with ID {reservationId} was not found.");
            }

            if (!await _orderRepository.OrderExistsAsync(orderId))
            {
                return NotFound($"Order with ID {orderId} was not found.");
            }

            var orderItem = await _orderItemRepository.GetOrderItemAsync(reservationId, orderId, orderItemId);

            if (orderItem == null)
            {
                return NotFound($"Order item with ID {orderItemId} was not found.");
            }

            var orderItemDto = _mapper.Map<OrderItemDto>(orderItem);
            return Ok(orderItemDto);
        }

        [HttpPost]
        public async Task<ActionResult<OrderItemDto>> CreateOrderItem(int reservationId, int orderId, OrderItemCreateDto orderItem)
        {
            if (!await _reservationRepository.ReservationExistsAsync(reservationId))
            {
                return NotFound($"Reservation with ID {reservationId} was not found.");
            }

            if (!await _orderRepository.OrderExistsAsync(orderId))
            {
                return NotFound($"Order with ID {orderId} was not found.");
            }

            var menuItem = await _menuItemRepository.GetMenuItemAsync(orderItem.MenuItemId);

            if (menuItem == null)
            {
                return NotFound($"Menu item with ID {orderItem.MenuItemId} was not found.");
            }

            var orderItemEntity = _mapper.Map<OrderItem>(orderItem);
            await _orderItemRepository.AddOrderItemForOrderAsync(orderId, orderItemEntity);
            await _orderItemRepository.SaveChangesAsync();

            var finalOrderItem = _mapper.Map<OrderItemDto>(orderItemEntity);

            return CreatedAtRoute("GetOrderItem",
                 new
                 {
                     reservationId = reservationId,
                     orderId = orderId,
                     orderItemId = finalOrderItem.OrderItemId
                 },
                 finalOrderItem);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrderItem(int reservationId, int orderId, int id, OrderItemForUpdateDto orderItemForUpdate)
        {
            if (!await _reservationRepository.ReservationExistsAsync(reservationId))
            {
                return NotFound($"Reservation with ID {reservationId} was not found.");
            }

            if (!await _orderRepository.OrderExistsAsync(orderId))
            {
                return NotFound($"Order with ID {orderId} was not found.");
            }

            if (!await _menuItemRepository.MenuItemExistsAsync(orderItemForUpdate.MenuItemId))
            {
                return NotFound($"Menu item item with ID {orderItemForUpdate.MenuItemId} was not found.");
            }

            var orderItemEntity = await _orderItemRepository.GetOrderItemAsync(reservationId, orderId, id);
            if (orderItemEntity == null)
            {
                return NotFound($"Order item with ID {id} was not found.");
            }

            _mapper.Map(orderItemForUpdate, orderItemEntity);
            await _orderItemRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PartiallyUpdateOrderItem(int reservationId, int orderId, int id, JsonPatchDocument<OrderItemForUpdateDto> patchDocument)
        {
            if (!await _reservationRepository.ReservationExistsAsync(reservationId))
            {
                return NotFound($"Reservation with ID {reservationId} was not found.");
            }

            if (!await _orderRepository.OrderExistsAsync(orderId))
            {
                return NotFound($"Order with ID {orderId} was not found.");
            }

            var orderItemEntity = await _orderItemRepository.GetOrderItemAsync(reservationId, orderId, id);
            if (orderItemEntity == null)
            {
                return NotFound($"Order item with ID {id} was not found.");
            }

            var orderItemToPatch = _mapper.Map<OrderItemForUpdateDto>(orderItemEntity);

            patchDocument.ApplyTo(orderItemToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(orderItemToPatch, orderItemEntity);
            await _orderItemRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderItem(int reservationId, int orderId, int id)
        {
            if (!await _reservationRepository.ReservationExistsAsync(reservationId))
            {
                return NotFound($"Reservation with ID {reservationId} was not found.");
            }

            if (!await _orderRepository.OrderExistsAsync(orderId))
            {
                return NotFound($"Order with ID {orderId} was not found.");
            }

            var orderItemEntity = await _orderItemRepository.GetOrderItemAsync(reservationId, orderId, id);
            if (orderItemEntity == null)
            {
                return NotFound($"Order item with ID {id} was not found.");
            }

            _orderItemRepository.DeleteOrderItemForOrderItemAsync(orderId, orderItemEntity);
            await _orderItemRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
