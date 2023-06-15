using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.BusinessLogic.DTO.HotelServices;
using Hotel.DataAccess.Entities;

namespace Hotel.BusinessLogic.Profiles
{
    public class HotelServiceProfile : Profile
    {
        public HotelServiceProfile()
        {
            CreateMap<ServiceToReturnDTO, HotelService>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) //skip id khi map
            .ForMember(dest => dest.Category, opt => opt.Ignore()) //skip Category khi map
          .ForMember(dest => dest.Invoices, opt => opt.Ignore()) //skip invoices khi map
          .ConstructUsing((dto, context) => new HotelService(0, dto.ServiceName, dto.UnitPrice));


            CreateMap<HotelService, ServiceToReturnDTO>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
            .ConstructUsing((dto, context) => new ServiceToReturnDTO(dto.Id, dto.Name, null, dto.Price));



            CreateMap<ServiceToCreateDTO, HotelService>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) //skip id khi map
           .ForMember(dest => dest.Category, opt => opt.Ignore()) //skip Category khi map
         .ForMember(dest => dest.Invoices, opt => opt.Ignore()) //skip invoices khi map
         .ConstructUsing((dto, context) => new HotelService(0, dto.ServiceName, dto.UnitPrice));


        }





        //private static string GetServiceName(ServiceCategory? category)
        //{
        //    if (category == null)
        //    {
        //        return string.Empty;
        //    }
        //    return category.Name ?? string.Empty;
        //}
    }
}
