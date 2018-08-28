using MarketplaceMVC.Data.Infrastructure;
using MarketplaceMVC.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Data.Repositories
{
    public interface IUserLoginRepository : IRepository<UserLogin>
    {
        Task<List<UserLogin>> GetByUserId(int userId);
        Task<UserLogin> FindByLoginProviderAndProviderKey(string loginProvider, string providerKey);
    }

    internal class UserLoginRepository : RepositoryBase<UserLogin>, IUserLoginRepository
    {
        public UserLoginRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public Task<List<UserLogin>> GetByUserId(int userId)
        {
            return dbSet.Where(ul => ul.UserId == userId).ToListAsync();
        }

        public Task<UserLogin> FindByLoginProviderAndProviderKey(string loginProvider, string providerKey)
        {
            return dbSet.FirstOrDefaultAsync(ul => ul.LoginProvider == loginProvider && ul.ProviderKey == providerKey);
        }
    }
}
