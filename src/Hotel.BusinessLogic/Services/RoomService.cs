using AutoMapper;
using Hotel.BusinessLogic.DTO.Rooms;
using Hotel.DataAccess.Entities;
using Hotel.DataAccess.Repositories;
using Hotel.DataAccess.Repositories.IRepositories;
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
        //private readonly IRoomDetail _roleRepository;

        public RoomService(IRoomRepository roomRepository,
                            IMapper mapper, IReservationRepository reservationRepository)
        {
            _mapper = mapper;
            _roomRepository = roomRepository;
            _reservationRepository = reservationRepository;
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
                throw new NotImplementedException();
            }
            Room new_room = new Room(roomToCreateDTO.Id, roomToCreateDTO.Status, roomToCreateDTO.Note);

            // fake data for testing purpose
            // right code:  RoomDetail detail=_roomDetailRepository.FindAsync(roomToCreateDTO.RoomDetailID);

            RoomDetail detail = new RoomDetail(
              0, 10000, "Double", "A haunted room", null
            );
            detail.RoomRegulation = new RoomRegulation(0, 5, 4, 90, 10, 10);

            if (detail != null)
            {
                new_room.RoomDetail = detail;
                await _roomRepository.CreateAsync(new_room);
                return _mapper.Map<RoomToReturnDetailDTO>(new_room);
            }
            else
            {
                throw new NotImplementedException();
            }

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
