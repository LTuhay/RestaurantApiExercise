using Microsoft.EntityFrameworkCore;
using RestaurantReservationAPI.DbContexts;
using RestaurantReservationAPI.Entities;

namespace RestaurantReservationAPI.Services
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RestaurantContext _context;

        private readonly IReservationRepository _reservationRepository;

        public OrderRepository(RestaurantContext context, IReservationRepository reservationRepository)
        {
            _context = context;
            _reservationRepository = reservationRepository;
        }

        async public Task AddOrderForReservationAsync(int reservationId, Order order)
        {
            Reservation? reservation = await _reservationRepository.GetReservationAsync(reservationId);
            if (reservation != null)
            {
                reservation.Orders.Add(order);
            }
        }

        public void DeleteOrderAsync(Order order)
        {
            _context.Orders.Remove(order);
        }

        async public Task DeleteOrderForReservationAsync(int reservationId, Order order)
        {
            Reservation? reservation = await _reservationRepository.GetReservationAsync(reservationId);
            if (reservation != null)
            {
                reservation.Orders.Remove(order);
            }
        }

        async public Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        async public Task<Order?> GetOrderAsync(int orderId)
        {
            return await _context.Orders.Where(o => o.OrderId == orderId).FirstOrDefaultAsync();
        }

        async public Task<IEnumerable<Order>> GetOrdersForEmployeeAsync(int employeeId)
        {
            return await _context.Orders.Where(o => o.EmployeeId == employeeId).ToListAsync();
        }

        async public Task<bool> OrderExistsAsync(int orderId)
        {
            return await _context.Orders.AnyAsync(e => e.OrderId == orderId);
        }

        async public Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
