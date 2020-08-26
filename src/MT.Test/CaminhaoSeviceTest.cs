using AutoMapper;
using KissLog;
using Microsoft.EntityFrameworkCore;
using Moq;
using MT.Data.Context;
using MT.Data.Mapper;
using MT.Data.Repository;
using MT.Domain.Dtos;
using MT.Domain.Entities;
using MT.Domain.Interfaces.Repository;
using MT.Domain.Interfaces.Service;
using MT.Service.Interface;
using MT.Service.Notificacoes;
using MT.Service.Service;
using System;
using System.Linq;
using Xunit;

namespace MT.Test
{
    public class CaminhaoSeviceTest
    {

        private readonly BContext _contextMemory;
        private readonly ICaminhaoService _serviceMemory;
        private readonly IMapper mapperMemory;
        private readonly ICaminhaoRepository _repositoryMemory;
        private readonly IModeloRepository _repositoryModeloMemory;
        private readonly INotificador _notificadorMemory;
        private readonly Guid caminhao1 = Guid.NewGuid();
        private readonly Guid caminhao2 = Guid.NewGuid();
        private readonly Guid modelo1 = Guid.NewGuid();
        private readonly Guid modelo2 = Guid.NewGuid();
        private readonly Guid modelo3 = Guid.NewGuid();
        private readonly Mock<ILogger> logger;
       



        public CaminhaoSeviceTest()
        {
            logger = new Mock<ILogger>();

            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile<AutomapperConfig>();
            });
            mapperMemory = mapperConfig.CreateMapper();

            _contextMemory = InMemoryContextFactory.Create();

            this._notificadorMemory = new Notificador();

            this._repositoryMemory = new CaminhaoRepository(_contextMemory);
            this._repositoryModeloMemory = new ModeloRepository(_contextMemory);
            this._serviceMemory = new CaminhaoService(_notificadorMemory, _repositoryMemory, _repositoryModeloMemory, mapperMemory, logger.Object);

            ConfigInMemory();
        }


        private void ConfigInMemory()
        {


            Modelo modelo;

            modelo = new Modelo()
            {
                Id= modelo1,
                Descricao = "FH"
            };
            _contextMemory.Modelos.Add(modelo);

            modelo = new Modelo()
            {
                Id = modelo2,
                Descricao = "FM"
            };
            _contextMemory.Modelos.Add(modelo);

            modelo = new Modelo()
            {
                Id = modelo3,
                Descricao = "FD"
            };
            _contextMemory.Modelos.Add(modelo);
            _contextMemory.SaveChanges();

            var modeloFH = _contextMemory.Modelos.FirstOrDefault(c => c.Descricao == "FH");

            Caminhao caminhao;


            caminhao = new Caminhao(caminhao1, DateTime.UtcNow.Year, DateTime.UtcNow.AddYears(1).Year, DateTime.UtcNow, modeloFH);

            _contextMemory.Caminhoes.Add(caminhao);

            var modeloFM = _contextMemory.Modelos.FirstOrDefault(c => c.Descricao == "FM");

            caminhao = new Caminhao(caminhao2, DateTime.UtcNow.Year, DateTime.UtcNow.Year, DateTime.UtcNow, modeloFM);

         

            _contextMemory.Caminhoes.Add(caminhao);
            _contextMemory.SaveChanges();


            var entity1 = _contextMemory.Find<Caminhao>(caminhao1); //To Avoid tracking error
            _contextMemory.Entry(entity1).State = EntityState.Detached;
            var entity2 = _contextMemory.Find<Caminhao>(caminhao2); //To Avoid tracking error
            _contextMemory.Entry(entity2).State = EntityState.Detached;

            var entity3 = _contextMemory.Find<Modelo>(modelo1); //To Avoid tracking error
            _contextMemory.Entry(entity3).State = EntityState.Detached;
            var entity4 = _contextMemory.Find<Modelo>(modelo2); //To Avoid tracking error
            _contextMemory.Entry(entity4).State = EntityState.Detached;
            var entity5 = _contextMemory.Find<Modelo>(modelo3); //To Avoid tracking error
            _contextMemory.Entry(entity5).State = EntityState.Detached;


        }

        private bool OperacaoValida()
        {
            return !_notificadorMemory.TemNotificacao();
        }

        [Fact]
        public void DeveObterCaminhaoPorId()
        {

            var id = caminhao1;
            var result = _serviceMemory.ObterPorId(caminhao1).Result;
            var operacaoIsValid = OperacaoValida();

            Assert.NotNull(result);
            Assert.True(operacaoIsValid);
            Assert.Equal(id, result.Id);

        }

        [Fact]
        public void DeveObterTodosCaminhoes()
        {

            var quantidadeCaminhoes = _contextMemory.Caminhoes.Count();
            var result = _serviceMemory.ObterTodos().Result;
            var operacaoIsValid = OperacaoValida();


            Assert.NotNull(result);
            Assert.True(operacaoIsValid);
            Assert.Equal(quantidadeCaminhoes, result.Count());

        }

        [Fact]
        public void DeveCadastrarUmCaminhao()
        {
            var id = Guid.NewGuid();       

            
            var caminhao = new CaminhaoDto()
            {
                Id = id,
                AnoFabricacao = DateTime.UtcNow.Year,
                AnoModelo = DateTime.UtcNow.AddYears(1).Year,
                ModeloId = modelo1

            };


            var result = _serviceMemory.Adicionar(caminhao).Result;
            var operacaoIsValid = OperacaoValida();
            var notificacao = _notificadorMemory.ObterNotificacoes();

            var caminhaoCadastrado = _serviceMemory.ObterPorId(id).Result;

            Assert.NotNull(caminhaoCadastrado);
            Assert.True(operacaoIsValid);
            Assert.Equal(id, caminhaoCadastrado.Id);
          

        }

      

        [Fact]
        public void DeveAlterarUmCaminhao()
        {
          
            var anoModelo = DateTime.UtcNow.AddYears(1).Year;

            var caminhaoCadastrado = _serviceMemory.ObterPorId(caminhao1).Result;

          
            var caminhao = new CaminhaoDto()
            {
                Id = caminhaoCadastrado.Id,
                AnoModelo = DateTime.UtcNow.AddYears(1).Year,
                AnoFabricacao = DateTime.UtcNow.Year,
                ModeloId = modelo1
            };

            var result = _serviceMemory.Atualizar(caminhao).Result;

            var caminhaoAlterado = _serviceMemory.ObterPorId(caminhao1).Result;


            Assert.NotNull(caminhaoAlterado);
            Assert.Equal(caminhao1, caminhaoCadastrado.Id);
            Assert.Equal(anoModelo, caminhaoAlterado.AnoModelo);

        }


        [Fact]
        public void DeveExcluirUmVeiculo()
        {
           
            var caminhaoCadastrado = _serviceMemory.ObterPorId(caminhao1).Result;
            var result = _serviceMemory.Remover(caminhaoCadastrado.Id).Result;
            var caminhoes = _serviceMemory.ObterTodos().Result;

            var caminhaodeletado = caminhoes.FirstOrDefault(c => c.Id == caminhao1);

            Assert.Null(caminhaodeletado);

        }

        [Fact]
        public void DeveValidarCaminhaoNaoEncontrado()
        {
            var id = Guid.NewGuid();
            var mensagem = "Veículo não encontrado no sistemas.";

            var caminhaoCadastrado = _serviceMemory.ObterPorId(id).Result;
            var notificacao = _notificadorMemory.ObterNotificacoes();
            var mensagemValidacao = notificacao.FirstOrDefault(c => c.Mensagem == mensagem);

            Assert.Equal(mensagem, mensagemValidacao.Mensagem);
            Assert.NotEqual(caminhaoCadastrado.Id,id);

        }

        [Fact]
        public void DeveValidarAnoFabricacaoCaminhao()
        {
            var id = caminhao1;
            var mensagem = "O campo ano de modelo  deve ser o ano atual ou ano subsequente";
          

            var caminhaoCadastrado = _serviceMemory.ObterPorId(caminhao1).Result;


            var caminhao = new CaminhaoDto()
            {
                Id = caminhaoCadastrado.Id,
                AnoModelo = DateTime.UtcNow.AddYears(2).Year,
                AnoFabricacao = DateTime.UtcNow.Year,
                ModeloId = modelo1
            };

            var result = _serviceMemory.Atualizar(caminhao).Result;        
            var notificacao = _notificadorMemory.ObterNotificacoes();
            var mensagemValidacao = notificacao.FirstOrDefault(c => c.Mensagem == mensagem);

            Assert.Equal(mensagem, mensagemValidacao.Mensagem);
          

        }

    }
}
