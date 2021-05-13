using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.ViewModels;
using System.Collections.Generic;

namespace FoodDeliveryBusinnesLogic.Interfaces
{
    public interface IMessageInfoStorage
    {
        List<MessageInfoViewModel> GetFullList();
        List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model);
        void Insert(MessageInfoBindingModel model);
    }
}
