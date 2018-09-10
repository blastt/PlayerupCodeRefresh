using AutoMapper;
using MarketplaceMVC.Model.Models;
using MarketplaceMVC.Service;
using MarketplaceMVC.Web.Areas.User.Models.Order;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MarketplaceMVC.Web.Areas.User.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        
        public async Task<ActionResult> SellList()
        {
            var userId = User.Identity.GetUserId<int>();
            var orders = await orderService.GetOrdersAsync(o => o.SellerId == userId, i => i.Seller);
            var modelOrders = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(orders);
            var model = new OrderListViewModel()
            {
                Orders = modelOrders
            };
            return View(model);
        }

        public async Task<ActionResult> BuyList()
        {
            var userId = User.Identity.GetUserId<int>();
            var orders = await orderService.GetOrdersAsync(o => o.SellerId == userId, i => i.Seller);
            var modelOrders = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(orders);
            var model = new OrderListViewModel()
            {
                Orders = modelOrders
            };
            return View(model);
        }
    }
}