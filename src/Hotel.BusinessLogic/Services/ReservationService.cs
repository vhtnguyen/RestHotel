using Hotel.BusinessLogic.DTO.HotelReservation;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.BusinessLogic.Services.IServices;

namespace Hotel.BusinessLogic.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IMapper _mapper;

        public ReservationService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<ReservationCardReturnDTO>> GetAll(int page, int entries)
        {

            return null;
        }
    }
}
