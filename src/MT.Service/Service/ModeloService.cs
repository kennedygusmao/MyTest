using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KissLog;
using MT.Domain.Entities;
using MT.Domain.Interfaces.Repository;
using MT.Domain.Interfaces.Service;
using MT.Service.Interface;

namespace MT.Service.Service
{
    public class ModeloService : BaseService, IModeloService
    {
     

        private readonly IMapper _mapper;
        private readonly IModeloRepository _modeloRepository;
        private readonly INotificador _notificador;
        private readonly ILogger _logger;

        public ModeloService(INotificador notificador, IModeloRepository modeloRepository, IMapper mapper, ILogger logger) : base(notificador)
        {
            _notificador = notificador;
            _modeloRepository = modeloRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<Modelo>> ObterModelos()
        {
            return  await _modeloRepository.ObterTodos();
        }
    }
}
