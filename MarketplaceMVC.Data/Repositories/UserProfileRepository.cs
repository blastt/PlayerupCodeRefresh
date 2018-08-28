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
    public class UserProfileRepository : RepositoryBase<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(IDbFactory dbFactory)
            : base(dbFactory) { }



        public UserProfile GetUserById(int userId)
        {
            return DbContext.UserProfiles.Find(userId);
        }

        public async Task<UserProfile> GetUserByIdAsync(int userId)
        {
            return await DbContext.UserProfiles.FindAsync(userId);
        }

        public UserProfile GetUserByName(string userName)
        {
            return DbContext.UserProfiles.FirstOrDefault(u => u.Name == userName);
        }

        public async Task<UserProfile> GetUserByNameAsync(string userName)
        {
            return await DbContext.UserProfiles.FirstOrDefaultAsync(u => u.Name == userName);
        }


    }

    public interface IUserProfileRepository : IRepository<UserProfile>
    {
        UserProfile GetUserByName(string userName);
        UserProfile GetUserById(int userId);
        Task<UserProfile> GetUserByIdAsync(int userId);
        Task<UserProfile> GetUserByNameAsync(string userName);


    }
}
