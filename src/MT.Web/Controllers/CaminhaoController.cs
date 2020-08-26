using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MT.Web.Models;
using MT.Web.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.Web.Controllers
{
    public class CaminhaoController : MainController
    {
        private readonly ICaminhaoService _caminhaoService;

        public CaminhaoController(ICaminhaoService caminhaoService)
        {
            _caminhaoService = caminhaoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var caminhoes = await _caminhaoService.ObterCaminhaoModelo();

            if (ResponsePossuiErros(caminhoes.Errors)) return View(caminhoes.Data);

            return View(caminhoes.Data);

        }

        [HttpGet]       
        public async Task<IActionResult> Cadastrar()
        {

            
            await GetModelos();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(Caminhao caminhao)
        {
            var caminhoes = await _caminhaoService.CadastrarCaminhao(caminhao);

            if (ResponsePossuiErros(caminhoes.Errors))
            {
                await GetModelos();
                return View(caminhao);
            }
          
            return RedirectToAction("Index", "Caminhao");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(Guid Id)
        {
            await GetModelos();

            var caminhoao = await _caminhaoService.ObterCaminhaoModeloPorId(Id);
            
            return View(caminhoao.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Caminhao caminhao)
        {
            var caminhoes = await _caminhaoService.EditarCaminhao(caminhao);

            if (ResponsePossuiErros(caminhoes.Errors))
            {
                await GetModelos();
                return View(caminhoes.Data);
            }         

            return RedirectToAction("Index", "Caminhao");
        }


        [HttpGet]
        public async Task<IActionResult> Deletar(Guid Id)
        {
            await GetModelos();

            var caminhoao = await _caminhaoService.ObterCaminhaoModeloPorId(Id);

            return View(caminhoao.Data);
        }

        [HttpPost]
        [Route("caminhao/deletar-caminhao")]      
        public async Task<IActionResult> DeletarCaminhao(Guid Id)
        {

            var caminhao =  await _caminhaoService.DeletarCaminhao(Id);

            if (ResponsePossuiErros(caminhao.Errors))
            {
                await GetModelos();
                return View("Deletar",caminhao.Data);
            }

            return RedirectToAction("Index", "Caminhao");
        }

        public async Task GetModelos() {
            var modelos = await _caminhaoService.ObterTodosModelos();
            ViewData["ModeloViewModel"] = modelos.Select(c => new SelectListItem()
            { Text = c.Descricao, Value = c.Id.ToString() }).ToList();
        }
    
    }
}