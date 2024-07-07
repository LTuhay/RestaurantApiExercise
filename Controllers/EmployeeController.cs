using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationAPI.DbContexts;
using RestaurantReservationAPI.Entities;
using RestaurantReservationAPI.Models;
using RestaurantReservationAPI.Services;

namespace RestaurantReservationAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper, IOrderRepository orderRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployees()
        {
            IEnumerable<Employee> employee = await _employeeRepository.GetAllEmployeesAsync();

            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employee));

        }

        [HttpGet("{id}", Name = "GetEmployee")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id) 
        {
            Employee? employeeToReturn = await _employeeRepository.GetEmployeeAsync(id);

            if (employeeToReturn == null)
            {
                return NotFound($"Employee with ID {id} was not found.");
            }

            return Ok(_mapper.Map<EmployeeDto>(employeeToReturn));


        }


        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee(EmployeeCreateDto employee)
        {
            var finalEmployee = _mapper.Map<Entities.Employee>(employee);

            await _employeeRepository.CreateEmployeeAsync(finalEmployee);
            await _employeeRepository.SaveChangesAsync();
            var createdEmployeeToReturn = _mapper.Map<Models.EmployeeDto>(finalEmployee);

            return CreatedAtRoute("GetEmployee",
                new
                {
                    id = createdEmployeeToReturn.EmployeeId

                },
                createdEmployeeToReturn
                );
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateEmployee(int id, EmployeeForUpdateDto employeeForUpdate)
        {

            var employeeEntity = await _employeeRepository.GetEmployeeAsync(id);

            if (employeeEntity == null)
            {
                return NotFound($"Employee with ID {id} was not found.");
            }

            _mapper.Map(employeeForUpdate, employeeEntity);


            await _employeeRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PartiallyUpdateEmployee(int id, JsonPatchDocument<EmployeeForUpdateDto> patchDocument)
        {
            var employeeEntity = await _employeeRepository.GetEmployeeAsync(id);

            if (employeeEntity == null)
            {
                return NotFound($"Employee with ID {id} was not found.");
            }

            var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);

            patchDocument.ApplyTo(employeeToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(employeeToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(employeeToPatch, employeeEntity);
            await _employeeRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var employeeEntity = await _employeeRepository.GetEmployeeAsync(id);

            if (employeeEntity == null)
            {
                return NotFound($"Employee with ID {id} was not found.");
            }

            _employeeRepository.DeleteEmployeeAsync(employeeEntity);
            await _employeeRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("managers")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllManagers()
        {

            IEnumerable<Employee> employee = await _employeeRepository.GetAllEmployeesAsync();

            IEnumerable<Employee> managers = employee.Where(e => e.Role == EmployeeRole.Manager);

            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(managers));

        }

        [HttpGet("{id}/average-order-amount")]
        public async Task<ActionResult<decimal>> AverageOrderAmount(int id)
        {
            var employeeEntity = await _employeeRepository.GetEmployeeAsync(id);

            if (employeeEntity == null)
            {
                return NotFound($"Employee with ID {id} was not found.");
            }

            var ordersForEmployee = await _orderRepository.GetOrdersForEmployeeAsync(id);

            if (!ordersForEmployee.Any())
            {
                return Ok(0);
            }

            decimal averageOrderAmount = ordersForEmployee
                .SelectMany(order => order.OrderItems)
                .Average(item => item.Quantity * item.MenuItem.Price);

            return Ok(averageOrderAmount);


        }


    }
}
