using AutoMapper;
using Hotel.BusinessLogic.DTO.RoomOccupancy;
using Hotel.BusinessLogic.DTO.RoomRevenue;
using Hotel.BusinessLogic.Services.IServices;
using Hotel.DataAccess.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.Services
{
    internal class RoomOccupancyService : IRoomOccupancyService
    {
        private readonly IRoomOccupancyRepository _roomOccupancyRepository;
        private readonly IMapper _mapper;

        public RoomOccupancyService(IRoomOccupancyRepository roomOccupancyService, IMapper mapper)
        {
            this._roomOccupancyRepository = roomOccupancyService;
            _mapper = mapper;

        }

        public async Task<IEnumerable<RoomOccupancyToReturnDTO>> getAll()
        {
            var result = await _roomOccupancyRepository.BrowserAsync();

            List<RoomOccupancyToReturnDTO> resultDTO = new List<RoomOccupancyToReturnDTO>();
            //await _roomRegulationRepository.FindAsync(expression);
            foreach (var x in result)
            {

                resultDTO.Add(_mapper.Map<RoomOccupancyToReturnDTO>(x));

            }
            foreach (var x in resultDTO)
            {
                x.getPercentage(resultDTO);
            }
            return resultDTO;

        }
    }
}
