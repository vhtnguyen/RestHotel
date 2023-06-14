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

        // CreateMap<invoiceBrowserDTO, Invoice>()
        //     .ForMember();

        CreateMap<InvoiceToReadDetailDTO, Invoice>()
        .ForMember(dest => dest.Date, opt => opt.Ignore())
        // .ForMember(dest => dest.Status, opt => opt.Ignore())
        .ForMember(dest => dest.TotalSum, opt => opt.Ignore())
        .ForMember(dest => dest.DownPayment, opt => opt.Ignore())
        // .ForMember(dest => dest.Email, opt => opt.Ignore())
        .ForMember(dest => dest.NameCus, opt => opt.Ignore())
        .ConstructUsing((dto, context) => new Invoice(0, DateTime.MinValue, null, 0, 0, null, string.Empty));

        CreateMap<Invoice, InvoiceToDetailDTO>()
        .ForMember(dest => dest.ReservationCards, opt => opt.MapFrom( i => i.ReservationCards))
        .ForMember(dest => dest.HotelServices, opt => opt.MapFrom( i => i.HotelServices.Select(a => a.HotelService)));

        CreateMap<InvoiceBrowserDTO, Invoice>()
        .ForMember(dest => dest.Date, opt => opt.Ignore())
        .ForMember(dest => dest.Status, opt => opt.Ignore())
        .ForMember(dest => dest.TotalSum, opt => opt.Ignore())
        .ForMember(dest => dest.DownPayment, opt => opt.Ignore())
        .ForMember(dest => dest.Email, opt => opt.Ignore())
        .ForMember(dest => dest.NameCus, opt => opt.Ignore())
        .ConstructUsing((dto, context) => new Invoice(0, DateTime.MinValue, null, 0, 0, null, string.Empty));
    }
}
