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
    [Route("api/customers")]
    public class CustumerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustumerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomers()
        {
            IEnumerable<Customer> customers = await _customerRepository.GetAllCustomersAsync();

            return Ok(_mapper.Map<IEnumerable<CustomerDto>>(customers));

        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            Customer? customerToReturn = await _customerRepository.GetCustomerAsync(id);

            if (customerToReturn == null)
            {
                return NotFound($"Customer with ID {id} was not found.");
            }

            return Ok(_mapper.Map<CustomerDto>(customerToReturn));


        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer (CustomerCreateDto customer)
        {


            var finalCustomer = _mapper.Map<Entities.Customer>(customer);

            await _customerRepository.CreateCustomerAsync(finalCustomer);
            await _customerRepository.SaveChangesAsync ();
            var createdCustomerToReturn = _mapper.Map<Models.CustomerDto>(finalCustomer);
                
            return CreatedAtRoute("GetCustomer", 
                new
                {
                    id = createdCustomerToReturn.CustomerId

                },
                createdCustomerToReturn
                );
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateCustomer(int id, CustomerForUpdateDto customerForUpdate)
        {

            var customerEntity = await _customerRepository.GetCustomerAsync(id);

            if (customerEntity == null)
            {
                return NotFound($"Customer with ID {id} was not found.");
            }

            _mapper.Map(customerForUpdate, customerEntity);


            await _customerRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PartiallyUpdateCustomer(int id, JsonPatchDocument<CustomerForUpdateDto> patchDocument)
        {
            var customerEntity = await _customerRepository.GetCustomerAsync(id);

            if (customerEntity == null)
            {
                return NotFound($"Customer with ID {id} was not found.");
            }

            var customerToPatch = _mapper.Map<CustomerForUpdateDto>(customerEntity);

            patchDocument.ApplyTo(customerToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(customerToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(customerToPatch, customerEntity);
            await _customerRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id) 
        {
            var customerEntity = await _customerRepository.GetCustomerAsync(id);

            if (customerEntity == null)
            {
                return NotFound($"Customer with ID {id} was not found.");
            }

            _customerRepository.DeleteCustomerAsync(customerEntity);
            await _customerRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
