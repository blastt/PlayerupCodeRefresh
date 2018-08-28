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
    public interface IWithdrawService
    {
        IEnumerable<Withdraw> GetAllWithdraws();
        IEnumerable<Withdraw> GetWithdraws(Expression<Func<Withdraw, bool>> where, params Expression<Func<Withdraw, object>>[] includes);

        Task<List<Withdraw>> GetAllWithdrawsAsync();
        Task<List<Withdraw>> GetWithdrawsAsync(Expression<Func<Withdraw, bool>> where, params Expression<Func<Withdraw, object>>[] includes);

        Withdraw GetWithdraw(int id);
        Task<Withdraw> GetWithdrawAsync(int id);

        void Delete(Withdraw Withdraw);

        void CreateWithdraw(Withdraw message);
        void SaveWithdraw();
        Task SaveWithdrawAsync();
    }

    public class WithdrawService : IWithdrawService
    {
        private readonly IWithdrawRepository WithdrawsRepository;
        private readonly IUnitOfWork unitOfWork;

        public WithdrawService(IWithdrawRepository WithdrawsRepository, IUnitOfWork unitOfWork)
        {
            this.WithdrawsRepository = WithdrawsRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IWithdrawService Members

        public IEnumerable<Withdraw> GetAllWithdraws()
        {
            var withdraws = WithdrawsRepository.GetAll();
            return withdraws;
        }        

        public IEnumerable<Withdraw> GetWithdraws(Expression<Func<Withdraw, bool>> where, params Expression<Func<Withdraw, object>>[] includes)
        {
            var query = WithdrawsRepository.GetMany(where, includes);
            return query;
        }


        public async Task<List<Withdraw>> GetAllWithdrawsAsync()
        {
            return await WithdrawsRepository.GetAllAsync();
        }

        public async Task<List<Withdraw>> GetWithdrawsAsync(Expression<Func<Withdraw, bool>> where, params Expression<Func<Withdraw, object>>[] includes)
        {
            return await WithdrawsRepository.GetManyAsync(where, includes);

        }


        public Withdraw GetWithdraw(int id)
        {
            var Withdraw = WithdrawsRepository.GetById(id);
            return Withdraw;
        }

        public async Task<Withdraw> GetWithdrawAsync(int id)
        {
            return await WithdrawsRepository.GetByIdAsync(id);

        }

        public void CreateWithdraw(Withdraw withdraw)
        {
            WithdrawsRepository.Add(withdraw);
        }

        public void Delete(Withdraw withdraw)
        {
            WithdrawsRepository.Remove(withdraw);
        }

        public void SaveWithdraw()
        {
            unitOfWork.SaveChanges();
        }

        public async Task SaveWithdrawAsync()
        {
            await unitOfWork.SaveChangesAsync();
        }


        #endregion

    }
}
