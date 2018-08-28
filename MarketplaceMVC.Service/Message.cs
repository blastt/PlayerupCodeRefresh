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
    public interface IMessageService
    {
        IEnumerable<Message> GetAllMessages();
        Task<List<Message>> GetAllMessagesAsync();
        Message GetMessage(int id);
        Task<Message> GetMessageAsync(int id);
        void SetMessageViewed(int id);
        void CreateMessage(Message message);
        void DeleteMessage(int id);
        void SaveMessage();
        Task SaveMessageAsync();
    }

    public class MessageService : IMessageService
    {
        private readonly IMessageRepository messagesRepository;
        private readonly IUnitOfWork unitOfWork;

        public MessageService(IMessageRepository messagesRepository, IUnitOfWork unitOfWork)
        {
            this.messagesRepository = messagesRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IUserProfileService Members

        public IEnumerable<Message> GetAllMessages()
        {
            var message = messagesRepository.GetAll();
            return message;
        }

        public async Task<List<Message>> GetAllMessagesAsync()
        {
            return await messagesRepository.GetAllAsync();
        }


        public Message GetMessage(int id)
        {
            var message = messagesRepository.GetById(id);
            return message;
        }

        public async Task<Message> GetMessageAsync(int id)
        {
            return await messagesRepository.GetByIdAsync(id);
        }


        public void CreateMessage(Message message)
        {


            // if true


            messagesRepository.Add(message);


        }

        public void SaveMessage()
        {
            unitOfWork.SaveChanges();
        }

        public async Task SaveMessageAsync()
        {
            await unitOfWork.SaveChangesAsync();
        }

        public void SetMessageViewed(int id)
        {
            var message = messagesRepository.GetById(id);
            if (message != null)
            {
                message.ToViewed = true;
            }
        }

        public void DeleteMessage(int id)
        {
            var message = messagesRepository.GetById(id);
            if (message != null)
            {
                messagesRepository.Remove(message);
            }
        }

        #endregion

    }
}
