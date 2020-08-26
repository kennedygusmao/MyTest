using AutoMapper;
using KissLog;
using MT.Domain.Dtos;
using MT.Domain.Entities;
using MT.Domain.Interfaces.Repository;
using MT.Domain.Interfaces.Service;
using MT.Service.Interface;
using MT.Service.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.Service.Service
{
    public class CaminhaoService : BaseService, ICaminhaoService
    {

        private readonly IMapper _mapper;
        private readonly ICaminhaoRepository _caminhaoRepository;
        private readonly IModeloRepository _modeloRepository;
        private readonly INotificador _notificador;
        private readonly ILogger _logger;

        public CaminhaoService(INotificador notificador,
                               ICaminhaoRepository caminhaoRepository,
                               IModeloRepository modeloRepository,
                               IMapper mapper,
                               ILogger logger) : base(notificador)
        {
            _notificador = notificador;
            _caminhaoRepository = caminhaoRepository;
            _mapper = mapper;
            _logger = logger;
            _modeloRepository = modeloRepository;

        }


        public async Task<bool> Adicionar(CaminhaoDto caminhaoDto)
        {
            var caminhao = _mapper.Map<Caminhao>(caminhaoDto);

            var modelo = await _modeloRepository.ObterPorId(caminhaoDto.ModeloId);

            if (modelo == null)
            {
                Notificar("Não existe um veículo cadastrado com esse modelo.");
                _logger.Info($"Não existe um veículo cadastrado com o modelo { caminhao.Modelo}  no sistemas.");
                return false;
            }

            caminhao.Modelo = modelo;

            if (!ExecutarValidacao(new CaminhaoValidation(), caminhao))
            {
                return false;
            }

            if (_caminhaoRepository.Buscar(f => f.Id == caminhao.Id).Result.Any())
            {
                Notificar("Já existe um veículo cadastrado com este chassi informado.");
                _logger.Info($"Veículo com chassi {caminhao.Id} já cadastrado no sistemas.");
                return false;
            }

           
           
            caminhao.CreateAt = DateTime.UtcNow;
            caminhao.Modelo = null;
            await _caminhaoRepository.Adicionar(caminhao);
            caminhaoDto.Id = caminhao.Id;
            var teste = await _modeloRepository.ObterTodos();
            return true;
        }

        public async Task<bool> Atualizar(CaminhaoDto caminhaoDto)
        {
            var caminhao = _mapper.Map<Caminhao>(caminhaoDto);

            var modelo = await _modeloRepository.ObterPorId(caminhaoDto.ModeloId);

            if (modelo == null)
            {
                Notificar("Não existe um veículo cadastrado com esse modelo.");
                _logger.Info($"Não existe um veículo cadastrado com o modelo { caminhao.Modelo}  no sistemas.");
                return false;
            }

            caminhao.Modelo = modelo;

            if (!ExecutarValidacao(new CaminhaoValidation(), caminhao))
            {
                return false;
            }

             var result = await _caminhaoRepository.ObterCaminhaoModeloById(caminhao.Id);
          

            if (result==null)
            {
                Notificar("Não existe um veículo cadastrado com esse id.");
                _logger.Info($"Não existe um veículo cadastrado com o id { caminhao.Id} cadastrado no sistemas.");
                return false;
            }
           

          
            caminhao.UpdateAt = DateTime.UtcNow;          
            await _caminhaoRepository.Atualizar(caminhao);
            return true;
        }

      

        public async Task<CaminhaoDetalheDto> ObterPorId(Guid id)
        {
            var result = _mapper.Map<CaminhaoDetalheDto>( await _caminhaoRepository.ObterCaminhaoModeloById(id));

            if (result == null)
            {
                Notificar("Veículo não encontrado no sistemas.");
                _logger.Info($"Veículo com chassi {id} não encontrado no sistemas.");
                return new CaminhaoDetalheDto();
            }

            return result;


        }

        public async Task<IEnumerable<CaminhaoDetalheDto>> ObterTodos()
        {
            var result = await _caminhaoRepository.ObterCaminhaoModelo();

            var query = (from a in result
                         select new CaminhaoDetalheDto()
                         {
                             Id = a.Id,
                             AnoFabricacao = a.AnoFabricacao,
                             AnoModelo = a.AnoModelo,
                             Modelo = a.Modelo.Descricao

                         }).ToList();

           

            return query;
        }

        public async Task<CaminhaoDto> Remover(Guid id)
        {
            var result = await _caminhaoRepository.Buscar(c => c.Id.Equals(id));

            if (result.Any())
            {

                await _caminhaoRepository.Remover(result.FirstOrDefault().Id);
                var resultVeiculo = _mapper.Map<CaminhaoDto>(result.FirstOrDefault());

                _logger.Info($"O veículo com o id {id} foi removido no sistemas.");
                return resultVeiculo;


            }
            else
                Notificar("Veículo não encontrado no sistemas.");

            return null;
        }

        public void Dispose()
        {
            _caminhaoRepository?.Dispose();

        }
    }
}