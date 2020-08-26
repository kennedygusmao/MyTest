using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace MT.Web.ViewComponents
{
    public class SummaryViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
