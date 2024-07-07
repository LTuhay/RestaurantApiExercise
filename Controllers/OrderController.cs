using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationAPI.DbContexts;
using RestaurantReservationAPI.Entities;
using RestaurantReservationAPI.Models;
using RestaurantReservationAPI.Services;

namespace RestaurantReservationAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/reservations/{reservationId}/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper, IReservationRepository reservationRepository, IEmployeeRepository employeeRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }


        [HttpGet]
        public async Task <ActionResult<IEnumerable<OrderDto>>> GetOrders(int reservationId)
        {

            Reservation? reservation = await _reservationRepository.GetReservationAsync(reservationId);

            if (reservation == null)
            {
                return NotFound($"Reservation with ID {reservationId} was not found.");

            }
            IEnumerable<Order> orders = reservation.Orders.ToList();

            return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));

        }

        [HttpGet("{orderId}", Name = "GetOrder")]
        public async Task<ActionResult<OrderDto>> GetOrder(int reservationId, int orderId)
        {

            Reservation? reservation = await _reservationRepository.GetReservationAsync(reservationId);

            if (reservation == null)
            {
                return NotFound($"Reservation with ID {reservationId} was not found.");

            }
            Order? order = reservation.Orders.Where(o=>o.OrderId == orderId).FirstOrDefault();

            if (order == null)
            {
                return NotFound($"Order with ID {orderId} was not found.");
            }

            return Ok(_mapper.Map<OrderDto>(order));


        }


        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder(int reservationId, OrderCreateDto order)
        {

            Reservation? reservation = await _reservationRepository.GetReservationAsync(reservationId);

            if (reservation == null)
            {
                return NotFound($"Reservation with ID {reservationId} was not found.");

            }

            var employee = await _employeeRepository.GetEmployeeAsync(order.EmployeeId);

            if (employee == null)
            {
                return NotFound($"Employee with ID {order.EmployeeId} was not found.");
            }

            var finalOrder = _mapper.Map<Entities.Order>(order);

            await _orderRepository.AddOrderForReservationAsync(
                reservationId, finalOrder);

            await _orderRepository.SaveChangesAsync();

            var createdOrderToReturn =
                _mapper.Map<Models.OrderDto>(finalOrder);

            return CreatedAtRoute("GetOrder",
                 new
                 {
                     reservationId = reservationId,
                     orderId = createdOrderToReturn.OrderId
                 },
                 createdOrderToReturn);
        }

        [HttpPut("{orderId}")]

        public async Task<ActionResult> UpdateOrder(int reservationId, int orderId, OrderForUpdateDto orderForUpdate)
        {
            Reservation? reservation = await _reservationRepository.GetReservationAsync(reservationId);

            if (reservation == null)
            {
                return NotFound($"Reservation with ID {reservationId} was not found.");

            }

            var orderEntity = await _orderRepository.GetOrderAsync(orderId);

            if (orderEntity == null)
            {
                return NotFound($"Order with ID {orderId} was not found.");
            }


            var employee = await _employeeRepository.GetEmployeeAsync(orderForUpdate.EmployeeId);

            if (employee == null)
            {
                return NotFound($"Employee with ID {orderForUpdate.EmployeeId} was not found.");
            }

            _mapper.Map(orderForUpdate, orderEntity);

            await _orderRepository.SaveChangesAsync();

            return NoContent();

        }

        [HttpPatch("{orderId}")]
        public async Task<ActionResult> PartiallyUpdateOrder(int reservationId, int orderId, JsonPatchDocument<OrderForUpdateDto> patchDocument)
        {

            Reservation? reservation = await _reservationRepository.GetReservationAsync(reservationId);

            if (reservation == null)
            {
                return NotFound($"Reservation with ID {reservationId} was not found.");

            }

            var orderEntity = await _orderRepository.GetOrderAsync(orderId);

            if (orderEntity == null)
            {
                return NotFound($"Order with ID {orderId} was not found.");
            }

            var orderToPatch = _mapper.Map<OrderForUpdateDto>(orderEntity);

            patchDocument.ApplyTo(orderToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(orderToPatch))
            {
                return BadRequest(ModelState);
            }


            var employee = await _employeeRepository.GetEmployeeAsync(orderToPatch.EmployeeId);

            if (employee == null)
            {
                return NotFound();
            }

            _mapper.Map(orderToPatch, orderEntity);
            await _orderRepository.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{orderId}")]
        public async Task<ActionResult> DeleteOrder(int reservationId, int orderId)
        {

            if (!await _reservationRepository.ReservationExistsAsync(reservationId))
            {
                return NotFound($"Reservation with ID {reservationId} was not found.");

            }

            var orderEntity = await _orderRepository.GetOrderAsync(orderId);

            if (orderEntity == null)
            {
                return NotFound($"Order with ID {orderId} was not found.");
            }

            _orderRepository.DeleteOrderAsync(orderEntity);
            await _orderRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}
