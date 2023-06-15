using AutoMapper;
using Hotel.BusinessLogic.DTO.RoomDetail;
using Hotel.BusinessLogic.DTO.RoomRegulation;
using Hotel.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.Profiles
{
    public class RoomDetailProfile: Profile
    {
        public RoomDetailProfile()
        {
            CreateMap<RoomDetail, RoomDetailToCreateDTO>();
            CreateMap<RoomDetailToCreateDTO, RoomDetail>().ForMember(des => des.Id, src => src.Ignore());
            CreateMap<RoomDetail, RoomDetailToReturnDTO>();
            CreateMap<RoomDetailToReturnDTO, RoomDetail>();
        }
    }
}
