using Hotel.BusinessLogic.DTO.HotelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.Services
{
    public interface IHotelServicesService
    {
        Task<IEnumerable<ServiceToReturnDTO>> GetServicesAsync();
        Task<ServiceToReturnDTO> CreateServiceAsync(ServiceToCreateDTO serviceDTO);
        Task <IEnumerable<ServiceToReturnDTO>?> SearchSeviceAsync(string? value, string searchOption, int category);
        Task RemoveServiceAsync(int id);


    }
}
