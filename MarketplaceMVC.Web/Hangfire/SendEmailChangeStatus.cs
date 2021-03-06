﻿using Hangfire;
using MarketplaceMVC.Service;
using MarketplaceMVC.Web.HtmlHelpers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketplaceMVC.Web.Hangfire
{
    public class SendEmailChangeStatus
    {
        private readonly IOrderService orderService;
        private readonly IIdentityMessageService identityMessageService;
        public SendEmailChangeStatus(IOrderService orderService, IIdentityMessageService identityMessageService)
        {
            this.orderService = orderService;
            this.identityMessageService = identityMessageService;
        }

        [DisableConcurrentExecution(10 * 60)]
        public void Do(int orderId, string userEmail, string currentStatus, string callbackUrl)
        {
            //Url.Action("BuyDetails", "Order", new { id = offer.Order.Id }, protocol: Request.Url.Scheme)).ToString()
            string body = EmailHelpers.ActivateForm($"Статус вашего заказа (id:{orderId}) изменился на: {currentStatus}", "Посмотреть детали", callbackUrl).ToString();

            identityMessageService.SendAsync(new IdentityMessage()
            {
                Body = body,
                Subject = "Статус заказа изменился",

                Destination = userEmail
            }).Wait();
        }
    }
}