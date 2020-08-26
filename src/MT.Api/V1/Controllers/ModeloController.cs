using KissLog;
using Microsoft.AspNetCore.Mvc;
using MT.API.Controllers;
using MT.Domain.Interfaces.Repository;
using MT.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MT.Api.V1.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/modelo")]
    public class ModeloController : MainController
    {

        private readonly IModeloRepository _modeloRepository;
        private readonly ILogger _logger;

        public ModeloController(IModeloRepository modeloRepository, INotificador notificador, ILogger logger) : base(notificador)
        {
            _modeloRepository = modeloRepository;
            _logger = logger;
        }
        /// <summary>
        /// Consulta todos os modelos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Object>>> ObterTodos()
        {

            return await _modeloRepository.ObterTodos();

        }


    }
}