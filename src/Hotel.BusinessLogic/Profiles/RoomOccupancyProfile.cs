using AutoMapper;
using Hotel.BusinessLogic.DTO.RoomOccupancy;
using Hotel.DataAccess.ObjectValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.Profiles
{
    public class RoomOccupancyProfile : Profile
    {
        public RoomOccupancyProfile()
        {
            CreateMap<RoomOccupancy, RoomOccupancyToReturnDTO>();
        }
    }
}
