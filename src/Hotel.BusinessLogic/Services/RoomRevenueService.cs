using AutoMapper;
using Hotel.BusinessLogic.DTO.RoomRevenue;
using Hotel.BusinessLogic.Services.IServices;
using Hotel.DataAccess.ObjectValues;
using Hotel.DataAccess.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StackExchange.Redis.Role;

namespace Hotel.BusinessLogic.Services
{
    internal class RoomRevenueService : IRoomRevenueService
    {
        private readonly IRoomRevenueRepository _roomRevenueRepository;
        private readonly IMapper _mapper;
        public RoomRevenueService(IRoomRevenueRepository roomRevenueRepository, IMapper mapper)
        {
            _roomRevenueRepository = roomRevenueRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<RoomRevenueToReturnDTO>> getAll()
        {
           
            var result= await _roomRevenueRepository.BrowserAsync();
    
            List<RoomRevenueToReturnDTO> resultDTO = new List<RoomRevenueToReturnDTO>();
            //await _roomRegulationRepository.FindAsync(expression);
            foreach (var x in result)
            {

                resultDTO.Add(_mapper.Map<RoomRevenueToReturnDTO>(x));

            }
            foreach (var x in resultDTO){
                x.getPercentage(resultDTO);
            }
            return resultDTO;
         
        }

  
    }
}
