using Hotel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Hotel.DataAccess.Repositories.IRepositories
{
    public interface IReservationRepository
    {
        Task<List<ReservationCard>?> GetListAsync();

        Task<List<ReservationCard>> GetListAsyncWithPagination(int page, int entries);

        Task<ReservationCard> CreateAsync(ReservationCard card);

        Task<Room> GetRoomById(int roomId);

        Task<Object> CreateTransaction();

        Task<Object> RollBackTranasction(Object transaction);

        Task<Object> CommitTranasction(Object transaction);

        Task<List<ReservationCard>> GetListReservationCardsByTime(DateTime from, DateTime to);
        
        Task UpdateAsync(ReservationCard card);

        Task RemoveAsync(ReservationCard card);

        Task<ReservationCard?> FindAsync(Expression<Func<ReservationCard, bool>> predicate);
    
        Task<List<ReservationCard>> FindAsyncByInvoiceID(int id);

        Task<ReservationCard?> GetReservationCardByID(int id);

        Task RemoveReservationCardByID(ReservationCard card);

        Task<int> GetTotalPages(int page, int entries);
    }
}
