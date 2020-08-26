using Microsoft.Extensions.Options;
using MT.Web.Extensions;
using MT.Web.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MT.Web.Service
{
    public class CaminhaoService : Service, ICaminhaoService
    {
        private readonly HttpClient _httpClient;

        public CaminhaoService(HttpClient httpClient,
                                   IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.CaminhaoUrl);

            _httpClient = httpClient;
        }

        public async Task<ReponseCaminhaoViewModel<CaminhaoDetalheViewModel>> ObterCaminhaoModelo()
        {

            var response = await _httpClient.GetAsync("/api/v1/caminhao");

            if (!TratarErrosResponse(response))
            {
                return await DeserializarObjetoResponse<ReponseCaminhaoViewModel<CaminhaoDetalheViewModel>>(response);

            }

            return await DeserializarObjetoResponse<ReponseCaminhaoViewModel<CaminhaoDetalheViewModel>>(response);

        }

        public async Task<ReponseCaminhaoViewModelData<Caminhao>> CadastrarCaminhao(Caminhao caminhao)
        {

            var caminhaoContent = ObterConteudo(caminhao);

            var response = await _httpClient.PostAsync("/api/v1/caminhao", caminhaoContent);

            if (!TratarErrosResponse(response))
            {
                return await DeserializarObjetoResponse<ReponseCaminhaoViewModelData<Caminhao>>(response);

            }

            return await DeserializarObjetoResponse<ReponseCaminhaoViewModelData<Caminhao>>(response);
        }

        public async Task<ReponseCaminhaoViewModelData<Caminhao>> EditarCaminhao(Caminhao caminhao)
        {

            var caminhaoContent = ObterConteudo(caminhao);

            var response = await _httpClient.PutAsync("/api/v1/caminhao", caminhaoContent);

            if (!TratarErrosResponse(response))
            {
                return await DeserializarObjetoResponse<ReponseCaminhaoViewModelData<Caminhao>>(response);

            }

            return await DeserializarObjetoResponse<ReponseCaminhaoViewModelData<Caminhao>>(response);
        }


        public async Task<ReponseCaminhaoViewModelData<Caminhao>> DeletarCaminhao(Guid id) {

            var response = await _httpClient.DeleteAsync($"/api/v1/caminhao/{id}");

            if (!TratarErrosResponse(response))
            {
                return await DeserializarObjetoResponse<ReponseCaminhaoViewModelData<Caminhao>>(response);

            }

            return await DeserializarObjetoResponse<ReponseCaminhaoViewModelData<Caminhao>>(response);
        }
        public async Task<ReponseCaminhaoViewModelData<Caminhao>> ObterCaminhaoModeloPorId(Guid id)
        {           
            var response = await _httpClient.GetAsync($"/api/v1/caminhao/{id}");

            if (!TratarErrosResponse(response))
            {
                return await DeserializarObjetoResponse<ReponseCaminhaoViewModelData<Caminhao>>(response);

            }

            return await DeserializarObjetoResponse<ReponseCaminhaoViewModelData<Caminhao>>(response);
        }

        public async Task<IEnumerable<ModeloViewModel>> ObterTodosModelos()
        {

            var response = await _httpClient.GetAsync("/api/v1/modelo");

            return await DeserializarObjetoResponse<IEnumerable<ModeloViewModel>>(response);
        }
    }
}
