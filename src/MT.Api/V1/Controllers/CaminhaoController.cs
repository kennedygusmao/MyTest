using KissLog;
using Microsoft.AspNetCore.Mvc;
using MT.API.Controllers;
using MT.Domain.Dtos;
using MT.Domain.Interfaces.Service;
using MT.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MT.Api.V1.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/caminhao")]
    public class CaminhaoController : MainController
    {

        private readonly ICaminhaoService _caminhaoService;
        private readonly ILogger _logger;

        public CaminhaoController(INotificador notificador, ICaminhaoService caminhaoService, ILogger logger) : base(notificador)
        {
            _caminhaoService = caminhaoService;
            _logger = logger;
        }
        /// <summary>
        /// Consulta todos os caminhões
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaminhaoDetalheDto>>> ObterTodos()
        {
            _logger.Info(string.Format("{0} data : {1}", "Obter todos caminhões", DateTime.UtcNow));
            _logger.Info("Obter todos caminhões");
            return CustomResponse(await _caminhaoService.ObterTodos());

        }

        /// <summary>
        ///  Obter caminhão por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CaminhaoDetalheDto>> ObterPorId(Guid id)
        {
            _logger.Info(string.Format("{0}  id : {2} data : {1}", "Obter caminhão por id ." ,id, DateTime.UtcNow));
            return CustomResponse(await _caminhaoService.ObterPorId(id));
        }

        /// <summary>
        /// Adicionar caminhão
        /// </summary>
        /// <param name="caminhaoDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CaminhaoDto>> Adicionar([FromBody] CaminhaoDto caminhaoDto)
        {
            _logger.Info(string.Format("{0}   data : {1}", "Adicionar caminhão .",  DateTime.UtcNow));
          
            await _caminhaoService.Adicionar(caminhaoDto);

            return CustomResponse(caminhaoDto);
        }


        /// <summary>
        /// Atualizar caminhão
        /// </summary>
        /// <param name="caminhaoDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<CaminhaoDto>> Atualizar([FromBody] CaminhaoDto caminhaoDto)
        {
                      

            _logger.Info(string.Format("{0}  id : {2} data : {1}", "Alterar caminhão  .", caminhaoDto.Id, DateTime.UtcNow));

            await _caminhaoService.Atualizar(caminhaoDto);

            return CustomResponse(caminhaoDto);
        }


        /// <summary>
        /// Excluir caminhão
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<CaminhaoDto>> Excluir(Guid id)
        {
            _logger.Info(string.Format("{0}  id : {2} data : {1}", "Excluir caminhão  .", id, DateTime.UtcNow));

            var caminhao = await _caminhaoService.Remover(id);
            return CustomResponse(caminhao);
        }
    }
}