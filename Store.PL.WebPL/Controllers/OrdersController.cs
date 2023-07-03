using Microsoft.AspNetCore.Mvc;
using Store.BLL.Interfaces;
using Store.Entities;
using Store.Entities.Filters;
using Store.PL.WebPL.Models;
using Store.PL.WebPL.Models.Order;
using Store.PL.WebPL.Models.OrderItemVM;

namespace Store.PL.WebPL.Controllers
{
    public class OrdersController : Controller
    {
        private readonly int _pageSize = 2;

        public async Task<IActionResult> Index([FromServices] IOrderLogic orderLogic,
            [FromServices] IOrderItemLogic itemLogic,
            [FromServices] IProviderLogic providerLogic,
            [FromForm] FiltersVM Filter,
            [FromRoute] int page = 1)
        {
            Filter.StartDate ??= DateTime.Now.AddMonths(-1).Date;
            Filter.EndDate ??= DateTime.Now.Date;

            var orderFilters = new OrderFilters
            {
                Numbers = Filter.Numbers,
                Providers = Filter.Providers,
                StartDate = Filter.StartDate.Value,
                EndDate = Filter.EndDate.Value
            };

            if((Filter.ItemNames != null && Filter.ItemNames.Any()) 
                || (Filter.ItemUnits != null && Filter.ItemUnits.Any()))
            {
                orderFilters.ItemFilters = new ItemFilters
                {
                    Names = Filter.ItemNames,
                    Units = Filter.ItemUnits
                };
            }
            else
            {
                orderFilters.ItemFilters = null;
            }

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

            var itemsInfo = new ItemInfoVM
            {
                Names = await itemLogic.GetAllNamesDistinct(),
                Units = await itemLogic.GetAllUnitsDistinct()
            };

            var pageInfo = new PagingInfo
            {
                CurrentPage = page,
                PageSize = _pageSize,
                TotalItems = await orderLogic.CountTotalItemsAsync(orderFilters)
            };

            var indexVM = new IndexViewModel
            {
                Data = new DataVM
                {
                    Orders = orders,
                    Numbers = orderNumbers,
                    Providers = orderProviders,
                    ItemInfo = itemsInfo,
                },
                PageInfo = pageInfo,
                Filter = Filter
            };

            return View(indexVM);
        }

        [HttpPost("{controller}/{action}/{page:int}")]
        public async Task<IActionResult> GetPage([FromServices] IOrderLogic orderLogic,
            [FromServices] IOrderItemLogic itemLogic,
            [FromServices] IProviderLogic providerLogic,
            [FromRoute] int page,
            [FromBody] FiltersVM Filter)
        {
            var orderFilters = new OrderFilters
            {
                Numbers = Filter.Numbers,
                Providers = Filter.Providers,
                StartDate = Filter.StartDate.Value,
                EndDate = Filter.EndDate.Value
            };

            if ((Filter.ItemNames != null && Filter.ItemNames.Any())
                || (Filter.ItemUnits != null && Filter.ItemUnits.Any()))
            {
                orderFilters.ItemFilters = new ItemFilters
                {
                    Names = Filter.ItemNames,
                    Units = Filter.ItemUnits
                };
            }
            else
            {
                orderFilters.ItemFilters = null;
            }

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

            var itemsInfo = new ItemInfoVM
            {
                Names = await itemLogic.GetAllNamesDistinct(),
                Units = await itemLogic.GetAllUnitsDistinct()
            };

            var pageInfo = new PagingInfo
            {
                CurrentPage = page,
                PageSize = _pageSize,
                TotalItems = await orderLogic.CountTotalItemsAsync(orderFilters)
            };

            var indexVM = new IndexViewModel
            {
                Data = new DataVM
                {
                    Orders = orders,
                    Numbers = orderNumbers,
                    Providers = orderProviders,
                    ItemInfo = itemsInfo,
                },
                PageInfo = pageInfo,
                Filter = Filter
            };

            return View("Index", indexVM);
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

            if(id == -1)
            {
                ModelState.AddModelError("Number", "There should not be two orders " +
                    "from the same provider with the same number");

                ViewBag.Providers = await providerLogic.GetAllAsync();
                return View(orderVM);
            }

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

            if (!await orderLogic.TryUpdateAsync(order))
            {
                ModelState.AddModelError("Number", "There should not be two orders " +
                    "from the same provider with the same number");

                ViewBag.Providers = await providerLogic.GetAllAsync();
                return View(orderVM);
            }

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
