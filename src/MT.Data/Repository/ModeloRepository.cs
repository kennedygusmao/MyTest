using MT.Data.Context;
using MT.Domain.Entities;
using MT.Domain.Interfaces.Repository;

namespace MT.Data.Repository
{
    public class ModeloRepository : Repository<Modelo>, IModeloRepository
    {
        private readonly BContext _context;
        public ModeloRepository(Context.BContext db) : base(db)
        {
            _context = db;
        }
    }
}
