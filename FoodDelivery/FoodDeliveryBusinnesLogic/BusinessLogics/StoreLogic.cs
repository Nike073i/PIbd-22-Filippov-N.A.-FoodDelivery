using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDeliveryBusinnesLogic.BusinessLogics
{
    public class StoreLogic
    {
        private readonly IStoreStorage _storeStorage;
        private readonly IDishStorage _dishStorage;
        public StoreLogic (IStoreStorage storeStorage,IDishStorage dishStorage)
        {
            _storeStorage = storeStorage;
            _dishStorage = dishStorage;
        }

        public void AddDishes(AddDishesToStoreBindingModel model)
        {
            var store = _storeStorage.GetElement(new StoreBindingModel
            {
                Id = model.StoreId
            });

            if (store == null)
            {
                throw new Exception("Склад не найден");
            }

            var dish = _dishStorage.GetElement(new DishBindingModel
            {
                Id = model.DishId
            });

            if (dish == null)
            {
                throw new Exception("Блюдо не найдено");
            }

            var storeDishes = store.StoreDishes;
            if (storeDishes.ContainsKey(dish.Id))
            {
                storeDishes[dish.Id] = (storeDishes[dish.Id].Item1, storeDishes[dish.Id].Item2 + model.Count);
            }
            else
            {
                storeDishes.Add(dish.Id, (dish.DishName, model.Count));
            }
        }
    }
}
