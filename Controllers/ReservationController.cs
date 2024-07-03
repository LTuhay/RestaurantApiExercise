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
    [Route("api/reservations")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;


        public ReservationController(IReservationRepository reservationRepository, IMapper mapper, ICustomerRepository customerRepository)
        {
            _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAllReservations()
        {
            IEnumerable<Reservation> reservations = await _reservationRepository.GetAllReservationsAsync();

            return Ok(_mapper.Map<IEnumerable<ReservationDto>>(reservations));

        }

        [HttpGet("{id}", Name ="GetReservation")]
        public async Task<ActionResult<ReservationDto>> GetReservation(int id)
        {
            Reservation? reservationToReturn = await _reservationRepository.GetReservationAsync(id);

            if (reservationToReturn == null)
            {
                return NotFound($"Reservation with ID {id} was not found.");
            }

            return Ok(_mapper.Map<ReservationDto>(reservationToReturn));

        }


        [HttpPost]
        public async Task<ActionResult<ReservationDto>> CreateReservation(ReservationCreateDto reservation)
        {
            Customer? customer = await _customerRepository.GetCustomerAsync(reservation.CustomerId);

            if (customer == null)
            {
                return NotFound($"Customer with ID {reservation.CustomerId} was not found.");
            }
            var finalReservation = _mapper.Map<Entities.Reservation>(reservation);

            await _reservationRepository.CreateReservationAsync(finalReservation);
            await _reservationRepository.SaveChangesAsync();
            var createdReservationToReturn = _mapper.Map<Models.ReservationDto>(finalReservation);



            return CreatedAtRoute("GetReservation",
                new
                {
                    id = createdReservationToReturn.ReservationId

                },
                createdReservationToReturn
                );
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateReservation(int id, ReservationForUpdateDto reservationForUpdate)
        {
            var reservationEntity = await _reservationRepository.GetReservationAsync(id);

            if (reservationEntity == null)
            {
                return NotFound($"Reservation with ID {id} was not found.");
            }

            Customer? customer = await _customerRepository.GetCustomerAsync(reservationForUpdate.CustomerId);

            if (customer == null)
            {
                return NotFound($"Customer with ID {reservationForUpdate.CustomerId} was not found.");
            }

            _mapper.Map(reservationForUpdate, reservationEntity);


            await _reservationRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PartiallyUpdateReservation(int id, JsonPatchDocument<ReservationForUpdateDto> patchDocument)
        {
            var reservationEntity = await _reservationRepository.GetReservationAsync(id);

            if (reservationEntity == null)
            {
                return NotFound($"Reservation with ID {id} was not found.");
            }


            var reservationToPatch = _mapper.Map<ReservationForUpdateDto>(reservationEntity);

            patchDocument.ApplyTo(reservationToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(reservationToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(reservationToPatch, reservationEntity);
            await _reservationRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReservation(int id)
        {
            var reservationEntity = await _reservationRepository.GetReservationAsync(id);

            if (reservationEntity == null)
            {
                return NotFound($"Reservation with ID {id} was not found.");
            }

            _reservationRepository.DeleteReservationAsync(reservationEntity);
            await _reservationRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("customers/{id}")]
        public async Task<ActionResult<List<ReservationDto>>> RetrieveReservationsByCustomer(int id)
        {

            var reservationEntity = await _reservationRepository.GetReservationAsync(id);

            if (reservationEntity == null)
            {
                return NotFound($"Reservation with ID {id} was not found.");
            }

            Customer? customer = await _customerRepository.GetCustomerAsync(reservationEntity.CustomerId);

            if (customer == null)
            {
                return NotFound($"Customer with ID {reservationEntity.CustomerId} was not found.");
            }

            IEnumerable<Reservation> reservations = await _reservationRepository.GetReservationByCustomer(id);

            return Ok(_mapper.Map<IEnumerable<ReservationDto>>(reservations));


        }

        [HttpGet("{id}/menu-items")]
        public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetOrderedMenu(int id)
        {
            Reservation? reservation = await _reservationRepository.GetReservationAsync(id);

            if (reservation == null)
            {
                return NotFound($"Reservation with ID {id} was not found.");
            }

            var menuItems = reservation.Orders?
                .SelectMany(order => order.OrderItems)
                .Select(orderItem => orderItem.MenuItem)
                .Distinct()
                .ToList();

            return Ok(_mapper.Map<IEnumerable<MenuItemDto>>(menuItems));

        }


    }
}
