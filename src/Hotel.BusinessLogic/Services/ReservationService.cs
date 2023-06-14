using Hotel.BusinessLogic.DTO.HotelReservation;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.BusinessLogic.DTO.Invoices;
using Hotel.DataAccess.Repositories;
using Hotel.DataAccess.Repositories.IRepositories;
using Hotel.DataAccess.Entities;
using Hotel.DataAccess.ObjectValues;
using System.Globalization;
using Microsoft.Extensions.Hosting;

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
            List<ReservationCardReturnDTO> result =  _mapper.Map<List<ReservationCardReturnDTO>>(CardsList);
            return result;
        }

        public async Task<List<ReservationCardReturnDTO>> GetReservationCardsByTime(PeriodTimeDTO periodTimeDTO)
        {
            List<ReservationCard> CardsList = await _reservationRepository.GetListReservationCardsByTime(periodTimeDTO.ArrivalDate, periodTimeDTO.DepartureDate);
            List<ReservationCardReturnDTO> result =  _mapper.Map<List<ReservationCardReturnDTO>>(CardsList);
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
                    createInvoice.ReservationCards.ElementAt(i).RoomRegulation = RoomByID.RoomDetail.RoomRegulation;
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
            
            Invoice Result = await _invoiceRepository.CreateAsync(createInvoice);

            //_reservationRepository.CommitTranasction(transaction);

            //return _mapper.Map<InvoiceReturnDTO>(Result);
            return new PendingInvoiceReturnDTO(Result.Id);
        }

        public async Task<InvoiceReturnDTO> ConfirmReservation(ReservationConfirmedDTO reservationDTO)
        {
            //create invoice
            Invoice invoice = await _invoiceRepository.GetInvoiceDetail(reservationDTO.InvoiceId);
            
            if (invoice != null)
            {
                invoice.Status = "partlydeposited";
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
                        if (aCard.Room.Id == card.RoomId)
                        {
                            aCard.Guests = _mapper.Map<List<Guest>>(card.Guests);
                            aCard.Notes = card.Notes;
                            await _reservationRepository.UpdateAsync(aCard);
                        }
                    }
                }
                invoice.ReservationCards = availableCards;
            }
            else
            {
                return null;
            }
    

            return _mapper.Map<InvoiceReturnDTO>(invoice);
        }
    }
}
