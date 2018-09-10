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
    public interface IBillingService
    {
        IEnumerable<Billing> GetAllBillings();
        //IEnumerable<Offer> GetCategoryGadgets(string categoryName, string gadgetName = null);
        Billing GetBilling(int id);
        Task<Billing> GetBillingAsync(int id);
        void UpdateBilling(Billing billing);
        void CreateBilling(Billing billing);
        void SaveBilling();
        Task SaveBillingAsync();
    }

    public class BillingService : IBillingService
    {
        private readonly IBillingRepository billingsRepository;
        private readonly IUnitOfWork unitOfWork;

        public BillingService(IBillingRepository billingsRepository, IUnitOfWork unitOfWork)
        {
            this.billingsRepository = billingsRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IBillingService Members

        public IEnumerable<Billing> GetAllBillings()
        {
            var billings = billingsRepository.GetAll();
            return billings;
        }

        public async Task<List<Billing>> GetAllBillingsAsync()
        {
            return await billingsRepository.GetAllAsync();
        }

        public void UpdateBilling(Billing billing)
        {
            billingsRepository.Update(billing);
        }
        public Billing GetBilling(int id)
        {
            var billing = billingsRepository.GetById(id);
            return billing;
        }

        public async Task<Billing> GetBillingAsync(int id)
        {
            return await billingsRepository.GetByIdAsync(id);
        }

        public void CreateBilling(Billing billing)
        {
            billingsRepository.Add(billing);
        }

        public void SaveBilling()
        {
            unitOfWork.SaveChanges();
        }

        public async Task SaveBillingAsync()
        {
            await unitOfWork.SaveChangesAsync();
        }

        #endregion

    }
}
