using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketplaceMVC.Web.SignalrHubs
{
    public interface IMessageHub
    {
        void UpdateMessage(int messagesCounter, string userId);
        void UpdateMessageInDialog(int messagesCounter, string lastMessage, string date, int dialogId, string userName, string companionId, string companionName);
        void AddMessage(string receiverName, string senderName, string messageBody, string date, string senderImage);
        void AddDialog(string userName, int dialogId);

    }
}