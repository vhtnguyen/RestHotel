
using AutoMapper;
using Hotel.BusinessLogic.DTO.HotelServices;
using Hotel.DataAccess.Entities;
using System.Linq.Expressions;
using Hotel.DataAccess.Repositories.IRepositories;
using Hotel.BusinessLogic.Services.IServices;
using Hotel.Shared.Exceptions;
using Microsoft.IdentityModel.Tokens;

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
        public async Task<IEnumerable<ServiceToReturnDTO>> GetServicesAsync()
        {
            var services = await _hotelServiceRepository.GetListAsync();
            return _mapper.Map<List<ServiceToReturnDTO>>(services);
        }

        public async Task<ServiceToReturnDTO> CreateServiceAsync(ServiceToCreateDTO serviceDTO)
        {
            var category = await _serviceCategoryRepository.FindAsync(serviceDTO.Category);
            if (category == null)
            {
                throw new DomainBadRequestException("", "Service category is not exist");
            }
            HotelService new_service = _mapper.Map<HotelService>(serviceDTO);
            var new_service_with_category = await _hotelServiceRepository.CreateAsync(new_service, category.Id);
            return _mapper.Map<ServiceToReturnDTO>(new_service_with_category);
        }
        public async Task<IEnumerable<ServiceToReturnDTO>?> SearchSeviceAsync(string value, string catName)
        {
            IEnumerable<HotelService>? result;
            var category = await _serviceCategoryRepository.FindAsync(catName);
            if(category == null && value.IsNullOrEmpty()){
                return await this.GetServicesAsync();
            }
         
            if (category == null) {
               result = await _hotelServiceRepository.FindAllAsync(service => service.Name!.Contains(value)&&service.Category!=null);
            }
            else if (value.IsNullOrEmpty())
            {
                result = await _hotelServiceRepository.FindAllAsync(service => service.Category!.Id == category.Id);
            }
            else
            {
                result = await _hotelServiceRepository.FindAllAsync(service => service!.Name!.Contains(value!) && service.Category!.Id == category.Id);
            }

            return _mapper.Map<IEnumerable<ServiceToReturnDTO>>(result);
        }

        public async Task RemoveServiceAsync(int id)
        {
            await _hotelServiceRepository.RemoveAsync(id);
        }

    }
}
