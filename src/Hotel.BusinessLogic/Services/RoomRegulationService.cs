using Hotel.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.DataAccess.Repositories;
using Hotel.DataAccess.Entities;
using System.Linq.Expressions;
namespace Hotel.BusinessLogic.Services
{
    internal class RoomRegulationService:IRoomRegulationService
    {
        private readonly IRoomRegulationRepository _userRepository;

        public RoomRegulationService(IRoomRegulationRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddRoomRegulation(RoomRegulation regulation)
        {
            _userRepository.CreateAsync(regulation);
        }

        public async Task<IEnumerable<RoomRegulation>> getAllRoomRegulation()
        {
            //Expression<Func<RoomRegulation, bool>> expression = x => true;

            return await _userRepository.BrowserAsync();
            //await _userRepository.FindAsync(expression);

            //throw new NotImplementedException();


        }

        public void RemoveRoomRegulation(RoomRegulation regulation)
        {

            _userRepository.DeleteAsync( regulation);
        }

        public void UpdateRoomRegulation(RoomRegulation regulation)
        {
            throw new NotImplementedException();
        }
    }
}
