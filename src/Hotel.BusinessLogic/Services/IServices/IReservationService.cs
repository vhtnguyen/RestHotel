using Hotel.BusinessLogic.DTO.HotelReservation;

namespace Hotel.BusinessLogic.Services.IServices
{
    public interface IReservationService
    {
        Task<List<ReservationCardReturnDTO>> GetAll(int page, int entries);
    }
}
