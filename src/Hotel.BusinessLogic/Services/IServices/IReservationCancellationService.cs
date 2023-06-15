using Hotel.BusinessLogic.DTO.HotelReservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.Services
{
    public interface IReservationCancellationService
    {
        Task CheckConfirmedReservation(int InvoiceId);
    }
}
