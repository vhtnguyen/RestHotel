using AutoMapper;
using Hotel.DataAccess.Entities;
using Hotel.BusinessLogic.DTO.HotelReservation;
using Hotel.DataAccess.ObjectValues;

namespace Hotel.BusinessLogic.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            //guests
            CreateMap<GuestDTO, Guest>();
            CreateMap<Guest, GuestDTO>();

            CreateMap<GuestRoomInfoDTO, ReservationCard>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<ReservationCreateDTO, Invoice>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ConstructUsing(src => new Invoice(0, src.Date, null, src.TotalSum,
                src.DownPayment, src.Email, src.NameCus));

            CreateMap<Invoice, InvoiceReturnDTO>();

            CreateMap<ReservationCard, ReservationCardReturnDTO>()
            .ForMember(dest => dest.InvoiceId, opt => opt.MapFrom(card => card.Invoice.Id))
            .ForMember(dest => dest.RoomId, opt => opt.MapFrom(card => card.Room.Id))
            .ForMember(dest => dest.GuestName, opt => opt.MapFrom(card => card.Invoice.NameCus))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(card => card.Room.RoomDetail.Price))
            .ForMember(dest => dest.RoomType, opt => opt.MapFrom(card => card.Room.RoomDetail.RoomType))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(card => card.Invoice.Status))
            .ForMember(dest => dest.MaxGuests, opt => opt.MapFrom(card => card.RoomRegulation.MaxGuest))
            .ForMember(dest => dest.GuestsNumber, opt => opt.MapFrom(card => card.Guests.Count()))
            .ForMember(dest => dest.ArrivalDate, opt => opt.MapFrom(card => card.ArrivalDate.ToString("dd/MM/yyyy")))
            .ForMember(dest => dest.DepartureDate, opt => opt.MapFrom(card => card.DepartureDate.ToString("dd/MM/yyyy")));
        }
    }
}
