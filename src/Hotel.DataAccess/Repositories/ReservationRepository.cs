using Hotel.DataAccess.Context;
using Hotel.DataAccess.Entities;
using Hotel.DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DataAccess.Repositories
{
    internal class ReservationRepository : IReservationRepository
    {
        private readonly IGenericRepository<Invoice> _genericRepository;
        private readonly AppDbContext _context;

        public ReservationRepository(IGenericRepository<Invoice> genericRepository, AppDbContext context) 
        {
            _genericRepository = genericRepository;
            _context = context;
        }

        public async Task<List<ReservationCard>?> GetListAsync()
        {
            var result = await _context.ReservationCard.ToListAsync();
            return result;
        }

        public Task<ReservationCard> CreateAsync(ReservationCard card)
        {
            Console.WriteLine("hehe");

            return null;
        }
    }
}
