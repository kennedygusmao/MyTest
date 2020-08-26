using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MT.Web.Models;

namespace MT.Web.Controllers
{
    public class MainController : Controller
    {
        protected bool ResponsePossuiErros(ResponseErrorMessagesCaminhaoViewModel resposta)
        {
            if (resposta != null && resposta.Mensagens.Any())
            {
                foreach (var mensagem in resposta.Mensagens)
                {
                    ModelState.AddModelError(string.Empty, mensagem);
                }

                return true;
            }

            return false;
        }
    }
}
