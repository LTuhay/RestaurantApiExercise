using RestaurantReservationAPI.Entities;

namespace RestaurantReservationAPI.Services
{
    public interface IEmployeeRepository
    {

        Task<Employee> CreateEmployeeAsync(Employee newEmployee);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee?> GetEmployeeAsync(int employeeId);

        Task<bool> EmployeeExistsAsync(int employeeId);
        void DeleteEmployeeAsync(Employee employee);

        Task<bool> SaveChangesAsync();

    }
}
