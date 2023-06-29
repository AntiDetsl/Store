using Microsoft.AspNetCore.Mvc;
using Store.BLL;
using Store.BLL.Interfaces;
using Store.Entities;
using Store.PL.WebPL.Models.OrderItemVM;

namespace Store.PL.WebPL.Controllers
{
    public class OrderItemsController : Controller
    {
        [HttpPost("{controller}/{action}/{orderId}")]
        public async Task<IActionResult> Create([FromRoute] int orderId,
            [FromForm] CreateOrderItemVM orderItemVM,
            [FromServices] IOrderItemLogic itemLogic)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Orders", new { id = orderId });
            }

            var item = new OrderItem
            {
                OrderId = orderId,
                Name = orderItemVM.Name,
                Quantity = orderItemVM.Quantity,
                Unit = orderItemVM.Unit
            };

            await itemLogic.AddAsync(item);
            return RedirectToAction("Details", "Orders", new { id = orderId });
        }

        [HttpGet("{controller}/{action}/{id}/{orderId}")]
        public async Task<IActionResult> Delete([FromRoute] int id,
            [FromRoute] int orderId,
            [FromServices] IOrderItemLogic itemLogic)
        {
            await itemLogic.DeleteAsync(id);

            return RedirectToAction("Details", "Orders", new { id = orderId });
        }

        [HttpGet("{controller}/{action}/{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id,
            [FromServices] IOrderItemLogic itemLogic)
        {
            var item = await itemLogic.GetByIdAsync(id);

            var createItemVM = new CreateOrderItemVM
            {
                Name = item.Name,
                Quantity = item.Quantity,
                Unit = item.Unit
            };

            ViewBag.Id = id;
            return View(createItemVM);
        }

        [HttpPost("{controller}/{action}/{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id,
            [FromForm] CreateOrderItemVM itemVM,
            [FromServices] IOrderItemLogic itemLogic)
        {
            if (!ModelState.IsValid)
            {
                return View(itemVM);
            }

            var item = await itemLogic.GetByIdAsync(id);

            item.Name = itemVM.Name;
            item.Quantity = itemVM.Quantity;
            item.Unit = itemVM.Unit;

            await itemLogic.UpdateAsync(item);

            return RedirectToAction("Details", "Orders", new { id = item.OrderId });
        }
    }
}
