using AutoMapper;
using MarketplaceMVC.Model.Models;
using MarketplaceMVC.Service;
using MarketplaceMVC.Web.Areas.User.Models.Offer;
using MarketplaceMVC.Web.Models.Offer;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MarketplaceMVC.Web.Areas.User.Controllers
{
    public class OfferController : Controller
    {

        private readonly IOfferService offerService;
        private readonly IGameService gameService;
        private readonly IUserProfileService userProfileService;
        // GET: Offer
        public OfferController(IOfferService offerService, IUserProfileService userProfileService, IGameService gameService)
        {
            this.offerService = offerService;
            this.userProfileService = userProfileService;
            this.gameService = gameService;
        }

        

        public async Task<ActionResult> Active()
        {
            int currentUserId = User.Identity.GetUserId<int>();
            var model = new Models.Offer.OfferListViewModel();
            var offers = await offerService.GetOffersAsync(o => o.State == OfferState.active);
            model.CountOfActive = offers.Count;
            model.CountOfClosed = (await offerService.GetOffersAsync(o => o.State == OfferState.closed)).Count;
            model.CountOfInactive = (await offerService.GetOffersAsync(o => o.State == OfferState.inactive)).Count;
            model.Offers = Mapper.Map<IEnumerable<Offer>, IEnumerable<OfferViewModel>>(offers);
            return View(model);
        }

        public async Task<ActionResult> Inactive()
        {
            int currentUserId = User.Identity.GetUserId<int>();
            var model = new Models.Offer.OfferListViewModel();
            var offers = await offerService.GetOffersAsync(o => o.State == OfferState.inactive);
            model.CountOfInactive = offers.Count;
            model.CountOfClosed = (await offerService.GetOffersAsync(o => o.State == OfferState.closed)).Count;
            model.CountOfActive = (await offerService.GetOffersAsync(o => o.State == OfferState.active)).Count;
            model.Offers = Mapper.Map<IEnumerable<Offer>, IEnumerable<OfferViewModel>>(offers);
            return View(model);
        }

        public async Task<ActionResult> Closed()
        {
            int currentUserId = User.Identity.GetUserId<int>();
            var model = new Models.Offer.OfferListViewModel();
            var offers = await offerService.GetOffersAsync(o => o.State == OfferState.closed);
            model.CountOfClosed = offers.Count;
            model.CountOfInactive = (await offerService.GetOffersAsync(o => o.State == OfferState.inactive)).Count;
            model.CountOfActive = (await offerService.GetOffersAsync(o => o.State == OfferState.active)).Count;
            model.Offers = Mapper.Map<IEnumerable<Offer>, IEnumerable<OfferViewModel>>(offers);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var userId = User.Identity.GetUserId<int>();
                Offer offer = await offerService.GetOfferAsync(id.Value, i => i.UserProfile);
                UserProfile user = offer.UserProfile;
                if (offer != null && user != null && user.Id == userId)
                {
                    offerService.Delete(offer);
                    await offerService.SaveOfferAsync();
                    return View();
                }
            }
            return HttpNotFound();
        }
    }
}