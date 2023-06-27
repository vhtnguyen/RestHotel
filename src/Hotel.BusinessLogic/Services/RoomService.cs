using AutoMapper;
using Hotel.BusinessLogic.DTO.Rooms;
using Hotel.DataAccess.Entities;
using Hotel.DataAccess.Repositories;
using Hotel.DataAccess.Repositories.IRepositories;
using Hotel.Shared.Exceptions;


namespace Hotel.BusinessLogic.Services
{
    public class RoomService : IRoomService
    {
        private readonly IMapper _mapper;
        private readonly IRoomRepository _roomRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomDetailRepository _roomDetailRepository;

        public RoomService(IRoomRepository roomRepository,
                            IMapper mapper, IReservationRepository reservationRepository,
                            IRoomDetailRepository roomDetail)
        {
            _mapper = mapper;
            _roomRepository = roomRepository;
            _reservationRepository = reservationRepository;
            _roomDetailRepository = roomDetail;
        }

        public async Task<RoomToReturnQueryPerPageDTO> GetRoomListAsync(int page, int pageSize)
        {
          
            var total = await _roomRepository.CountAsync();
            if(total==0|| pageSize == 0)
            {
                total = 1; 
                page = 1;
                pageSize = 1;
            }
            var totalPages =((total - 1) / pageSize) + 1;
            var roomList= await _roomRepository.GetListAsync(page, pageSize);
            var result = new RoomToReturnQueryPerPageDTO(
                totalPages, pageSize, page
                );
            result.AddList(_mapper.Map<List<RoomToReturnListDTO>>(roomList));
            return result;
        }
        public async Task<RoomToReturnDetailDTO> CreateRoomAsync(RoomToCreateDTO roomToCreateDTO)
        {

            if (await _roomRepository.FindAsync(roomToCreateDTO.Id) != null)
            {
                // room id is existed
                throw new DomainBadRequestException($"role has existed at id '{roomToCreateDTO.Id}'", "room_existed");
            }

            var roomDetail = await _roomDetailRepository.FindAsync(i => i.Id == roomToCreateDTO.RoomDetailID);
            if (roomDetail == null)
            {
                throw new DomainBadRequestException($"room detail hasn't existed at id '{roomToCreateDTO.RoomDetailID}'",
                 "room_not_existed");
            }
            var room = _mapper.Map<Room>(roomToCreateDTO);
            room.RoomDetail = roomDetail;


            var _room = await _roomRepository.CreateAsync(room);
            return _mapper.Map<RoomToReturnDetailDTO>(room);
        }

        public async Task<RoomToReturnDetailDTO> GetRoomByIDAsync(int id)
        {
            var room = await _roomRepository.FindAsync(id);
            return _mapper.Map<RoomToReturnDetailDTO>(room);
        }
        public async Task<RoomToReturnDetailDTO> UpdateRoomAsync(RoomToCreateDTO room)
        {
            var room_to_edit = await _roomRepository.FindAsync(r=>r.Id==room.Id);
            if(room_to_edit == null)
            {
                throw new DomainBadRequestException("", "");
            }
            var new_detail = await _roomDetailRepository.FindAsync(rD => rD.Id == room.RoomDetailID);

            if(new_detail == null)
            {
                throw new DomainBadRequestException("", "");
            }
            room_to_edit.Note = room.Note;
            room_to_edit.Status = room.Status;
            room_to_edit.RoomDetail = new_detail;
            var room_updated=await _roomRepository.UpdateAsync(room_to_edit);
            return _mapper.Map<RoomToReturnDetailDTO>(room_updated);

        }
        public async Task RemoveRoomByIDAsync(int id)
        {
            var room_to_remove = await _roomRepository.FindAsync(id);
            if (room_to_remove == null)
            {
                throw new NotImplementedException();
            }
            await _roomRepository.RemoveAsync(room_to_remove);
        }
        public async Task<List<RoomFreeToReturnDTO>?> FindFreeByDateAsync(RoomToFindFreeDTO roomToFindFreeDTO)
        {
            List<ReservationCard> CardsListByTime = await _reservationRepository
                .GetListReservationCardsInTime(roomToFindFreeDTO.From, roomToFindFreeDTO.To);
            
            List<int> idBookedRoomsList = new List<int>();

            foreach (ReservationCard card in CardsListByTime)
            {
                idBookedRoomsList.Add(card.Room.Id);
            }

            IEnumerable<Room>? freeRoomsList = await _roomRepository
                            .FindFreeByDateAsync(r => !idBookedRoomsList.Contains(r.Id) && r.RoomDetail.RoomType == roomToFindFreeDTO.Type);

            return _mapper.Map<List<RoomFreeToReturnDTO>>(freeRoomsList);
        }
        public async Task<List<RoomToReturnDetailDTO>> FindRooms(string roomType, string status)
        {
            roomType = roomType.ToLower();
            status = status.ToLower();
            var rooms = await _roomRepository.FindAllAsync(r => r.RoomDetail.RoomType == roomType && r.Status == status);
            return _mapper.Map<List<RoomToReturnDetailDTO>>(rooms);
        }
    }

   
}
