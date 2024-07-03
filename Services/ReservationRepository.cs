using Microsoft.EntityFrameworkCore;
using RestaurantReservationAPI.DbContexts;
using RestaurantReservationAPI.Entities;

namespace RestaurantReservationAPI.Services
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly RestaurantContext _context;

        public ReservationRepository(RestaurantContext context)
        {
            _context = context;
        }

        public async Task<Reservation> CreateReservationAsync(Reservation newReservation)
        {
            _context.Reservations.Add(newReservation);
            await _context.SaveChangesAsync();
            return newReservation;
        }

        public void DeleteReservationAsync(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
        }

        async public Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            return await _context.Reservations.ToListAsync();
        }

        async public Task<Reservation?> GetReservationAsync(int reservationId)
        {
            return await _context.Reservations.Where(r => r.ReservationId == reservationId).FirstOrDefaultAsync();
        }

        async public Task<IEnumerable<Reservation>> GetReservationByCustomer(int customerId)
        {
            return await _context.Reservations.Where(r => r.CustomerId == customerId).ToListAsync();

        }

        async public Task<bool> ReservationExistsAsync(int reservationId)
        {
            return await _context.Reservations.AnyAsync(e => e.ReservationId == reservationId);
        }

        async public Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
