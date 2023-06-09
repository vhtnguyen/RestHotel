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
            .ConstructUsing((dto, context) => new User(0, dto.Account, dto.Password, dto.FullName, dto.Email, dto.TelephoneNumber, null));

            CreateMap<User, UserToReturnDTO>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => GetHighestRole(src.Roles)));

            CreateMap<UserToCreateDTO, UserToReturnDTO>();



            CreateMap<ServiceToReturnDTO, HotelService>()
            .ForMember(dest => dest.Category, opt => opt.Ignore()) //skip Category khi map
          .ForMember(dest => dest.Invoices, opt => opt.Ignore()) //skip invoices khi map
          .ConstructUsing((dto, context) => new HotelService(dto.Id, dto.ServiceName, dto.UnitPrice));


            CreateMap<HotelService, ServiceToReturnDTO>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => GetServiceName(src.Category)));
           

        }



        private static string GetHighestRole(ICollection<Role> roles)
        {
            if (roles == null || roles.Count == 0)
            {
                return string.Empty;
            }

            var highestRoleId = roles.MinBy(role => role.Id)?.Id;
            var highestRole = roles.FirstOrDefault(role => role.Id == highestRoleId);

            return highestRole?.NameType ?? string.Empty;
        }

        private static string GetServiceName(ServiceCategory? category)
        {
            if (category == null)
            {
                return string.Empty;
            }
            return category.Name ?? string.Empty;
        }
    }
}
