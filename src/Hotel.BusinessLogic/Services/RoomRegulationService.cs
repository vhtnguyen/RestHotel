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
    
            await _userRepository.CreateAsync(room);
        }

        public async Task<IEnumerable<RoomRegulationToReturnDTO>> getAllRoomRegulation()
        {
            //Expression<Func<RoomRegulation, bool>> expression = x => true;

            var roomRegulationList= await _userRepository.BrowserAsync();
            List<RoomRegulationToReturnDTO> result=new List<RoomRegulationToReturnDTO>();
            //await _userRepository.FindAsync(expression);
            foreach (var x in roomRegulationList)
            {
          
                result.Add(_mapper.Map<RoomRegulationToReturnDTO>(x));

            }
  
            return result;
            //throw new NotImplementedException();


        }

        public async Task<RoomRegulationToReturnDTO> getRoomByID(int id)
        {
          return    _mapper.Map< RoomRegulationToReturnDTO >(await _userRepository.FindAsync(x => x.Id == id));
        }

        public async Task RemoveRoomRegulation(int id)
        {
            var entity = await _userRepository.FindAsync(x => x.Id == id);
            if(entity != null)
            {
                await _userRepository.DeleteAsync(id, entity);

            }
            else
            {
                //throw exception here
            }
        }

        public Task UpdateRoomRegulation(RoomRegulation regulation)
        {
            throw new NotImplementedException();
        }
    }
}
