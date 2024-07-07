using Microsoft.EntityFrameworkCore;
using RestaurantReservationAPI.DbContexts;
using RestaurantReservationAPI.Entities;

namespace RestaurantReservationAPI.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly RestaurantContext _context;

        public CustomerRepository(RestaurantContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Customer> CreateCustomerAsync(Customer newCustomer)
        {
            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();
            return newCustomer;
        }

        async public Task<bool> CustomerExistsAsync(int customerId)
        {
            return await _context.Customers.AnyAsync(c => c.CustomerId == customerId);
        }

        public void DeleteCustomerAsync(Customer customer)
        {
            _context.Customers.Remove(customer);
        }

        async public Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        async public Task<Customer?> GetCustomerAsync(int customerId)
        {
            return await _context.Customers.Where(c => c.CustomerId == customerId).FirstOrDefaultAsync();
        }

        async public Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
