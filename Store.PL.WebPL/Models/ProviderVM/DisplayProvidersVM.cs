using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Entities;

namespace Store.PL.WebPL.Models.ProviderVM
{
    public class DisplayProvidersVM
    {
        private readonly List<SelectListItem> _providers;

        public DisplayProvidersVM(IEnumerable<Provider> providers)
        {
            _providers = new List<SelectListItem>();

            foreach (var p in providers)
            {
                _providers.Add(new SelectListItem(p.Name, p.Id.ToString()));
            }
        }

        public List<SelectListItem> Provider => _providers;
    }
}
