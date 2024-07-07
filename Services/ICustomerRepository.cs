using RestaurantReservationAPI.Entities;
using RestaurantReservationAPI.Models;

namespace RestaurantReservationAPI.Services
{
    public interface ICustomerRepository
    {

        Task<Customer> CreateCustomerAsync(Customer newCustomer);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerAsync(int customerId);

        Task<bool> CustomerExistsAsync(int customerId);
        void DeleteCustomerAsync(Customer customer);

        Task<bool> SaveChangesAsync();
    }
}
