using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.Interfaces;
using FoodDeliveryBusinnesLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace FoodDeliveryBusinnesLogic.BusinessLogics
{
    public class SetLogic
    {
        private readonly ISetStorage _setStorage;
        public SetLogic(ISetStorage setStorage)
        {
            _setStorage = setStorage;
        }
        public List<SetViewModel> Read(SetBindingModel model)
        {
            if (model == null)
            {
                return _setStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<SetViewModel> { _setStorage.GetElement(model) };
            }
            return _setStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(SetBindingModel model)
        {
            var set = _setStorage.GetElement(new SetBindingModel
            {
                SetName = model.SetName
            });
            if (set != null && set.Id != model.Id)
            {
                throw new Exception("Уже есть набор с таким названием");
            }
            if (model.Id.HasValue)
            {
                _setStorage.Update(model);
            }
            else
            {
                _setStorage.Insert(model);
            }
        }
        public void Delete(SetBindingModel model)
        {
            var set = _setStorage.GetElement(new SetBindingModel
            {
                Id = model.Id
            });
            if (set == null)
            {
                throw new Exception("Набор не найден");
            }
            _setStorage.Delete(model);
        }
    }
}
