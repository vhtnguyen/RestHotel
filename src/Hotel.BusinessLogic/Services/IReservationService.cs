using Hotel.BusinessLogic.DTO.HotelReservation;
using Hotel.BusinessLogic.DTO.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.Services
{
    public interface IReservationService
    {
        Task<List<ReservationCardReturnDTO>> GetAll(int page, int entries);

        Task<invoiceBrowserDTO> CreateReservation(ReservationCreateDTO reservation);
    }
}
