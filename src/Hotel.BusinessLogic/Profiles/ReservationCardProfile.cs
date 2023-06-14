using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.BusinessLogic.DTO.ReservationCard;
using Hotel.DataAccess.Entities;

namespace Hotel.BusinessLogic.Profiles;

public class ReservationCardProfile : Profile
{
    public ReservationCardProfile()
    {
        CreateMap<ReservationCard, ReservationCardToDetailDTO>();
    }
}
