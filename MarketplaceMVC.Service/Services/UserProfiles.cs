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
    public interface IUserProfileService
    {
        IEnumerable<UserProfile> GetAllUserProfiles();
        IEnumerable<UserProfile> GetAllUserProfiles(Expression<Func<UserProfile, bool>> where, params Expression<Func<UserProfile, object>>[] includes);
        Task<List<UserProfile>> GetAllUserProfilesAsync();
        Task<List<UserProfile>> GetAllUserProfilesAsync(Expression<Func<UserProfile, bool>> where, params Expression<Func<UserProfile, object>>[] includes);

        UserProfile GetUserProfile(Expression<Func<UserProfile, bool>> where, params Expression<Func<UserProfile, object>>[] includes);
        UserProfile GetUserProfileById(int id);
        UserProfile GetUserProfileByName(string name);

        Task<UserProfile> GetUserProfileAsync(Expression<Func<UserProfile, bool>> where, params Expression<Func<UserProfile, object>>[] includes);
        Task<UserProfile> GetUserProfileByIdAsync(int id);
        Task<UserProfile> GetUserProfileByNameAsync(string name);

        void CreateUserProfile(UserProfile userProfile);
        void UpdateUserProfile(UserProfile userProfile);
        void SaveUserProfile();
        Task SaveUserProfileAsync();
    }

    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository userProfilesRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserProfileService(IUserProfileRepository userProfilesRepository, IUnitOfWork unitOfWork)
        {
            this.userProfilesRepository = userProfilesRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IUserProfileService Members

        public IEnumerable<UserProfile> GetAllUserProfiles()
        {
            var userProfile = userProfilesRepository.GetAll();
            return userProfile;
        }

        public IEnumerable<UserProfile> GetAllUserProfiles(Expression<Func<UserProfile, bool>> where, params Expression<Func<UserProfile, object>>[] includes)
        {
            var userProfile = userProfilesRepository.GetMany(where, includes);
            return userProfile;
        }

        public async Task<List<UserProfile>> GetAllUserProfilesAsync()
        {
            return await userProfilesRepository.GetAllAsync();
        }

        public async Task<List<UserProfile>> GetAllUserProfilesAsync(Expression<Func<UserProfile, bool>> where, params Expression<Func<UserProfile, object>>[] includes)
        {
            return await userProfilesRepository.GetManyAsync(where, includes);
        }



        public UserProfile GetUserProfileById(int id)
        {
            var userProfile = userProfilesRepository.GetUserById(id);
            return userProfile;
        }

        public UserProfile GetUserProfile(Expression<Func<UserProfile, bool>> where, params Expression<Func<UserProfile, object>>[] includes)
        {
            var userProfile = userProfilesRepository.Get(where, includes);
            return userProfile;
        }

        public UserProfile GetUserProfileByName(string name)
        {
            var userProfile = userProfilesRepository.GetUserByName(name);
            return userProfile;
        }

        public async Task<UserProfile> GetUserProfileByIdAsync(int id)
        {
            return await userProfilesRepository.GetUserByIdAsync(id);
        }

        public async Task<UserProfile> GetUserProfileAsync(Expression<Func<UserProfile, bool>> where, params Expression<Func<UserProfile, object>>[] includes)
        {
            return await userProfilesRepository.GetAsync(where, includes);
        }

        public async Task<UserProfile> GetUserProfileByNameAsync(string name)
        {
            return await userProfilesRepository.GetUserByNameAsync(name);
        }

        public void UpdateUserProfile(UserProfile userProfile)
        {
            userProfilesRepository.Update(userProfile);
        }

        




        public void CreateUserProfile(UserProfile userProfile)
        {
            userProfilesRepository.Add(userProfile);
        }

        public void SaveUserProfile()
        {
            unitOfWork.SaveChangesAsync();
        }

        public async Task SaveUserProfileAsync()
        {
            await unitOfWork.SaveChangesAsync();
        }

        #endregion

    }
}
