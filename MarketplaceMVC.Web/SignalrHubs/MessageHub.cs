using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace MarketplaceMVC.Web.SignalrHubs
{
    public class MessageHub : Hub, IMessageHub
    {
        readonly IHubContext context;
        public MessageHub()
        {
            context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<MessageHub>();
        }
        public void UpdateMessage(int messagesCounter, string userId)
        {
            // отправляем сообщение
            context.Clients.User(userId).updateMessage(messagesCounter);
        }

        public void UpdateMessageInDialog(int messagesCounter, string lastMessage, string date, int dialogId, string userName, string companionId, string companionName)
        {
            context.Clients.User(userName).updateMessageInDialog(companionId, companionName, messagesCounter, lastMessage, date, dialogId);
        }

        public void AddMessage(string receiverName, string senderName, string messageBody, string date, string senderImage)
        {
            context.Clients.User(senderName).addMessage(receiverName, senderName, messageBody, date, senderImage);
            context.Clients.User(receiverName).addMessage(receiverName, senderName, messageBody, date, senderImage);

        }

        public void AddDialog(string userName, int dialogId)
        {
            context.Clients.User(userName).addDialog(dialogId);

            //_hubContext.Clients.User(fromUser.Name).addDialog(toUser.Id, toUser.Name, privateDialog.Id);// hub
            //_hubContext.Clients.User(toUser.Name).addDialog(fromUser.Id, fromUser.Name, privateDialog.Id);// hub
        }
    }
}