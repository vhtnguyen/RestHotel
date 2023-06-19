using AutoMapper;
using Hotel.BusinessLogic.DTO.RoomOccupancy;
using Hotel.BusinessLogic.Services.IServices;
using Hotel.DataAccess.Repositories.IRepositories;


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

        public async Task<IEnumerable<RoomOccupancyToReturnDTO>> getByRoomDetailId(int id)
        {
            var result = await _roomOccupancyRepository.FindByRoomDetailFilters(id);

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

        public async Task<IEnumerable<RoomOccupancyToReturnDTO>> getByTypeAndMonth(int id,int month,int year)
        {
            var result = await _roomOccupancyRepository.FindByAllFilters(id, month, year);

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
