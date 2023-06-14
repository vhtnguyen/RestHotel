using System;
using System.Collections.Generic;
using System.Linq;
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
            CreateMap<HotelService, ServiceToDetailDTO>();
        }
    }
}