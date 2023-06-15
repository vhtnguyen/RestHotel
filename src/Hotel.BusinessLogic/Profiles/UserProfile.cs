using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.BusinessLogic.DTO.HotelServices;
using Hotel.BusinessLogic.DTO.Users;
using Hotel.DataAccess.Entities;

namespace Hotel.BusinessLogic.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserToCreateDTO, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // skip Id khi map
            .ForMember(dest => dest.Avatar, opt => opt.Ignore()) //skip Avatar khi map, lúc create không có tạo avt
            .ForMember(dest=>dest.Role, opt => opt.Ignore())
            .ConstructUsing((dto, context) => new User(0, dto.Account, dto.Password, dto.FullName, dto.Email, dto.TelephoneNumber, null));

            CreateMap<User, UserToReturnDTO>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src=>src.Role.NameType));

            CreateMap<UserToCreateDTO, UserToReturnDTO>();
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
