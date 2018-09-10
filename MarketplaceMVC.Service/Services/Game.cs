using MarketplaceMVC.Data.Infrastructure;
using MarketplaceMVC.Data.Repositories;
using MarketplaceMVC.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Service
{
    public interface IGameService
    {
        void Delete(Game game);

        IEnumerable<Game> GetAllGames();
        Task<List<Game>> GetAllGamesAsync();

        IEnumerable<Game> GetGames(Expression<Func<Game, bool>> where, params Expression<Func<Game, object>>[] includes);
        Task<List<Game>> GetGamesAsync(Expression<Func<Game, bool>> where, params Expression<Func<Game, object>>[] includes);
        
        Game GetGame(int id);
        Game GetGameByValue(string name);
        
        Game GetGameByValue(string name, params Expression<Func<Game, object>>[] includes);        
        void CreateGame(Game message);
        void SaveGame();
        Task SaveGameAsync();
    }

    public class GameService : IGameService
    {
        private readonly IGameRepository gamesRepository;
        private readonly IUnitOfWork unitOfWork;

        public GameService(IGameRepository gamesRepository, IUnitOfWork unitOfWork)
        {
            this.gamesRepository = gamesRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IGameService Members

        public IEnumerable<Game> GetAllGames()
        {
            var games = gamesRepository.GetAll();
            return games;
        }

        public async Task<List<Game>> GetAllGamesAsync()
        {
            return await gamesRepository.GetAllAsync();
        }


        public IEnumerable<Game> GetGames(Expression<Func<Game, bool>> where, params Expression<Func<Game, object>>[] includes)
        {
            var query = gamesRepository.GetMany(where, includes);
            return query;
        }

        public async Task<List<Game>> GetGamesAsync(Expression<Func<Game, bool>> where, params Expression<Func<Game, object>>[] includes)
        {
            return await gamesRepository.GetManyAsync(where, includes);
        }

        public Game GetGame(int id)
        {
            var game = gamesRepository.GetById(id);
            return game;
        }


        public void CreateGame(Game game)
        {
            gamesRepository.Add(game);
        }

        public void Delete(Game game)
        {
            gamesRepository.Remove(game);
        }

        public void SaveGame()
        {
            unitOfWork.SaveChanges();
        }

        public async Task SaveGameAsync()
        {
            await unitOfWork.SaveChangesAsync();
        }

        public Game GetGameByValue(string name)
        {
            return gamesRepository.GetGameByValue(name);
        }

        public Game GetGameByValue(string name, params Expression<Func<Game, object>>[] includes)
        {
            var query = gamesRepository.GetGameByValue(name, includes);
            return query;
        }


        #endregion

    }
}
