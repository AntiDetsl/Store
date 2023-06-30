using Microsoft.AspNetCore.Mvc;
using Store.BLL.Interfaces;
using Store.Entities;
using Store.Entities.Filters;
using Store.PL.WebPL.Models;
using Store.PL.WebPL.Models.Order;

namespace Store.PL.WebPL.Controllers
{
    public class OrdersController : Controller
    {
        private readonly int _pageSize = 2;

        public async Task<IActionResult> Index([FromServices] IOrderLogic orderLogic,
            [FromServices] IProviderLogic providerLogic,
            [FromForm] OrderFiltersVM Filter,
            [FromRoute] int page = 1)
        {
            Filter.StartDate ??= DateTime.Now.AddMonths(-10000).Date;
            Filter.EndDate ??= DateTime.Now.Date;

            var orderFilters = new OrderFilters
            {
                Numbers = Filter.Numbers,
                Providers = Filter.Providers,
                StartDate = Filter.StartDate.Value,
                EndDate = Filter.EndDate.Value
            };

            var orders = (await orderLogic.PageAsync(page, _pageSize, orderFilters))
                .Select(o => new DisplayOrderVM
                {
                    ID = o.Id,
                    Date = o.Date,
                    Number = o.Number,
                    Items = o.Items,
                    Provider = o.Provider.Name
                });

            var orderNumbers = await orderLogic.GetNumbersDistinct();

            var orderProviders = (await providerLogic.GetAllAsync())
                .Select(p => p.Name);

            var pageInfo = new PagingInfo
            {
                CurrentPage = page,
                PageSize = _pageSize,
                TotalItems = await orderLogic.CountTotalItemsAsync(orderFilters)
            };

            var indexVM = new OrderIndexViewModel
            {
                Data = new OrderDataVM
                {
                    Orders = orders,
                    Numbers = orderNumbers,
                    Providers = orderProviders
                },
                PageInfo = pageInfo,
                Filter = Filter
            };

            return View(indexVM);
        }

        [HttpPost("{controller}/{action}/{page:int}")]
        public async Task<IActionResult> Page([FromRoute] int page,
            [FromBody] OrderFiltersVM Filter,
            [FromServices] IOrderLogic orderLogic,
            [FromServices] IProviderLogic providerLogic)
        {
            return Json(Filter);
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

            var id = await orderLogic.AddAsync(new Order 
            { 
                Number = orderVM.Number,
                Date = orderVM.Date,
                ProviderId = orderVM.Provider
            });

            try
            {
                return RedirectToAction("Details", "Orders", new { id });
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
