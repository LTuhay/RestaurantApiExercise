using Microsoft.EntityFrameworkCore;
using RestaurantReservationAPI.DbContexts;
using RestaurantReservationAPI.Entities;

namespace RestaurantReservationAPI.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly RestaurantContext _context;

        public EmployeeRepository(RestaurantContext context) 
        {
            _context = context;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee newEmployee)
        {
            _context.Employees.Add(newEmployee);
            await _context.SaveChangesAsync();
            return newEmployee;
        }

        public void DeleteEmployeeAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
        }

        async public Task<bool> EmployeeExistsAsync(int employeeId)
        {
            return await _context.Employees.AnyAsync(e => e.EmployeeId == employeeId);
        }

        async public Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        async public Task<Employee?> GetEmployeeAsync(int employeeId)
        {
            return await _context.Employees.Where(e => e.EmployeeId == employeeId).FirstOrDefaultAsync();
        }

        async public Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
