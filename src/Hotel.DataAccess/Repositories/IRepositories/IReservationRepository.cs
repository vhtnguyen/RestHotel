using Hotel.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DataAccess.Repositories.IRepositories
{
    public interface IReservationRepository
    {
        Task<List<ReservationCard>?> GetListAsync();

        Task<ReservationCard> CreateAsync(ReservationCard card);
    }
}
