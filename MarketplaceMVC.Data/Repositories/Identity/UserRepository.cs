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
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByNameAsync(string name);
        Task<User> FindByEmailAsync(string email);
    }

    internal class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public async Task<User> FindByNameAsync(string name)
        {
            return await dbSet.FirstOrDefaultAsync(u => u.UserName.ToUpper() == name.ToUpper());
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await dbSet.FirstOrDefaultAsync(u => u.Email.ToUpper() == email.ToUpper());
        }
    }
}
