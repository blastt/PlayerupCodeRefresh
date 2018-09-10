using AutoMapper;
using MarketplaceMVC.Model.Models;
using MarketplaceMVC.Service;
using MarketplaceMVC.Web.Areas.User.Models.Feedback;
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
    public class FeedbackController : Controller
    {
        private readonly IUserProfileService userProfileService;
        private readonly IFeedbackService feedbackService;
        public FeedbackController(IUserProfileService userProfileService, IFeedbackService feedbackService)
        {
            this.userProfileService = userProfileService;
            this.feedbackService = feedbackService;
        }

        public async Task<ActionResult> All()
        {
            var userId = User.Identity.GetUserId<int>();
            var feedbacks = await feedbackService.GetFeedbacksAsync(f => f.UserToId == userId, i => i.UserTo);
            var modelFeedbacks = Mapper.Map<IEnumerable<Feedback>, IEnumerable<FeedbackViewModel>>(feedbacks);
            var model = new FeedbackListViewModel()
            {
                Feedbacks = modelFeedbacks
            };
            return View(model);
        }

        public async Task<ActionResult> Positive()
        {
            var userId = User.Identity.GetUserId<int>();
            var feedbacks = await feedbackService.GetFeedbacksAsync(f => f.UserToId == userId && f.Grade == Emotions.Good, i => i.UserTo);
            var modelFeedbacks = Mapper.Map<IEnumerable<Feedback>, IEnumerable<FeedbackViewModel>>(feedbacks);
            var model = new FeedbackListViewModel()
            {
                Feedbacks = modelFeedbacks
            };
            return View(model);
        }

        public async Task<ActionResult> Negative()
        {
            var userId = User.Identity.GetUserId<int>();
            var feedbacks = await feedbackService.GetFeedbacksAsync(f => f.UserToId == userId && f.Grade == Emotions.Bad, i => i.UserTo);
            var modelFeedbacks = Mapper.Map<IEnumerable<Feedback>, IEnumerable<FeedbackViewModel>>(feedbacks);
            var model = new FeedbackListViewModel()
            {
                Feedbacks = modelFeedbacks
            };
            return View(model);
        }
    }
}