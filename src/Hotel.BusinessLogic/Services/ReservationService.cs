using Hotel.BusinessLogic.DTO.HotelReservation;
using AutoMapper;
using Hotel.DataAccess.Repositories.IRepositories;
using Hotel.DataAccess.Entities;
using Hotel.BusinessLogic.Services.IServices;

namespace Hotel.BusinessLogic.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IMapper _mapper;
        IReservationRepository _reservationRepository;
        IInvoiceRepository _invoiceRepository;

        public ReservationService(IMapper mapper,
            IReservationRepository reservationRepository, IInvoiceRepository invoiceRepository)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            _invoiceRepository = invoiceRepository;
        }

        public async Task<List<ReservationCardReturnDTO>> GetAll(int page, int entries)
        {
            List<ReservationCard> CardsList = await _reservationRepository.GetListAsyncWithPagination(page, entries);
            List<ReservationCardReturnDTO> result = _mapper.Map<List<ReservationCardReturnDTO>>(CardsList);
            return result;
        }

        public async Task<List<ReservationCardReturnDTO>> GetReservationCardsByTime(PeriodTimeDTO periodTimeDTO)
        {
            List<ReservationCard> CardsList = await _reservationRepository.GetListReservationCardsByTime(periodTimeDTO.ArrivalDate, periodTimeDTO.DepartureDate);
            List<ReservationCardReturnDTO> result = _mapper.Map<List<ReservationCardReturnDTO>>(CardsList);
            return result;
        }

        public async Task<InvoiceReturnDTO> CreateReservation(ReservationCreateDTO reservationDTO)
        {
            //create invoice
            Invoice createInvoice = _mapper.Map<Invoice>(reservationDTO);

            createInvoice.Status = "Pending";

            //var transaction = _reservationRepository.CreateTransaction();
            List<ReservationCard> CardsListByTime = await _reservationRepository
                .GetListReservationCardsByTime(reservationDTO.ArrivalDate, reservationDTO.DepartureDate);

            if (createInvoice != null)
            {
                for (int i = 0; i < createInvoice.ReservationCards.Count(); i++)
                {
                    createInvoice.ReservationCards.ElementAt(i).ArrivalDate = reservationDTO.ArrivalDate;
                    createInvoice.ReservationCards.ElementAt(i).DepartureDate = reservationDTO.DepartureDate;
                    createInvoice.ReservationCards.ElementAt(i).SetInvoice(createInvoice);
                    Room RoomByID = await _reservationRepository.GetRoomById(reservationDTO.ReservationCards.ElementAt(i).RoomId);
                    if (RoomByID == null)
                    {
                        Console.WriteLine("Room doesn't exist");
                        return null;
                    }
                    createInvoice.ReservationCards.ElementAt(i).SetRoom(RoomByID);
                    //check conflict
                    foreach (ReservationCard card in CardsListByTime)
                    {
                        if (card.Room.Id == reservationDTO.ReservationCards.ElementAt(i).RoomId)
                        {
                            return null;
                        }
                    }
                }
            }

            Invoice? Result = await _invoiceRepository.CreateAsync(createInvoice!);
            //_reservationRepository.CommitTranasction(transaction);

            //return _mapper.Map<InvoiceReturnDTO>(Result);
            return null;
        }
    }
}
