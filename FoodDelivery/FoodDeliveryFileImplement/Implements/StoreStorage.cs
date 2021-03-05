using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.Interfaces;
using FoodDeliveryBusinnesLogic.ViewModels;
using FoodDeliveryFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDeliveryFileImplement.Implements
{
    public class StoreStorage : IStoreStorage
    {
        private readonly FileDataListSingleton source;

        public StoreStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<StoreViewModel> GetFullList() => source.Stores.Select(CreateModel).ToList();

        public List<StoreViewModel> GetFilteredList(StoreBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Stores.Where(rec => rec.StoreName.Contains(model.StoreName)).Select(CreateModel).ToList();
        }

        public StoreViewModel GetElement(StoreBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var store = source.Stores.FirstOrDefault(rec => rec.Id == model.Id || rec.StoreName == model.StoreName);
            return store != null ? CreateModel(store) : null;
        }

        public void Insert(StoreBindingModel model)
        {
            int maxId = source.Stores.Count > 0 ? source.Sets.Max(rec => rec.Id) : 0;
            Store store = new Store
            {
                Id = maxId + 1,
                StoreDishes = new Dictionary<int, int>(),
                CreationDate = DateTime.Now
            };
            source.Stores.Add(CreateModel(model, store));
        }

        public void Update(StoreBindingModel model)
        {
            var store = source.Stores.FirstOrDefault(rec => rec.Id == model.Id);
            if (store == null)
            {
                throw new Exception("Склад не найден");
            }
            CreateModel(model, store);
        }

        public void Delete(StoreBindingModel model)
        {
            var store = source.Stores.FirstOrDefault(rec => rec.Id == model.Id);
            if (store != null)
            {
                source.Stores.Remove(store);
            }
            else
            {
                throw new Exception("Склад не найден");
            }
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
            return new StoreViewModel
            {
                Id = store.Id,
                StoreName = store.StoreName,
                FullNameResponsible = store.FullNameResponsible,
                CreationDate = store.CreationDate,
                StoreDishes = store.StoreDishes.ToDictionary(recSD => recSD.Key, recSD =>
                (source.Dishes.FirstOrDefault(recD => recD.Id == recSD.Key)?.DishName, recSD.Value))
            };
        }

        public bool CheckAvailabilityAndWriteOff(int SetId, int SetCount)
        {
            var set = source.Sets.FirstOrDefault(rec => rec.Id == SetId);

            if (set == null)
            {
                return false;
            }
            else
            {
                foreach (var dish in set.SetDishes)
                {
                    int count = 0;
                    count = source.Stores.Where(store =>
                    store.StoreDishes.ContainsKey(dish.Key))
                    .Sum(store => store.StoreDishes[dish.Key]);

                    if (count < dish.Value * SetCount)
                    {
                        return false;
                    }
                }
                foreach (var setDishes in set.SetDishes)
                {
                    int count = setDishes.Value * SetCount;
                    var stores = source.Stores.Where(component => component.StoreDishes.ContainsKey(setDishes.Key));

                    foreach (Store store in stores)
                    {
                        if (store.StoreDishes[setDishes.Key] <= count)
                        {
                            count -= store.StoreDishes[setDishes.Key];
                            store.StoreDishes.Remove(setDishes.Key);
                        }
                        else
                        {
                            store.StoreDishes[setDishes.Key] -= count;
                            count = 0;
                        }
                        if (count == 0)
                        {
                            break;
                        }
                    }
                }
                return true;
            }
        }
    }
}
