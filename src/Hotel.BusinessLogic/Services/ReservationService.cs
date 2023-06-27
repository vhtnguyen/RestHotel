using Hotel.BusinessLogic.DTO.HotelReservation;
using AutoMapper;
using Hotel.DataAccess.Repositories.IRepositories;
using Hotel.DataAccess.Entities;
using Hotel.BusinessLogic.Services.IServices;
using Hotel.DataAccess.ObjectValues;
using Hotel.Shared.Helpers;

namespace Hotel.BusinessLogic.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IMapper _mapper;
        IReservationRepository _reservationRepository;
        IInvoiceRepository _invoiceRepository;
        IRoomRepository _roomRepository;

        public ReservationService(IMapper mapper, IRoomRepository roomRepository,
            IReservationRepository reservationRepository, IInvoiceRepository invoiceRepository)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            _invoiceRepository = invoiceRepository;
            _roomRepository = roomRepository;
        }

        public async Task<List<ReservationCardReturnDTO>> GetAll(int page, int entries)
        {
            List<ReservationCard> CardsList = await _reservationRepository.GetListAsyncWithPagination(page, entries);
            List<ReservationCardReturnDTO> result = _mapper.Map<List<ReservationCardReturnDTO>>(CardsList);
            return result;
        }

        public async Task<List<ReservationCardReturnDTO>> GetReservationCardsByTime(PeriodTimeDTO periodTimeDTO)
        {
            List<ReservationCard> CardsList = await _reservationRepository.GetListReservationCardsInTime(periodTimeDTO.ArrivalDate, periodTimeDTO.DepartureDate);
            List<ReservationCardReturnDTO> result = _mapper.Map<List<ReservationCardReturnDTO>>(CardsList);
            return result;
        }

        public async Task<PendingInvoiceReturnDTO> CreatePendingReservation(ReservationCreateDTO reservationDTO)
        {
            //create invoice
            Invoice createInvoice = _mapper.Map<Invoice>(reservationDTO);

            createInvoice.Status = "pending";

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
                        return null;
                    }
                    createInvoice.ReservationCards.ElementAt(i).SetRoom(RoomByID);
                    createInvoice.ReservationCards.ElementAt(i).RoomRegulation = RoomByID.RoomDetail!.RoomRegulation;
                    //check conflict
                    foreach (ReservationCard card in CardsListByTime)
                    {
                        if (card.Room!.Id == reservationDTO.ReservationCards.ElementAt(i).RoomId)
                        {
                            return null;
                        }
                    }
                }
            }

            Invoice? Result = await _invoiceRepository.CreateAsync(createInvoice!);

            //_reservationRepository.CommitTranasction(transaction);

            //return _mapper.Map<InvoiceReturnDTO>(Result);
            return new PendingInvoiceReturnDTO(Result!.Id);
        }

        public async Task<InvoiceReturnDTO?> ConfirmReservation(ReservationConfirmedDTO reservationDTO)
        {
            //create invoice
            Invoice? invoice = await _invoiceRepository.GetInvoiceDetail(reservationDTO.InvoiceId);

            if (invoice != null)
            {
                invoice.Status = "partly_deposited";
                invoice.Email = reservationDTO.Email;
                invoice.TotalSum = reservationDTO.TotalSum;
                invoice.DownPayment = reservationDTO.DownPayment;
                invoice.NameCus = reservationDTO.NameCus;
                await _invoiceRepository.UpdateInvoice(invoice);

                ICollection<ReservationCard> availableCards = await _reservationRepository.FindAsyncByInvoiceID(invoice.Id);
                foreach (GuestRoomInfoDTO card in reservationDTO.ReservationCards)
                {
                    foreach (ReservationCard aCard in availableCards)
                    {
                        if (aCard.Room!.Id == card.RoomId)
                        {
                            aCard.Guests = _mapper.Map<List<Guest>>(card.Guests);
                            aCard.Notes = card.Notes;
                            await _reservationRepository.UpdateAsync(aCard);
                        }
                    }
                }
                invoice.ReservationCards = availableCards;
                double total = await CountRoomFee(invoice.Id);
                invoice.TotalSum = total;
                await _invoiceRepository.UpdateInvoice(invoice);
            }
            else
            {
                return null;
            }

            InvoiceReturnDTO result = _mapper.Map<InvoiceReturnDTO>(invoice);
            result.ArrivalDate = reservationDTO.ArrivalDateStr;
            result.DepartureDate = reservationDTO.DepartureDateStr;

            return _mapper.Map<InvoiceReturnDTO>(result);
        }

        public async Task<ReservationCardReturnDTO?> GetReservationCardByID(int id)
        {
            ReservationCard? card = await _reservationRepository.GetReservationCardByID(id);
            if (card == null)
            {
                return null;
            }
            var result = _mapper.Map<ReservationCardReturnDTO>(card);
            return result;
        }

        public async Task<List<ReservationCardReturnDTO>?> GetReservationCardByInvoiceID(int id)
        {
            List<ReservationCard> cards = await _reservationRepository.FindAsyncByInvoiceID(id);
            var result = _mapper.Map<List<ReservationCardReturnDTO>>(cards);
            return result;
        }

        public async Task<List<ReservationCardReturnDTO>?> ChangeRoom(ChangeRoomDTO changeRoomDTO)
        {
            List<ReservationCard> handledCards = new List<ReservationCard>();
            //get revcards by invoice id
            List<ReservationCard> cards = await _reservationRepository.FindAsyncByInvoiceID(changeRoomDTO.InvoiceId);

            if (cards.Count() == 0)
            {
                var result = _mapper.Map<List<ReservationCardReturnDTO>>(cards);
                return result;
            }

            Invoice? invoiceToFindFee = await _invoiceRepository.FindAsync(i => i.Id == changeRoomDTO.InvoiceId);
            double oldRoomFee = await CountRoomFee(invoiceToFindFee!.Id);
            double serviceFee = invoiceToFindFee.TotalSum - oldRoomFee;

            foreach (ReservationCard oldCard in cards)
            {
                if (oldCard.Room!.Id == changeRoomDTO.OldRoomId)
                {
                    //just extend time of stay
                    if (changeRoomDTO.OldRoomId == changeRoomDTO.NewRoomId)
                    {
                        oldCard.DepartureDate = changeRoomDTO.To;
                    }
                    else
                    {
                        Room RoomByID = await _reservationRepository.GetRoomById(changeRoomDTO.NewRoomId);
                        if (RoomByID == null)
                        {
                            return null;
                        }
                        Invoice? invoice = await _invoiceRepository.FindAsync(i => i.Id == oldCard.Invoice!.Id);
                        oldCard.DepartureDate = DateTime.UtcNow.ToVietnameseDatetime();
                        ReservationCard newCard = new ReservationCard();
                        newCard.ArrivalDate = DateTime.UtcNow.ToVietnameseDatetime();
                        newCard.DepartureDate = changeRoomDTO.To;
                        newCard.SetInvoice(invoice!);
                        newCard.SetRoom(RoomByID);
                        foreach (Guest guest in oldCard.Guests)
                        {
                            newCard.AddGuest(new Guest(guest.Name, guest.TelephoneNumber,
                                    guest.Address, guest.Type, guest.PersonIdentification));
                        }
                        newCard.Notes = oldCard.Notes;
                        newCard.RoomRegulation = RoomByID.RoomDetail!.RoomRegulation;
                        ReservationCard createdCard = await _reservationRepository.CreateAsync(newCard);
                        handledCards.Add(createdCard);
                    }
                    await _reservationRepository.UpdateAsync(oldCard);
                    handledCards.Add(oldCard);
                }
            }

            invoiceToFindFee.TotalSum = serviceFee + await CountRoomFee(invoiceToFindFee.Id);
            await _invoiceRepository.UpdateInvoice(invoiceToFindFee);

            return _mapper.Map<List<ReservationCardReturnDTO>>(handledCards);
        }

        public async Task<ReservationCardReturnDTO?> EditReservationCard(ReservationCardEditDTO reservationCardEditDTO)
        {
            ReservationCard? card = await _reservationRepository.GetReservationCardByID(reservationCardEditDTO.Id);
            if (card == null)
            {
                return null;
            }

            card.Guests = _mapper.Map<List<Guest>>(reservationCardEditDTO.Guests);
            card.Notes = card.Notes;
            await _reservationRepository.UpdateAsync(card);

            var result = _mapper.Map<ReservationCardReturnDTO>(card);
            return result;
        }

        public async Task<ReservationCardReturnDTO?> RemoveReservationCard(IdDTO idDTO)
        {
            ReservationCard? card = await _reservationRepository.GetReservationCardByID(idDTO.Id);
            if (card == null)
            {
                return null;
            }
            card.Invoice!.TotalSum -= _countRoomFee(card);
            await _reservationRepository.RemoveAsync(card);

            await _invoiceRepository.SaveChangesAsync();
            var result = _mapper.Map<ReservationCardReturnDTO>(card);
            return result;
        }

        public async Task<int> GetTotalPage(int page, int entries)
        {
            return await _reservationRepository.GetTotalPages(page, entries);
        }

        public async Task<double> CountRoomFee(int id)
        {
            double total = 0;

            Invoice? invoice = await _invoiceRepository.GetInvoiceDetail(id);

            foreach (ReservationCard card in invoice!.ReservationCards)
            {
                total += _countRoomFee(card);
            }

            invoice.TotalSum = total;

            //await _invoiceRepository.UpdateInvoice(invoice);
            return total;
        }
        private double _countRoomFee(ReservationCard card)
        {
            TimeSpan duration = card.DepartureDate - card.ArrivalDate;
            int daysOfStay = duration.Days + 1;
            int numGuests = card.Guests.Count();
            Boolean hasForeign = false;
            double roomFee = card.Room!.RoomDetail!.Price;
            foreach (Guest guest in card.Guests)
            {
                if (guest.Type == "foreigner")
                {
                    hasForeign = true;
                    break;
                }
            }

            if (hasForeign)
            {
                roomFee = roomFee + roomFee * card.RoomRegulation!.MaxOverseaSurchargeRatio;
            }
            
            if (numGuests > card.RoomRegulation!.DefaultGuest)
            {
                roomFee = roomFee + roomFee * card.RoomRegulation!.MaxSurchargeRatio;
            }

            roomFee = roomFee * daysOfStay;
            return roomFee;
        }
    }
}
