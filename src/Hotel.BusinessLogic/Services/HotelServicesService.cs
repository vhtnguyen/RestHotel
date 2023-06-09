using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.DataAccess.Repositories;
using Hotel.BusinessLogic.DTO.Users;
using AutoMapper;
using Hotel.DataAccess.Entities;
using Hotel.BusinessLogic.Profiles;
using Microsoft.Extensions.Hosting;
using Hotel.BusinessLogic.DTO.HotelServices;

namespace Hotel.BusinessLogic.Services
{
    public class HotelService : IHotelServicesService
    {
        private readonly IMapper _mapper;
        private readonly IHotelServiceRepository _hotelServiceRepository;
        private readonly IServiceCategoryRepository _serviceCategoryRepository;

        public HotelService(IMapper mapper, IHotelServiceRepository hotelServiceRepository, IServiceCategoryRepository serviceCategoryRepository)
        {
            _mapper = mapper;
            _hotelServiceRepository = hotelServiceRepository;
            _serviceCategoryRepository = serviceCategoryRepository;
        }
        public async Task<List<ServiceToReturnDTO>> GetServicesAsync()
        {
            var ServicesToReturn = await _hotelServiceRepository.GetListAsync();
            return _mapper.Map<List<ServiceToReturnDTO>>(ServicesToReturn);
        }

    }
}
