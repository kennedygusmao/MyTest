using MT.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MT.Web.Service
{
    public interface ICaminhaoService
    {
        Task<IEnumerable<ModeloViewModel>> ObterTodosModelos();
        Task<ReponseCaminhaoViewModel<CaminhaoDetalheViewModel>> ObterCaminhaoModelo();
        Task<ReponseCaminhaoViewModelData<Caminhao>> CadastrarCaminhao(Caminhao caminhao);

        Task<ReponseCaminhaoViewModelData<Caminhao>> ObterCaminhaoModeloPorId(Guid id);

        Task<ReponseCaminhaoViewModelData<Caminhao>> EditarCaminhao(Caminhao caminhao);

        Task<ReponseCaminhaoViewModelData<Caminhao>> DeletarCaminhao(Guid id);
    }
}
