using RestaurantReservationAPI.Entities;

namespace RestaurantReservationAPI.Services
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderAsync(int orderId);

        Task<bool> OrderExistsAsync(int orderId);
        void DeleteOrderAsync(Order order);

        Task AddOrderForReservationAsync(int reservationId, Order order);

        Task DeleteOrderForReservationAsync(int reservationId, Order order);

        Task<IEnumerable<Order>> GetOrdersForEmployeeAsync(int employeeId);

        Task<bool> SaveChangesAsync();
    }
}
