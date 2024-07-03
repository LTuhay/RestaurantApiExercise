using RestaurantReservationAPI.Entities;

namespace RestaurantReservationAPI.Services
{
    public interface IMenuItemRepository
    {
        Task<MenuItem> CreateMenuItemAsync(MenuItem newMenuItem);
        Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync();
        Task<MenuItem?> GetMenuItemAsync(int menuItemId);

        Task<bool> MenuItemExistsAsync(int menuItemId);
        void DeleteMenuItemAsync(MenuItem menuItem);

        Task<bool> SaveChangesAsync();

        Task<List<MenuItem>> GetMenuItemsByIdsAsync(List<int> menuItemIds);
    }
}
