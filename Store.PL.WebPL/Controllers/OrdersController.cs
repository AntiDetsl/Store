using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.BLL;
using Store.BLL.Interfaces;
using Store.Entities;
using Store.PL.WebPL.Models.Order;
using Store.PL.WebPL.Models.OrderItemVM;

namespace Store.PL.WebPL.Controllers
{
    public class OrdersController : Controller
    {
        public async Task<IActionResult> Index([FromServices] IOrderLogic orderLogic)
        {
            var orders = (await orderLogic.GetAllAsync()).ToList();
            var ordersVM = orders.Select(o => new DisplayOrderVM
            {
                ID = o.Id,
                Date = o.Date,
                Number = o.Number,
                Items = o.Items,
                Provider = o.Provider.Name
            });
            return View(ordersVM);
        }

        public async Task<IActionResult> Details([FromRoute] int id,
            [FromServices] IOrderLogic orderLogic)
        {
            var order = await orderLogic.GetByIdAsync(id);
            var orderVM = new DisplayOrderVM
            {
                ID= order.Id,
                Number = order.Number,
                Date = order.Date,
                Items = order.Items,
                Provider = order.Provider.Name
            };

            return View(orderVM);
        }

        public async Task<IActionResult> Create([FromServices] IProviderLogic providerLogic)
        {
            ViewBag.Providers = await providerLogic.GetAllAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CreateOrderVM orderVM,
            [FromServices] IOrderLogic orderLogic,
            [FromServices] IProviderLogic providerLogic)
        {
            if(orderVM.Provider == 0)
            {
                ModelState.AddModelError("Provider", "Provider should be selected");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Providers = await providerLogic.GetAllAsync();
                return View(orderVM);
            }

            await orderLogic.AddAsync(new Order 
            { 
                Number = orderVM.Number,
                Date = orderVM.Date,
                ProviderId = orderVM.Provider
            });

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(orderVM);
            }
        }

        public async Task<IActionResult> Edit(int id,
            [FromServices] IOrderLogic orderLogic,
            [FromServices] IProviderLogic providerLogic)
        {
            ViewBag.Providers = await providerLogic.GetAllAsync();

            var order = await orderLogic.GetByIdAsync(id);

            var orderVM = new CreateOrderVM
            {
                Number = order.Number,
                Date = order.Date,
                Provider = order.ProviderId
            };

            return View(orderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id,
            [FromForm] CreateOrderVM orderVM,
            [FromServices] IOrderLogic orderLogic,
            [FromServices] IProviderLogic providerLogic)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Providers = await providerLogic.GetAllAsync();
                return View(orderVM);
            }

            var order = await orderLogic.GetByIdAsync(id);

            order.Number = orderVM.Number;
            order.Date = orderVM.Date;
            
            if (orderVM.Provider != 0 && orderVM.Provider != order.ProviderId)
            {
                order.ProviderId = orderVM.Provider;
                order.Provider = await providerLogic.GetByIdAsync(orderVM.Provider);
            }

            await orderLogic.UpdateAsync(order);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(orderVM);
            }
        }

        public async Task<IActionResult> Delete(int id,
            [FromServices] IOrderLogic orderLogic)
        {
            await orderLogic.DeleteAsync(id);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
