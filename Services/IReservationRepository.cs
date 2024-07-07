using RestaurantReservationAPI.Entities;

namespace RestaurantReservationAPI.Services
{
    public interface IReservationRepository

    {
        Task<Reservation> CreateReservationAsync(Reservation newReservation);
        Task<IEnumerable<Reservation>> GetAllReservationsAsync();

        Task<IEnumerable<Reservation>> GetReservationByCustomer(int customerId);
        Task<Reservation?> GetReservationAsync(int reservationId);

        Task<bool> ReservationExistsAsync(int reservationId);
        void DeleteReservationAsync(Reservation reservation);

        Task<bool> SaveChangesAsync();
    }
}
