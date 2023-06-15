﻿using AutoMapper;
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
        //private readonly IRoomDetail _roleRepository;

        public RoomService(IRoomRepository roomRepository, IMapper mapper)
        {
            _mapper = mapper;
            _roomRepository = roomRepository;
        }

        public async Task<List<RoomToReturnListDTO>> GetRoomListAsync()
        {
            var result = await _roomRepository.GetListAsync();
            return _mapper.Map<List<RoomToReturnListDTO>>(result);
        }
        public async Task<RoomToReturnDetailDTO> CreateRoomAsync(RoomToCreateDTO roomToCreateDTO)
        {
            Room new_room = new Room(roomToCreateDTO.Id, roomToCreateDTO.Status, roomToCreateDTO.Note);

            // fake data for testing purpose
            // right code:  RoomDetail detail=_roomDetailRepository.FindByID(roomToCreateDTO.RoomDetailID);

            RoomDetail detail = new RoomDetail(
              0, 999, "Double", "A haunted room", null
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
    }
}
