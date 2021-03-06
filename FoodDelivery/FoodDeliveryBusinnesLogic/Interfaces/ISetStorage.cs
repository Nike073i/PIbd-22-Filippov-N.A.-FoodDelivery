using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.ViewModels;
using System.Collections.Generic;

namespace FoodDeliveryBusinnesLogic.Interfaces
{
    public interface ISetStorage
    {
        List<SetViewModel> GetFullList();
        List<SetViewModel> GetFilteredList(SetBindingModel model);
        SetViewModel GetElement(SetBindingModel model);
        void Insert(SetBindingModel model);
        void Update(SetBindingModel model);
        void Delete(SetBindingModel model);
    }
}
