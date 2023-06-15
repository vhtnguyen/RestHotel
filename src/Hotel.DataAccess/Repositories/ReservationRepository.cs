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

        public Task<ReservationCard> CreateAsync(ReservationCard card)
        {
            Console.WriteLine("hehe");

            return null;
        }

        public async Task<Room> GetRoomById(int roomId)
        {
            var result = await _genericRoomRepository.FindAsync(roomId);
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
            var result = await _context.ReservationCard
                                .Where(card => ((card.ArrivalDate >= from && to >= card.DepartureDate) ||
                                (card.ArrivalDate == card.DepartureDate && (card.ArrivalDate == from || card.ArrivalDate == to))))
                                .ToListAsync();
            return result;
        }

        public async Task<Invoice?> GetInvoiceByID(int id)
        {
            var result = await _genericInvoiceRepository.FindAsync(id);
            return result;
        }
    }
}
