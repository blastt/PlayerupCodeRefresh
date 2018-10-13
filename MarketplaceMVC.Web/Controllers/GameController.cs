using MarketplaceMVC.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MarketplaceMVC.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService gameService;

        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }
        // GET: User/Game
        public PartialViewResult GameListPannel()
        {
            Dictionary<char, List<KeyValuePair<string, string>>> gameNames = new Dictionary<char, List<KeyValuePair<string, string>>>();
            IList<Char> letters = new List<Char>()
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H',
                'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
                'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
                'Y', 'Z'
            };

            var games = gameService.GetAllGames();
            var sortedGames = games.OrderBy(g => g.Name);


            foreach (var letter in letters)
            {
                var gamesInLetter = new List<KeyValuePair<string, string>>();
                foreach (var game in sortedGames.Where(g => g.Name.FirstOrDefault() == letter))
                {
                    gamesInLetter.Add(new KeyValuePair<string, string>(game.Value, game.Name));
                }
                gameNames.Add(letter, gamesInLetter);
            }

            return PartialView("_GameListPannel", gameNames);
        }
    }
}