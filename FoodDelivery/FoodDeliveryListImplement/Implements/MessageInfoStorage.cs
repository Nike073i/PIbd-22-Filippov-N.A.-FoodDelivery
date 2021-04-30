using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.Interfaces;
using FoodDeliveryBusinnesLogic.ViewModels;
using FoodDeliveryListImplement.Models;
using System;
using System.Collections.Generic;

namespace FoodDeliveryListImplement.Implements
{
    public class MessageInfoStorage : IMessageInfoStorage
    {
        private readonly DataListSingleton source;
        public MessageInfoStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<MessageInfoViewModel> GetFullList()
        {
            List<MessageInfoViewModel> result = new List<MessageInfoViewModel>();
            foreach (var messageInfo in source.MessageInfoes)
            {
                result.Add(CreateModel(messageInfo));
            }
            return result;
        }
        public List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<MessageInfoViewModel> result = new List<MessageInfoViewModel>();
            foreach (var messageInfo in source.MessageInfoes)
            {
                if ((model.ClientId.HasValue && messageInfo.ClientId == model.ClientId)
                    || (!model.ClientId.HasValue && messageInfo.DateDelivery.Date == model.DateDelivery.Date))
                {
                    result.Add(CreateModel(messageInfo));
                }
            }
            return result;
        }
        public void Insert(MessageInfoBindingModel model)
        {
            MessageInfo tempMessageInfo = new MessageInfo { MessageId = model.MessageId };
            foreach (var messageInfo in source.MessageInfoes)
            {
                if (tempMessageInfo.MessageId.Equals(messageInfo.MessageId))
                {
                    throw new Exception("Уже есть письмо с таким идентификатором");
                }
            }
            source.MessageInfoes.Add(CreateModel(model, tempMessageInfo));
        }
        private MessageInfo CreateModel(MessageInfoBindingModel model, MessageInfo messageInfo)
        {
            messageInfo.ClientId = model.ClientId;
            messageInfo.SenderName = model.FromMailAddress;
            messageInfo.DateDelivery = model.DateDelivery;
            messageInfo.Subject = model.Subject;
            messageInfo.Body = model.Body;
            return messageInfo;
        }

        private MessageInfoViewModel CreateModel(MessageInfo messageInfo)
        {
            return new MessageInfoViewModel
            {
                MessageId = messageInfo.MessageId,
                SenderName = messageInfo.SenderName,
                DateDelivery = messageInfo.DateDelivery,
                Subject = messageInfo.Subject,
                Body = messageInfo.Body
            };
        }
    }
}
