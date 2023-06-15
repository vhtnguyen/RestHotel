using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.BusinessLogic.DTO.HotelServices;
using Hotel.BusinessLogic.DTO.Rooms;
using Hotel.BusinessLogic.DTO.Users;
using Hotel.DataAccess.Entities;

namespace Hotel.BusinessLogic.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomToReturnDetailDTO>()
            .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.RoomDetail.RoomType))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.RoomDetail.Price))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.RoomDetail.Description))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.RoomDetail.Image))
            .ForMember(dest => dest.MaxGuest, opt => opt.MapFrom(src => src.RoomDetail.RoomRegulation.MaxGuest))
            .ForMember(dest => dest.DefaultGuest, opt => opt.MapFrom(src => src.RoomDetail.RoomRegulation.DefaultGuest))
            .ForMember(dest => dest.MaxSurchargeRatio, opt => opt.MapFrom(src => src.RoomDetail.RoomRegulation.MaxSurchargeRatio))
            .ForMember(dest => dest.MaxOverseaSurchargeRatio, opt => opt.MapFrom(src => src.RoomDetail.RoomRegulation.MaxOverseaSurchargeRatio))
            .ForMember(dest => dest.RoomExchangeFee, opt => opt.MapFrom(src => src.RoomDetail.RoomRegulation.RoomExchangeFee));

            CreateMap<Room, RoomToReturnListDTO>()
                .ForMember(dest => dest.RoomDetail, opt => opt.MapFrom(src => src.RoomDetail.Id))
                .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.RoomDetail.RoomType))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.RoomDetail.Price));

        }
    }
}
