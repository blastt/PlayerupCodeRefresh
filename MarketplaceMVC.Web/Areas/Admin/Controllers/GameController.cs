using AutoMapper;
using MarketplaceMVC.Model.Models;
using MarketplaceMVC.Service;
using MarketplaceMVC.Web.Areas.Admin.Models.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MarketplaceMVC.Web.Areas.Admin.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService gameService;
        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateGameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var game = Mapper.Map<CreateGameViewModel, Game>(model);
            gameService.CreateGame(game);
            await gameService.SaveGameAsync();
            return View();
        }
    }
}