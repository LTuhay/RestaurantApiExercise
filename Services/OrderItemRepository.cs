using Microsoft.EntityFrameworkCore;
using RestaurantReservationAPI.DbContexts;
using RestaurantReservationAPI.Entities;

namespace RestaurantReservationAPI.Services
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly RestaurantContext _context;

        private readonly IOrderRepository _orderRepository;

        public OrderItemRepository(RestaurantContext context, IOrderRepository orderRepository)
        {
            _context = context;
            _orderRepository = orderRepository;
        }

        async public Task AddOrderItemForOrderAsync(int orderId, OrderItem orderItem)
        {
            Order? order = await _orderRepository.GetOrderAsync(orderId);
            if (order != null)
            {
                order.OrderItems.Add(orderItem);
            }
        }

        async public Task DeleteOrderItemForOrderItemAsync(int orderId, OrderItem orderItem)
        {
            Order? order = await _orderRepository.GetOrderAsync(orderId);
            if (order != null)
            {
                order.OrderItems.Remove(orderItem);
            }
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync(int reservationId, int orderId)
        {
            return await _context.OrderItems
                .Where(oi => oi.Order.ReservationId == reservationId && oi.OrderId == orderId)
                .ToListAsync();
        }

        public async Task<OrderItem?> GetOrderItemAsync(int reservationId, int orderId, int orderItemId)
        {
            return await _context.OrderItems
                   .FirstOrDefaultAsync(oi => oi.Order.ReservationId == reservationId && oi.OrderId == orderId && oi.OrderItemId == orderItemId);
        }

        async public Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
