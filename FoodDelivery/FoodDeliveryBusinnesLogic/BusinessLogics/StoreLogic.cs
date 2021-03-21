using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.Interfaces;
using FoodDeliveryBusinnesLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace FoodDeliveryBusinnesLogic.BusinessLogics
{
    public class StoreLogic
    {
        private readonly IStoreStorage _storeStorage;
        private readonly IDishStorage _dishStorage;
        public StoreLogic(IStoreStorage storeStorage, IDishStorage dishStorage)
        {
            _storeStorage = storeStorage;
            _dishStorage = dishStorage;
        }

        public List<StoreViewModel> Read(StoreBindingModel model)
        {
            if (model == null)
            {
                return _storeStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<StoreViewModel> { _storeStorage.GetElement(model) };
            }
            return _storeStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(StoreBindingModel model)
        {
            var store = _storeStorage.GetElement(new StoreBindingModel
            {
                StoreName = model.StoreName
            });
            if (store != null && store.Id != model.Id)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            if (model.Id.HasValue)
            {
                model.CreationDate = store.CreationDate;
                _storeStorage.Update(model);
            }
            else
            {
                _storeStorage.Insert(model);
            }
        }

        public void Delete(StoreBindingModel model)
        {
            var store = _storeStorage.GetElement(new StoreBindingModel
            {
                Id = model.Id
            });
            if (store == null)
            {
                throw new Exception("Склад не найден");
            }
            _storeStorage.Delete(model);
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
            _storeStorage.Update(new StoreBindingModel
            {
                Id = store.Id,
                StoreName = store.StoreName,
                FullNameResponsible = store.FullNameResponsible,
                CreationDate = store.CreationDate,
                StoreDishes = storeDishes
            });
        }
    }
}
