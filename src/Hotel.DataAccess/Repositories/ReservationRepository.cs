using Hotel.DataAccess.Context;
using Hotel.DataAccess.Entities;
using Hotel.DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Hotel.DataAccess.Repositories
{
    internal class ReservationRepository : IReservationRepository
    {
        private readonly IGenericRepository<Invoice> _genericInvoiceRepository;
        private readonly IGenericRepository<Room> _genericRoomRepository;
        private readonly IGenericRepository<ReservationCard> _genericCardRepository;
        private readonly AppDbContext _context;

        public ReservationRepository(IGenericRepository<Invoice> genericRepository, 
            IGenericRepository<ReservationCard> genericCardRepository, 
            AppDbContext context, IGenericRepository<Room> genericRoomRepository) 
        {
            _genericInvoiceRepository = genericRepository;
            _genericRoomRepository = genericRoomRepository;
            _genericCardRepository = genericCardRepository;
            _context = context;
        }

        public async Task<List<ReservationCard>?> GetListAsync()
        {
            var result = await _context.ReservationCard.ToListAsync();
            return result;
        }

        public async Task<List<ReservationCard>> GetListAsyncWithPagination(int page, int entries)
        {
            int TotalCount = await _context.ReservationCard.CountAsync();
            int TotalPages = (int)Math.Ceiling((double)TotalCount / entries);
            int Skip = (page - 1) * entries;
            var result = await _context.ReservationCard
                            .Include(card => card.Invoice)
                            .Include(card => card.Room)
                            .Skip(Skip)
                            .Take(entries)
                            .ToListAsync();
            return result;
        }

        public async Task<ReservationCard> CreateAsync(ReservationCard card)
        {
            return await _genericCardRepository.CreateAsync(card);
        }

        public async Task<Room> GetRoomById(int roomId)
        {
            // Room room = await _context.Room.Fin
            var result = await _context.Room
                                .Include(room => room.RoomDetail)
                                .ThenInclude(rD => rD.RoomRegulation)
                                .FirstOrDefaultAsync(room => room.Id == roomId);
            return result;
        }

        public async Task<Object> CreateTransaction()
        {
            var transaction = await _genericRoomRepository.CreateTransaction();
            return transaction;
        }

        public async Task<Object> RollBackTranasction(Object transaction)
        {
            IDbContextTransaction Transaction = (IDbContextTransaction) transaction;
            await Transaction.RollbackAsync();
            return true;
        }

        public async Task<Object> CommitTranasction(Object transaction)
        {
            IDbContextTransaction Transaction = (IDbContextTransaction) transaction;
            Transaction.Commit();
            return true;
        }

        public async Task<List<ReservationCard>> GetListReservationCardsByTime(DateTime from, DateTime to)
        {
            if (from == to)
            {
                to = to + new TimeSpan(23, 59, 59);
            }
            var result = await _context.ReservationCard
                                .Include(card => card.Invoice)
                                .Include(card => card.Room)
                                .Where(card => (card.ArrivalDate >= from && to >= card.DepartureDate))
                                .ToListAsync();
            return result;
        }

        public async Task UpdateAsync(ReservationCard card)
        {
            await _genericCardRepository.UpdateAsync(card);
        }

        public async Task RemoveAsync(ReservationCard card)
        {
            await _genericCardRepository.DeleteAsync(card);
        }

        public async Task<ReservationCard?> FindAsync(Expression<Func<ReservationCard, bool>> predicate) 
        => await _genericCardRepository.FindAsync(predicate);

        public async Task<List<ReservationCard>> FindAsyncByInvoiceID(int id) 
        {
            return await _context.ReservationCard
                            .Include(c => c.Room)
                            .Include(c => c.Invoice)
                            .Where(c => c.Invoice.Id == id)
                            .ToListAsync();
        }

        public async Task<ReservationCard?> GetReservationCardByID(int id)
        {
            return await _context.ReservationCard
                            .Include(c => c.Room)
                            .Include(c => c.Invoice)
                            .Where(c => c.Id == id)
                            .FirstOrDefaultAsync();
        }

        public async Task RemoveReservationCardByID(ReservationCard card)
        {
            await _genericCardRepository.DeleteAsync(card);
        }
    }
}
