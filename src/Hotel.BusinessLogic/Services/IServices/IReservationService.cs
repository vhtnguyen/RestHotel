using Hotel.BusinessLogic.DTO.HotelReservation;
namespace Hotel.BusinessLogic.Services.IServices
{
    public interface IReservationService
    {
        Task<List<ReservationCardReturnDTO>> GetAll(int page, int entries);

        Task<List<ReservationCardReturnDTO>> GetReservationCardsByTime(PeriodTimeDTO periodTimeDTO);
        Task<PendingInvoiceReturnDTO> CreatePendingReservation(ReservationCreateDTO reservation);
        Task<InvoiceReturnDTO?> ConfirmReservation(ReservationConfirmedDTO reservationDTO);
        Task<ReservationCardReturnDTO?> GetReservationCardByID(int id);
        Task<List<ReservationCardReturnDTO>?> GetReservationCardByInvoiceID(int id);
        Task<List<ReservationCardReturnDTO>?> ChangeRoom(ChangeRoomDTO changeRoomDTO);
        Task<ReservationCardReturnDTO?> EditReservationCard(ReservationCardEditDTO reservationCardEditDTO);
        Task<ReservationCardReturnDTO?> RemoveReservationCard(IdDTO idDTO);
    }
}
