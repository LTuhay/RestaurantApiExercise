using Microsoft.EntityFrameworkCore;
using RestaurantReservationAPI.DbContexts;
using RestaurantReservationAPI.Entities;

namespace RestaurantReservationAPI.Services
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly RestaurantContext _context;


        public MenuItemRepository(RestaurantContext context)
        {
            _context = context;
        }

        public async Task<MenuItem> CreateMenuItemAsync(MenuItem newMenuItem)
        {
            _context.MenuItems.Add(newMenuItem);
            await _context.SaveChangesAsync();
            return newMenuItem;
        }

        public void DeleteMenuItemAsync(MenuItem menuItem)
        {
            _context.MenuItems.Remove(menuItem);
        }



        public async Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync()
        {
            return await _context.MenuItems.ToListAsync();
        }

        public async Task<MenuItem?> GetMenuItemAsync(int menuItemId)
        {
            return await _context.MenuItems.Where(i => i.MenuItemId == menuItemId).FirstOrDefaultAsync();
        }

        public async Task<List<MenuItem>> GetMenuItemsByIdsAsync(List<int> menuItemIds)
        {
            return await _context.MenuItems
                .Where(mi => menuItemIds.Contains(mi.MenuItemId))
                .ToListAsync();
        }

        public async Task<bool> MenuItemExistsAsync(int menuItemId)
        {
            return await _context.MenuItems.AnyAsync(i => i.MenuItemId == menuItemId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
