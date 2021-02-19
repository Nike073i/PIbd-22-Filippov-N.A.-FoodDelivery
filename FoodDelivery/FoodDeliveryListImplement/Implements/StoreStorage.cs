using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.Interfaces;
using FoodDeliveryBusinnesLogic.ViewModels;
using System;
using System.Collections.Generic;

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
        }

        public void Update(StoreBindingModel model)
        {
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
    }
}
