using Hotel.BusinessLogic.DTO.HotelServices;
namespace Hotel.BusinessLogic.Services.IServices
{
    public interface IHotelServicesService
    {
        Task<IEnumerable<ServiceToReturnDTO>> GetServicesAsync();
        Task<ServiceToReturnDTO> CreateServiceAsync(ServiceToCreateDTO serviceDTO);
        Task<IEnumerable<ServiceToReturnDTO>?> SearchSeviceAsync(string? value, string searchOption, int category);
        Task RemoveServiceAsync(int id);


    }
}
