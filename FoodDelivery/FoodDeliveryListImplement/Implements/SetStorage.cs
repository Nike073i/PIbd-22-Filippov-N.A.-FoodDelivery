using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.Interfaces;
using FoodDeliveryBusinnesLogic.ViewModels;
using FoodDeliveryListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDeliveryListImplement.Implements
{
    public class SetStorage : ISetStorage
    {
        private readonly DataListSingleton source;
        public SetStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<SetViewModel> GetFullList()
        {
            List<SetViewModel> result = new List<SetViewModel>();
            foreach (var set in source.Sets)
            {
                result.Add(CreateModel(set));
            }
            return result;
        }
        public List<SetViewModel> GetFilteredList(SetBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<SetViewModel> result = new List<SetViewModel>();
            foreach (var set in source.Sets)
            {
                if (set.SetName.Contains(model.SetName))
                {
                    result.Add(CreateModel(set));
                }
            }
            return result;
        }
        public SetViewModel GetElement(SetBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var set in source.Sets)
            {
                if (set.Id == model.Id || set.SetName == model.SetName)
                {
                    return CreateModel(set);
                }
            }
            return null;
        }
        public void Insert(SetBindingModel model)
        {
            Set tempSet = new Set
            {
                Id = 1,
                SetDishes = new Dictionary<int, int>()
            };
            foreach (var set in source.Sets)
            {
                if (set.Id >= tempSet.Id)
                {
                    tempSet.Id = set.Id + 1;
                }
            }
            source.Sets.Add(CreateModel(model, tempSet));
        }
        public void Update(SetBindingModel model)
        {
            Set tempSet = null;
            foreach (var set in source.Sets)
            {
                if (set.Id == model.Id)
                {
                    tempSet = set;
                }
            }
            if (tempSet == null)
            {
                throw new Exception("Набор не найден");
            }
            CreateModel(model, tempSet);
        }
        public void Delete(SetBindingModel model)
        {
            for (int i = 0; i < source.Sets.Count; i++)
            {
                if (source.Sets[i].Id == model.Id)
                {
                    source.Sets.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Набор не найден");
        }
        private Set CreateModel(SetBindingModel model, Set set)
        {
            set.SetName = model.SetName;
            set.Price = model.Price;
            foreach (var key in set.SetDishes.Keys.ToList())
            {
                if (!model.SetDishes.ContainsKey(key))
                {
                    set.SetDishes.Remove(key);
                }
            }
            foreach (var dish in model.SetDishes)
            {
                if (set.SetDishes.ContainsKey(dish.Key))
                {
                    set.SetDishes[dish.Key] =
                    model.SetDishes[dish.Key].Item2;
                }
                else
                {
                    set.SetDishes.Add(dish.Key,
                    model.SetDishes[dish.Key].Item2);
                }
            }
            return set;
        }
        private SetViewModel CreateModel(Set set)
        {
            Dictionary<int, (string, int)> setDishes = new Dictionary<int, (string, int)>();
            foreach (var sd in set.SetDishes)
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
                setDishes.Add(sd.Key, (dishName, sd.Value));
            }
            return new SetViewModel
            {
                Id = set.Id,
                SetName = set.SetName,
                Price = set.Price,
                SetDishes = setDishes
            };
        }
    }
}
