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
        CreateMap<Invoice, InvoiceToGetAllDTO>();
        CreateMap<Invoice, InvoiceToDetailDTO>()
            .ForMember(dest => dest.ReservationCards, opt => opt.MapFrom(src => src.ReservationCards))
            .ForMember(dest => dest.HotelServices, otp =>
                otp.MapFrom(src => src.HotelServices.Select(i => i.HotelService)));
    }
}
