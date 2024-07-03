using RestaurantReservationAPI.Entities;

namespace RestaurantReservationAPI.Services
{
    public interface IOrderItemRepository
    {

        Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync(int reservationId, int orderId);
        Task<OrderItem?> GetOrderItemAsync(int reservationId, int orderId, int orderItemId);
        Task AddOrderItemForOrderAsync(int orderId, OrderItem orderItem);

        Task DeleteOrderItemForOrderItemAsync(int orderId, OrderItem orderItem);

        Task<bool> SaveChangesAsync();
    }
}
