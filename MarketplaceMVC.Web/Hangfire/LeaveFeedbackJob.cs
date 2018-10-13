using Hangfire;
using MarketplaceMVC.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketplaceMVC.Web.Hangfire
{
    public class LeaveFeedbackJob
    {
        private readonly IFeedbackService feedbackService;

        public LeaveFeedbackJob(IFeedbackService feedbackService)
        {
            this.feedbackService = feedbackService;
        }

        [DisableConcurrentExecution(10 * 60)]
        public void Do(int sellerId, int buyerId, int orderId)
        {
            feedbackService.LeaveAutomaticFeedback(sellerId, buyerId, orderId);
            feedbackService.SaveFeedback();
        }
    }
}