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
using Hotel.BusinessLogic.Services.IServices;
using Hotel.Shared.Exceptions;

namespace Hotel.BusinessLogic.Services
{
    internal class RoomRegulationService : IRoomRegulationService
    {
        private readonly IRoomRegulationRepository _roomRegulationRepository;
        private readonly IRoomDetailRepository _roomDetailRepository;
        private readonly IMapper _mapper;

        public RoomRegulationService(IRoomRegulationRepository roomRegulationRepository, IRoomDetailRepository roomDetailRepository, IMapper mapper)
        {
            _roomRegulationRepository = roomRegulationRepository;
            _roomDetailRepository = roomDetailRepository;
            _mapper = mapper;
        }

        public async Task AddRoomRegulation(RoomRegulationToCreateDTO roomRegulation)
        {
            var room = _mapper.Map<RoomRegulation>(roomRegulation);

            await _roomRegulationRepository.CreateAsync(room);


        }

        public async Task<IEnumerable<RoomRegulationToReturnDTO>> getAllRoomRegulation()
        {
            //Expression<Func<RoomRegulation, bool>> expression = x => true;
            var roomRegulationList = await _roomRegulationRepository.BrowserAsync();
            List<RoomRegulationToReturnDTO> result = new List<RoomRegulationToReturnDTO>();
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
            return _mapper.Map<RoomRegulationToReturnDTO>(await _roomRegulationRepository.FindAsync(x => x.Id == id));
        }

        public async Task RemoveRoomRegulation(int id)
        {
            var entity = await _roomRegulationRepository.FindAsync(x => x.Id == id);
            if (entity != null)
            {
                await _roomRegulationRepository.DeleteAsync(id, entity);

            }
            else
            {
                //throw exception here
                throw new DomainBadRequestException("","");
            }
        }



        public async Task UpdateRoomRegulation(int id,RoomRegulationToCreateDTO roomRegulation)
        {

   
            var findRegulation = await _roomRegulationRepository.FindAsync(x => x.Id == id);
            Console.WriteLine("haha");
            if (findRegulation == null)
            {
                throw new DomainBadRequestException("Invalid id for RoomRegulation", "id_not_existed");
            }
            var roomDetailList = await _roomDetailRepository.BrowserAsync();

            Console.WriteLine("h");

            var room = _mapper.Map<RoomRegulation>(roomRegulation);
            await _roomRegulationRepository.CreateAsync(room);

            foreach (var x in roomDetailList)
            {
                Console.WriteLine(x.RoomRegulation.Id);
                if (x.RoomRegulation.Id == id)
                {
                    Console.WriteLine("there");
                    x.RoomRegulation = room;
                    
                }
            }
            await _roomDetailRepository.SaveChangeAsync();
          

        }
    }
}
