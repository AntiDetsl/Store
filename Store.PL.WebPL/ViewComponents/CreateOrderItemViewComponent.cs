using Microsoft.AspNetCore.Mvc;
using Store.PL.WebPL.Models.OrderItemVM;

namespace Store.PL.WebPL.ViewComponents
{
    public class CreateOrderItemViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(CreateOrderItemVM orderItemVM)
        {
            return View(orderItemVM);
        }
    }
}
