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

            return null;
        }

        public async Task<invoiceBrowserDTO> CreateReservation(ReservationCreateDTO reservation)
        {

            //create invoice

            Invoice createInvoice = new Invoice()
            {
                Date = DateTime.UtcNow,
                Status = "Check-in",
                TotalSum = reservation.Cost,
                DownPayment = reservation.Cost,
                Email = "example@example.com"
            };

            var invoice = _invoiceRepository.CreateAsync(null);

            //create reservation card
            var reservationCard = _reservationRepository.CreateAsync(null);


            return null;
        }
    }
}
