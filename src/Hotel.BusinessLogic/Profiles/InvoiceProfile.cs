using AutoMapper;
using Hotel.BusinessLogic.DTO.Invoices;
using Hotel.DataAccess.Entities;

namespace Hotel.BusinessLogic.Profiles;

public class InvoiceProfile : Profile
{
    public InvoiceProfile()
    {
        CreateMap<Invoice, InvoiceToGetAllDTO>();
        CreateMap<Invoice, InvoiceToDetailDTO>()
            .ForMember(dest => dest.ReservationCards, opt => opt.MapFrom(src => src.ReservationCards))
            .ForMember(dest => dest.HotelServices, otp =>
                otp.MapFrom(src => src.HotelServices.Select(i => new ServiceToDetailDTO
                {
                    Id = i.HotelServiceId,
                    Name = i.HotelService.Name,
                    Price = i.HotelService.Price,
                    CreateOn = i.CreateOn.ToString("dd/MM/yyyy")
                })));
    }
}
