using MT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MT.Domain.Interfaces.Repository
{
    public interface ICaminhaoRepository : IRepository<Caminhao>
    {
        Task<IEnumerable<Caminhao>> ObterCaminhaoModelo();

        Task<Caminhao> ObterCaminhaoModeloById(Guid id);

    }
}
