using Hotel.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.DataAccess.Entities;
using System.Linq.Expressions;
using Hotel.BusinessLogic.DTO.RoomRegulation;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System.CodeDom;
using Hotel.DataAccess.Repositories.IRepositories;


namespace Hotel.BusinessLogic.Services
{
    internal class RoomRegulationService : IRoomRegulationService
    {
        private readonly IRoomRegulationRepository _roomRegulationRepository;
        private readonly IMapper _mapper;
        public RoomRegulationService(IRoomRegulationRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _roomRegulationRepository = userRepository;
        }

        public async Task AddRoomRegulation(RoomRegulationToCreateDTO roomRegulation)
        {
            var room = _mapper.Map<RoomRegulation>(roomRegulation);

            await _roomRegulationRepository.CreateAsync(room);


        }

        public async Task<IEnumerable<RoomRegulationToReturnDTO>> getAllRoomRegulation()
        {
            //Expression<Func<RoomRegulation, bool>> expression = x => true;
            var roomRegulationList= await _roomRegulationRepository.BrowserAsync();
            List<RoomRegulationToReturnDTO> result=new List<RoomRegulationToReturnDTO>();
            //await _roomRegulationRepository.FindAsync(expression);
            foreach (var x in roomRegulationList)
            {

                result.Add(_mapper.Map<RoomRegulationToReturnDTO>(x));

            }

            return result;
            //throw new NotImplementedException();


        }

        public async Task<RoomRegulationToReturnDTO> getRoomByID(int id)
        {

            return _mapper.Map<RoomRegulationToReturnDTO>(await _userRepository.FindAsync(x => x.Id == id));
        }

        public async Task RemoveRoomRegulation(int id)
        {

            var entity = await _userRepository.FindAsync(x => x.Id == id);
            if (entity != null)
            {
                await _roomRegulationRepository.DeleteAsync(id, entity);

            }
            else
            {
                //throw exception here
                throw new Exception("bad request");
            }
        }

   

        public Task UpdateRoomRegulation(RoomRegulation regulation)
        {
            throw new NotImplementedException();
        }
    }
}
