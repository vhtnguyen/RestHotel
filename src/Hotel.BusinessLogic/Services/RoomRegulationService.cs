using Hotel.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.DataAccess.Repositories;
using Hotel.DataAccess.Entities;
using System.Linq.Expressions;
using Hotel.BusinessLogic.DTO.RoomRegulation;
using AutoMapper;

namespace Hotel.BusinessLogic.Services
{
    internal class RoomRegulationService:IRoomRegulationService
    {
        private readonly IRoomRegulationRepository _userRepository;
        private readonly IMapper _mapper;
        public RoomRegulationService(IRoomRegulationRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
            
        public async Task AddRoomRegulation(RoomRegulationToCreateDTO roomRegulation)
        {
            var room = _mapper.Map<RoomRegulation>(roomRegulation);
            Console.WriteLine(room.Id);
            await _userRepository.CreateAsync(room);
        }

        public async Task<IEnumerable<RoomRegulation>> getAllRoomRegulation()
        {
            //Expression<Func<RoomRegulation, bool>> expression = x => true;

            return await _userRepository.BrowserAsync();
            //await _userRepository.FindAsync(expression);

            //throw new NotImplementedException();


        }

        public async Task<RoomRegulation> getRoomByID(int id)
        {
          return   await _userRepository.FindAsync(x => x.Id == id);
        }

        public async Task RemoveRoomRegulation(int id)
        {

        await    _userRepository.DeleteAsync( id);
        }

        public Task UpdateRoomRegulation(RoomRegulation regulation)
        {
            throw new NotImplementedException();
        }
    }
}
