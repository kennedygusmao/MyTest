using AutoMapper;
using KissLog;
using Microsoft.EntityFrameworkCore;
using Moq;
using MT.Data.Context;
using MT.Data.Mapper;
using MT.Data.Repository;
using MT.Domain.Entities;
using MT.Domain.Interfaces.Repository;
using MT.Domain.Interfaces.Service;
using MT.Service.Interface;
using MT.Service.Notificacoes;
using MT.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MT.Test
{


    public class ModeloSeviceTest
    {
        private readonly BContext _contextMemory;
        private readonly IModeloService _serviceMemory;
        private readonly IMapper mapperMemory;
        private readonly IModeloRepository _repositoryMemory;
        private readonly INotificador _notificadorMemory;
        private readonly Mock<ILogger> logger;
        private readonly Guid modelo1 = Guid.NewGuid();
        private readonly Guid modelo2 = Guid.NewGuid();
        private readonly Guid modelo3 = Guid.NewGuid();

        public ModeloSeviceTest()
        {
            logger = new Mock<ILogger>();

            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile<AutomapperConfig>();
            });
            mapperMemory = mapperConfig.CreateMapper();

            _contextMemory = InMemoryContextFactory.Create();

            this._notificadorMemory = new Notificador();

            this._repositoryMemory = new ModeloRepository(_contextMemory);          
            this._serviceMemory = new ModeloService(_notificadorMemory, _repositoryMemory,  mapperMemory, logger.Object);

            ConfigInMemory();
        }

        private void ConfigInMemory()
        {

            Modelo modelo;

            modelo = new Modelo()
            {
                Id = modelo1,
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
            
            var entity3 = _contextMemory.Find<Modelo>(modelo1); //To Avoid tracking error
            _contextMemory.Entry(entity3).State = EntityState.Detached;
            var entity4 = _contextMemory.Find<Modelo>(modelo2); //To Avoid tracking error
            _contextMemory.Entry(entity4).State = EntityState.Detached;
            var entity5 = _contextMemory.Find<Modelo>(modelo3); //To Avoid tracking error
            _contextMemory.Entry(entity5).State = EntityState.Detached;


        }

        [Fact]
        public void DeveObterTodosModelos()
        {
            int countModelos = 3;            
            var resultModelos = _serviceMemory.ObterModelos().Result;
            Assert.Equal(countModelos, resultModelos.Count());           

        }

    }
}
