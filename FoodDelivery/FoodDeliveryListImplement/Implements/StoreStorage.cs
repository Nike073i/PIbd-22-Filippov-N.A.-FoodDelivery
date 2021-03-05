using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.Interfaces;
using FoodDeliveryBusinnesLogic.ViewModels;
using FoodDeliveryListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDeliveryListImplement.Implements
{
    public class StoreStorage : IStoreStorage
    {
        private readonly DataListSingleton source;

        public StoreStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<StoreViewModel> GetFullList()
        {
            List<StoreViewModel> result = new List<StoreViewModel>();
            foreach (var store in source.Stores)
            {
                result.Add(CreateModel(store));
            }
            return result;
        }

        public List<StoreViewModel> GetFilteredList(StoreBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<StoreViewModel> result = new List<StoreViewModel>();
            foreach (var store in source.Stores)
            {
                if (store.StoreName.Contains(model.StoreName))
                {
                    result.Add(CreateModel(store));
                }
            }
            return result;
        }

        public StoreViewModel GetElement(StoreBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var store in source.Stores)
            {
                if (store.Id == model.Id || store.StoreName == model.StoreName)
                {
                    return CreateModel(store);
                }
            }
            return null;
        }

        public void Insert(StoreBindingModel model)
        {
            Store tempStore = new Store
            {
                Id = 1,
                StoreDishes = new Dictionary<int, int>(),
                CreationDate = DateTime.Now
            };
            foreach (var store in source.Stores)
            {
                if (store.Id >= tempStore.Id)
                {
                    tempStore.Id = store.Id + 1;
                }
            }
            source.Stores.Add(CreateModel(model, tempStore));
        }

        public void Update(StoreBindingModel model)
        {
            Store tempStore = null;
            foreach (var store in source.Stores)
            {
                if (store.Id == model.Id)
                {
                    tempStore = store;
                }
            }
            if (tempStore == null)
            {
                throw new Exception("Склад не найден");
            }
            CreateModel(model, tempStore);
        }

        public void Delete(StoreBindingModel model)
        {
            for (int i = 0; i < source.Stores.Count; i++)
            {
                if (source.Stores[i].Id == model.Id)
                {
                    source.Stores.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Склад не найден");
        }

        private Store CreateModel(StoreBindingModel model, Store store)
        {
            store.StoreName = model.StoreName;
            store.FullNameResponsible = model.FullNameResponsible;

            foreach (var key in store.StoreDishes.Keys.ToList())
            {
                if (!model.StoreDishes.ContainsKey(key))
                {
                    store.StoreDishes.Remove(key);
                }
            }

            foreach (var dish in model.StoreDishes)
            {
                if (store.StoreDishes.ContainsKey(dish.Key))
                {
                    store.StoreDishes[dish.Key] = model.StoreDishes[dish.Key].Item2;
                }
                else
                {
                    store.StoreDishes.Add(dish.Key, model.StoreDishes[dish.Key].Item2);
                }
            }
            return store;
        }

        private StoreViewModel CreateModel(Store store)
        {
            Dictionary<int, (string, int)> storeDishes = new Dictionary<int, (string, int)>();
            foreach (var sd in store.StoreDishes)
            {
                string dishName = string.Empty;
                foreach (var dish in source.Dishes)
                {
                    if (sd.Key == dish.Id)
                    {
                        dishName = dish.DishName;
                        break;
                    }
                }
                storeDishes.Add(sd.Key, (dishName, sd.Value));
            }

            return new StoreViewModel
            {
                Id = store.Id,
                StoreName = store.StoreName,
                FullNameResponsible = store.FullNameResponsible,
                CreationDate = store.CreationDate,
                StoreDishes = storeDishes
            };
        }

        public bool CheckAvailabilityAndWriteOff(int SetId, int SetCount)
        {
            return true;
        }
    }
}
