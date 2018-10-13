using Hangfire;
using MarketplaceMVC.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MarketplaceMVC.Web.Hangfire
{
    public class ConfirmOrderJob
    {
        private readonly IOrderService orderService;

        public ConfirmOrderJob(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [DisableConcurrentExecution(10 * 60)]
        public async Task Do(int orderId)
        {
            var order = orderService.GetOrder(orderId, i => i.BuyerId, i => i.SellerId);

            if (order != null)
            {
                var result = orderService.ConfirmOrder(orderId, order.BuyerId.Value);
                if (result)
                {
                    order.JobId = MarketplaceMVCHangfire.SetLeaveFeedbackJob(order.SellerId, order.BuyerId.Value, order.Id, TimeSpan.FromDays(15));
                }
                await orderService.SaveOrderAsync();
            }
        }
    }
}