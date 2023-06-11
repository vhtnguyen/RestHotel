using AutoMapper;
using Hotel.BusinessLogic.DTO.Invoices;
using Hotel.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.Profiles;

public class InvoiceProfile : Profile
{
    public InvoiceProfile() 
    {
        //CreateMap<Invoice, invoiceGetAllDTO>()
        //    .ForMember(dest => dest.NameGuest, opt => opt.MapFrom(src => src.ReservationCards.FirstOrDefault().Guests.Name));

        //CreateMap<invoiceBrowserDTO, Invoice>()
        //    .ForMember();
    }
}
