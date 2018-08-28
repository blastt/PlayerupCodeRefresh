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
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        Task<bool> IsInRoleAsync(int userId, int roleId);
    }

    internal class UserRoleRepository : RepositoryBase<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public Task<bool> IsInRoleAsync(int userId, int roleId)
        {
            return dbSet.AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        }
    }
}
