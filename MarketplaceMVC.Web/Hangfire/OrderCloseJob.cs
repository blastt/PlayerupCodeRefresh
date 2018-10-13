using Hangfire;
using MarketplaceMVC.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketplaceMVC.Web.Hangfire
{
    public class OrderCloseJob
    {
        private readonly IOrderService orderService;

        public OrderCloseJob(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [DisableConcurrentExecution(10 * 60)]
        public void Do(int orderId)
        {
            orderService.CloseOrderAutomatically(orderId);
            orderService.SaveOrderAsync();

        }
    }
}