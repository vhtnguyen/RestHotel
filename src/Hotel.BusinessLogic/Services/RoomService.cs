using AutoMapper;
using Hotel.BusinessLogic.DTO.Rooms;
using Hotel.DataAccess.Entities;
using Hotel.DataAccess.Repositories;
using Hotel.DataAccess.Repositories.IRepositories;
using Hotel.Shared.Exceptions;
using org.apache.zookeeper.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<List<RoomToReturnListDTO>> GetRoomListAsync()
        {
            var result = await _roomRepository.GetListAsync();
            return _mapper.Map<List<RoomToReturnListDTO>>(result);
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
        public async Task RemoveRoomByIDAsync(int id)
        {
            await _roomRepository.RemoveByIDAsync(id);
        }
        public async Task<List<RoomFreeToReturnDTO>?> FindFreeByDateAsync(RoomToFindFreeDTO roomToFindFreeDTO)
        {
            List<ReservationCard> CardsListByTime = await _reservationRepository
                .GetListReservationCardsByTime(roomToFindFreeDTO.From, roomToFindFreeDTO.To);

            List<int> idBookedRoomsList = new List<int>();

            foreach (ReservationCard card in CardsListByTime)
            {
                idBookedRoomsList.Add(card.Room.Id);
            }

            IEnumerable<Room>? freeRoomsList = await _roomRepository
                            .FindFreeByDateAsync(r => !idBookedRoomsList.Contains(r.Id) && r.RoomDetail.RoomType == roomToFindFreeDTO.Type);

            return _mapper.Map<List<RoomFreeToReturnDTO>>(freeRoomsList);
        }
    }
}
