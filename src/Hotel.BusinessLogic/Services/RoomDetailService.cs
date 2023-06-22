using AutoMapper;
using Hotel.BusinessLogic.DTO.RoomDetail;
using Hotel.BusinessLogic.Services.IServices;
using Hotel.DataAccess.Entities;
using Hotel.DataAccess.Repositories.IRepositories;
using Hotel.Shared.Exceptions;

namespace Hotel.BusinessLogic.Services
{
    internal class RoomDetailService : IRoomDetailService
    {
        private readonly IRoomDetailRepository _roomDetailRepository;
        private readonly IRoomRegulationRepository _roomRegulationRepository;
        private readonly IMapper _mapper;

        public RoomDetailService(IRoomDetailRepository roomDetailRepository, IRoomRegulationRepository roomRegulationRepository, IMapper mapper)
        {
            _roomDetailRepository = roomDetailRepository;
            _roomRegulationRepository = roomRegulationRepository;
            _mapper = mapper;
        }

        public async Task<RoomDetailToReturnDTO> CreateRoomDetail(RoomDetailToCreateDTO roomDetail)
        {
            var roomRegulationID = roomDetail.RoomRegulationId;
            var roomRegulation = await _roomRegulationRepository.FindAsync(x => x.Id == roomRegulationID);
            if (roomRegulation == null)
            {
                throw new DomainBadRequestException("", "");
            }

            RoomDetail newRoomDetail = _mapper.Map<RoomDetail>(roomDetail);
            newRoomDetail.RoomRegulation = roomRegulation;
            await _roomDetailRepository.CreateAsync(newRoomDetail);

            return _mapper.Map<RoomDetailToReturnDTO>(newRoomDetail);

        }

        public async Task<IEnumerable<int>> getAllId()
        {
            var result = await _roomDetailRepository.GetAllId();
            return result;
        }

        public async Task<IEnumerable<RoomDetailToReturnDTO>> getAllRoomDetail()
        {
            var roomDetailList = await _roomDetailRepository.BrowserAsync();
            return _mapper.Map<List<RoomDetailToReturnDTO>>(roomDetailList);
            //throw new NotImplementedException();

        }

        public async Task<RoomDetailToReturnDTO> getRoomDetailByID(int id)
        {
            return _mapper.Map<RoomDetailToReturnDTO>(await _roomDetailRepository.FindAsync(x => x.Id == id));
        }

        public async Task RemoveRoomDetail(int id)
        {
            var entity = await _roomDetailRepository.FindAsync(x => x.Id == id);
            if (entity != null)
            {
                await _roomDetailRepository.DeleteAsync(entity);

            }
            else
            {
                //throw exception here
                throw new DomainBadRequestException("", "");
            }

        }

        public async Task UpdateRoomDetail(RoomDetailToReturnDTO roomDetail)
        {
            var roomDetailId = await _roomDetailRepository.FindAsync(x => x.Id == roomDetail.Id);
            if (roomDetailId == null)
            {
                throw new DomainBadRequestException("Invalid id", "update_fail");
            }
            roomDetailId = _mapper.Map<RoomDetailToReturnDTO, RoomDetail>(roomDetail, roomDetailId);
            await _roomDetailRepository.SaveChangeAsync();
        }
    }
}
