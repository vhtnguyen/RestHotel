
using Hotel.DataAccess.Repositories;
using AutoMapper;
using Hotel.BusinessLogic.DTO.HotelServices;
using Hotel.DataAccess.Entities;

namespace Hotel.BusinessLogic.Services
{
    public class HotelServicesService : IHotelServicesService
    {
        private readonly IMapper _mapper;
        private readonly IHotelServiceRepository _hotelServiceRepository;
        private readonly IServiceCategoryRepository _serviceCategoryRepository;

        public HotelServicesService(IMapper mapper, IHotelServiceRepository hotelServiceRepository, IServiceCategoryRepository serviceCategoryRepository)
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

        public async Task<ServiceToReturnDTO> CreateServiceAsync(ServiceToCreateDTO serviceDTO)
        {
            HotelService new_service = _mapper.Map<HotelService>(serviceDTO);
            var new_service_with_category = await _hotelServiceRepository.CreateAsync(new_service, serviceDTO.Category);

            return _mapper.Map<ServiceToReturnDTO>(new_service_with_category);
        }

        public async Task RemoveServiceAsync(int id)
        {
            await _hotelServiceRepository.RemoveAsync(id);
        }

    }
}
