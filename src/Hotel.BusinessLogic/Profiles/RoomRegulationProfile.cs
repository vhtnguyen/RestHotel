using AutoMapper;
using Hotel.BusinessLogic.DTO.RoomRegulation;
using Hotel.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.Profiles
{
    public class RoomRegulationProfile : Profile
    {
        public RoomRegulationProfile()
        {
            CreateMap<RoomRegulation,RoomRegulationToCreateDTO>();
            CreateMap<RoomRegulationToCreateDTO, RoomRegulation>().ForMember(des=>des.Id, src=>src.Ignore());
        }
    }
}
