
using AutoMapper;
using Hotel.BusinessLogic.DTO.HotelServices;
using Hotel.DataAccess.Entities;
using System.Linq.Expressions;
using Hotel.DataAccess.Repositories.IRepositories;
using Hotel.BusinessLogic.Services.IServices;

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
            HotelService new_service = _mapper.Map<HotelService>(serviceDTO);
            var new_service_with_category = await _hotelServiceRepository.CreateAsync(new_service, serviceDTO.Category);
            return _mapper.Map<ServiceToReturnDTO>(new_service_with_category);
        }
        public async Task<IEnumerable<ServiceToReturnDTO>?> SearchSeviceAsync(string? value, string searchOption, int category)
        {
            IEnumerable<HotelService>? result;
            if (searchOption == "all" && category == 0)
            {
                return await this.GetServicesAsync();
            }


            switch (searchOption)
            {
                case "id":
                    {
                        Expression<Func<HotelService, bool>> predicate = service => service.Id.ToString().Contains(value!) && (category == 0 || service!.Category!.Id == category);
                        result = await _hotelServiceRepository.FindAllAsync(predicate);
                        break;
                    }

                case "name":
                    {
                        Expression<Func<HotelService, bool>> predicate = service => service!.Name!.Contains(value!) && (category == 0 || service!.Category!.Id == category);
                        result = await _hotelServiceRepository.FindAllAsync(predicate);
                        break;
                    }


                case "all":

                    {
                        Expression<Func<HotelService, bool>> predicate = service => service!.Category!.Id == category;
                        result = await _hotelServiceRepository.FindAllAsync(predicate);
                        break;
                    }
                default:

                    throw new ArgumentException("Invalid search option.", nameof(searchOption));

            }
            return _mapper.Map<IEnumerable<ServiceToReturnDTO>>(result);
        }

        public async Task RemoveServiceAsync(int id)
        {
            await _hotelServiceRepository.RemoveAsync(id);
        }

    }
}
