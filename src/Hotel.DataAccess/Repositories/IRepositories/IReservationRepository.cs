using Hotel.DataAccess.Entities;
using System.Linq.Expressions;

namespace Hotel.DataAccess.Repositories.IRepositories
{
    public interface IReservationRepository
    {
        Task<ReservationCard?> GetAsync(int id);
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
    }
}
