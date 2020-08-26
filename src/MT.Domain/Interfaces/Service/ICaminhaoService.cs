using MT.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MT.Domain.Interfaces.Service
{
    public interface ICaminhaoService : IDisposable
    {
        Task<IEnumerable<CaminhaoDetalheDto>> ObterTodos();
        Task<CaminhaoDetalheDto> ObterPorId(Guid id);
        Task<bool> Adicionar(CaminhaoDto caminhaoDto);
        Task<bool> Atualizar(CaminhaoDto caminhaoDto);
        Task<CaminhaoDto> Remover(Guid id);
    }
}
