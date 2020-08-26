using Microsoft.EntityFrameworkCore;
using MT.Data.Context;
using MT.Domain.Entities;
using MT.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MT.Data.Repository
{
    public  class CaminhaoRepository : Repository<Caminhao>, ICaminhaoRepository
    {
        private readonly BContext _context; 
        public CaminhaoRepository(Context.BContext db) : base(db)
        {
            _context = db;
        }

        public async Task<IEnumerable<Caminhao>> ObterCaminhaoModelo()
        {
            return await _context.Caminhoes.AsNoTracking().Include(c => c.Modelo).ToListAsync();
        }

        public async Task<Caminhao> ObterCaminhaoModeloById(Guid id)
        {
            return await _context.Caminhoes.AsNoTracking().Include(c => c.Modelo).FirstOrDefaultAsync(c=>c.Id.Equals(id));
        }
    }
}
