﻿using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.ViewModels;
using System.Collections.Generic;

namespace FoodDeliveryBusinnesLogic.Interfaces
{
    public interface IDishStorage
    {
        List<DishViewModel> GetFullList();
        List<DishViewModel> GetFilteredList(DishBindingModel model);
        DishViewModel GetElement(DishBindingModel model);
        void Insert(DishBindingModel model);
        void Update(DishBindingModel model);
        void Delete(DishBindingModel model);
    }
}
