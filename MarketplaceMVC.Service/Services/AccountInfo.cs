using MarketplaceMVC.Data.Infrastructure;
using MarketplaceMVC.Data.Repositories;
using MarketplaceMVC.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Service
{
    public interface IAccountInfoService
    {
        IEnumerable<AccountInfo> GetAllAccountInfos();
        Task<List<AccountInfo>> GetAllAccountInfosAsync();
        AccountInfo GetAccountInfo(int id);
        Task<AccountInfo> GetAccountInfoAsync(int id);
        void DeleteAccountInfo(AccountInfo feedback);
        void UpdateAccountInfo(AccountInfo feedback);
        void CreateAccountInfo(AccountInfo feedback);
        void SaveAccountInfo();
        Task SaveAccountInfoAsync();
    }

    public class AccountInfoService : IAccountInfoService
    {
        private readonly IAccountInfoRepository accountInfoRepository;
        private readonly IUnitOfWork unitOfWork;

        public AccountInfoService(IAccountInfoRepository accountInfoRepository, IUnitOfWork unitOfWork)
        {
            this.accountInfoRepository = accountInfoRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IMessageService Members

        public IEnumerable<AccountInfo> GetAllAccountInfos()
        {
            var feedback = accountInfoRepository.GetAll();
            return feedback;
        }

        public async Task<List<AccountInfo>> GetAllAccountInfosAsync()
        {
            return await accountInfoRepository.GetAllAsync();
        }


        public AccountInfo GetAccountInfo(int id)
        {
            var feedback = accountInfoRepository.GetById(id);
            return feedback;
        }

        public async Task<AccountInfo> GetAccountInfoAsync(int id)
        {
            return await accountInfoRepository.GetByIdAsync(id);
        }


        public void CreateAccountInfo(AccountInfo feedback)
        {
            accountInfoRepository.Add(feedback);
        }

        public void SaveAccountInfo()
        {
            unitOfWork.SaveChanges();
        }

        public async Task SaveAccountInfoAsync()
        {
            await unitOfWork.SaveChangesAsync();
        }

        public void DeleteAccountInfo(AccountInfo feedback)
        {
            accountInfoRepository.Remove(feedback);
        }

        public void UpdateAccountInfo(AccountInfo feedback)
        {
            accountInfoRepository.Update(feedback);
        }

        #endregion

    }
}
