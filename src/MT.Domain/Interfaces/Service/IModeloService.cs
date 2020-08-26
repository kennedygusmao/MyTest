using MT.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MT.Domain.Interfaces.Service
{
    public interface IModeloService
    {
        Task<IEnumerable<Modelo>> ObterModelos();
    }
}
