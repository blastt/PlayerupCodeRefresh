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
    public interface IStatusLogService
    {
        IEnumerable<StatusLog> GetAllStatusLogs();
        Task<List<StatusLog>> GetAllStatusLogsAsync();
        StatusLog GetStatusLog(int id);
        Task<StatusLog> GetStatusLogAsync(int id);
        void CreateStatusLog(StatusLog message);
        void SaveStatusLog();
        Task SaveStatusLogAsync();
    }

    public class StatusLogService : IStatusLogService
    {
        private readonly IStatusLogRepository statusLogsRepository;
        private readonly IUnitOfWork unitOfWork;

        public StatusLogService(IStatusLogRepository statusLogsRepository, IUnitOfWork unitOfWork)
        {
            this.statusLogsRepository = statusLogsRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IStatusLogService Members

        public IEnumerable<StatusLog> GetAllStatusLogs()
        {
            var statusLogs = statusLogsRepository.GetAll();
            return statusLogs;
        }

        public async Task<List<StatusLog>> GetAllStatusLogsAsync()
        {
            return await statusLogsRepository.GetAllAsync();
        }


        public StatusLog GetStatusLog(int id)
        {
            var statusLog = statusLogsRepository.GetById(id);
            return statusLog;
        }

        public async Task<StatusLog> GetStatusLogAsync(int id)
        {
            return await statusLogsRepository.GetByIdAsync(id);
        }


        public void CreateStatusLog(StatusLog statusLog)
        {
            statusLogsRepository.Add(statusLog);
        }

        public void SaveStatusLog()
        {
            unitOfWork.SaveChanges();
        }

        public async Task SaveStatusLogAsync()
        {
            await unitOfWork.SaveChangesAsync();
        }

        #endregion

    }
}
