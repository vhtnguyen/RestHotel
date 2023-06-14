using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.DTO.HotelReservation
{
    public class PendingInvoiceReturnDTO
    {
        public int InvoiceId { get; set;}

        public PendingInvoiceReturnDTO(int id)
        {
            InvoiceId = id;
        }
    }
}
