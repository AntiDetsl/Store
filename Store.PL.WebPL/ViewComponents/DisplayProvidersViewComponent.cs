using Microsoft.AspNetCore.Mvc;
using Store.Entities;
using Store.PL.WebPL.Models.ProviderVM;

namespace Store.PL.WebPL.ViewComponents
{
    public class DisplayProvidersViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IEnumerable<Provider> providers)
        {
            return View(new DisplayProvidersVM(providers));
        }
    }
}
