using AutoMapper;
using Hotel.BusinessLogic.DTO.RoomRevenue;
using Hotel.DataAccess.ObjectValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.Profiles
{
    public class RoomRevenueProfile : Profile
    {
       public RoomRevenueProfile()
        {
            CreateMap<RoomRevenue, RoomRevenueToReturnDTO>().ForMember(src=>src.percentage, dest=>dest.Ignore());
        }
    }
}
