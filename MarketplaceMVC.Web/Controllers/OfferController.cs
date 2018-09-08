using AutoMapper;
using MarketplaceMVC.Model.Models;
using MarketplaceMVC.Service;
using MarketplaceMVC.Web.Models.Offer;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MarketplaceMVC.Web.Controllers
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


        public async Task<ActionResult> List()
        {
            var games = await gameService.GetAllGamesAsync();
            var offers = await offerService.GetAllOffersAsync();
            var model = new OfferListViewModel();
            
            foreach (var game in (await gameService.GetAllGamesAsync()).OrderBy(g => g.Rank).ToList())
            {
                model.Games.Add(
                    new SelectListItem
                    {
                        Value = game.Value,
                        Text = game.Name
                    }
                );
            }
            model.Offers = Mapper.Map<IEnumerable<Offer>, IEnumerable<OfferViewModel>>(offers);
            return View(model);
        }

        public async Task<ActionResult> OfferSearch(SearchOfferViewModel search)
        {
            Game game = gameService.GetGameByValue(search.Game);
            
            var offers = await offerService.GetOffersAsync(o => 
            (!search.PersonalAccount || (search.PersonalAccount && o.PersonalAccount)) &&
            (!search.IsBanned || (search.IsBanned && o.IsBanned)) &&
            search.Game == o.Game.Value, i => i.Game);
            if (search.PriceFrom == 0)
            {
                search.PriceFrom = offers.Min(o => o.Price);
            }
            if (search.PriceTo == 0)
            {
                search.PriceTo = offers.Max(o => o.Price);
            }
            offers = offers.Where(o => search.PriceFrom <= o.Price && search.PriceTo >= o.Price).ToList();

            var modelOffers = Mapper.Map<IEnumerable<Offer>, IEnumerable<OfferViewModel>>(offers);

            var model = new OfferListViewModel()
            {
                Offers = modelOffers
            };
            return PartialView("_OfferTable", model);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var model = new CreateOfferViewModel();
            foreach (var game in (await gameService.GetAllGamesAsync()).OrderBy(g => g.Rank))
            {
                model.Games.Add(
                    new SelectListItem
                    {
                        Value = game.Value,
                        Text = game.Name
                    }
                );
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateOfferViewModel model)
        {
            int currentUserId = User.Identity.GetUserId<int>();
            Game game = gameService.GetGameByValue(model.Game);
            UserProfile user = await userProfileService.GetUserProfileByIdAsync(currentUserId);
            if (ModelState.IsValid && game != null && user != null)
            {
                var offer = Mapper.Map<CreateOfferViewModel, Offer>(model);
                offer.UserProfile = user;
                offer.Game = game;
                offerService.CreateOffer(offer);
                offerService.SaveOffer();
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            if (id != null)
            {
                Offer offer = await offerService.GetOfferAsync(id.Value, i => i.UserProfile);
                if (offer != null)
                {
                    var model = Mapper.Map<Offer, DetailsOfferViewModel>(offer);
                    return View(model);
                }
            }
            return HttpNotFound();
        }

        

    }
}