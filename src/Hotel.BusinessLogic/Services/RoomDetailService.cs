using AutoMapper;
using Hotel.BusinessLogic.DTO.RoomDetail;
using Hotel.BusinessLogic.DTO.RoomRegulation;
using Hotel.BusinessLogic.Services.IServices;
using Hotel.DataAccess.Entities;
using Hotel.DataAccess.Repositories;
using Hotel.DataAccess.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.Services
{
    internal class RoomDetailService : IRoomDetailService
    {
        private readonly IRoomDetailRepository _roomDetailRepository;
        private readonly IMapper _mapper;

        public RoomDetailService(IRoomDetailRepository userRepository, IMapper mapper)
        {
            _roomDetailRepository = userRepository;
            _mapper = mapper;
        }

        public async Task AddRoomDetail(RoomDetailToCreateDTO roomRegulation)
        {
            var room = _mapper.Map<RoomDetail>(roomRegulation);

            await _roomDetailRepository.CreateAsync(room);
        }

        public async Task<IEnumerable<RoomDetailToReturnDTO>> getAllRoomDetail()
        {
            var roomRegulationList = await _roomDetailRepository.BrowserAsync();
            List<RoomDetailToReturnDTO> result = new List<RoomDetailToReturnDTO>();
            //await _roomDetailRepository.FindAsync(expression);
            foreach (var x in roomRegulationList)
            {

                result.Add(_mapper.Map<RoomDetailToReturnDTO>(x));

            }

            return result;
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
                await _roomDetailRepository.DeleteAsync( entity);

            }
            else
            {
                //throw exception here
                //throw exception here
                throw new Exception("bad request");
            }
        
        }

        public Task UpdateRoomDetail(RoomDetail regulation)
        {
            throw new NotImplementedException();
        }
    }
}
