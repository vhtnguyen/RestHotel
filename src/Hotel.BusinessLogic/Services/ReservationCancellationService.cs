﻿using AutoMapper;
using Hotel.BusinessLogic.DTO.HotelReservation;
using Hotel.BusinessLogic.Services.IServices;
using Hotel.DataAccess.Entities;
using Hotel.DataAccess.Repositories.IRepositories;
using Microsoft.Extensions.Hosting;
using org.apache.zookeeper.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.Services
{
    public class ReservationCancellationService : IReservationCancellationService
    {
        private readonly int _invoiceId;
        private readonly TimeSpan _cancellationDelay;
        private readonly int _cancellationTimeoutByMinutes = 1;
        private readonly IMapper _mapper;
        IReservationRepository _reservationRepository;
        IInvoiceRepository _invoiceRepository;

        public ReservationCancellationService(IMapper mapper,
            IReservationRepository reservationRepository, IInvoiceRepository invoiceRepository)
        {
            _cancellationDelay = TimeSpan.FromMinutes(_cancellationTimeoutByMinutes);
            //_cancellationDelay = TimeSpan.FromSeconds(10);
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            _invoiceRepository = invoiceRepository;
        }

        public async Task CheckConfirmedReservation(int InvoiceId)
        {
            await Task.Delay(_cancellationDelay);
            Invoice? invoice = await _invoiceRepository.FindAsync(invoice => invoice.Id == InvoiceId);
            if (invoice != null)
            {
                if (invoice.Status == "pending")
                {
                    await RemoveReservation(InvoiceId);
                }
            }
        }

        public async Task RemoveReservation(int InvoiceId)
        {
            Invoice? invoice = await _invoiceRepository.FindAsync(invoice => invoice.Id == InvoiceId);
            if (invoice != null)
            {
                await _invoiceRepository.RemoveInvoice(invoice);
            }
        }
    }
}
