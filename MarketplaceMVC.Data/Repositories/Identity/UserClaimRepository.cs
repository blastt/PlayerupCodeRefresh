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
    public interface IUserClaimRepository : IRepository<UserClaim>
    {
        Task<List<UserClaim>> GetByUserId(int userId);
    }

    internal class UserClaimRepository : RepositoryBase<UserClaim>, IUserClaimRepository
    {
        public UserClaimRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public Task<List<UserClaim>> GetByUserId(int userId)
        {
            return dbSet.Where(uc => uc.UserId == userId).ToListAsync();
        }
    }
}
