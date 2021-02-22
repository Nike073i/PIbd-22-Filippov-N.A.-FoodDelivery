using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.Interfaces;
using FoodDeliveryBusinnesLogic.ViewModels;
using FoodDeliveryFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDeliveryFileImplement.Implements
{
    public class SetStorage : ISetStorage
    {
        private readonly FileDataListSingleton source;
        public SetStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<SetViewModel> GetFullList() => source.Sets.Select(CreateModel).ToList();
        public List<SetViewModel> GetFilteredList(SetBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Sets.Where(rec => rec.SetName.Contains(model.SetName)).Select(CreateModel).ToList();
        }
        public SetViewModel GetElement(SetBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var set = source.Sets.FirstOrDefault(rec => rec.SetName == model.SetName || rec.Id == model.Id);
            return set != null ? CreateModel(set) : null;
        }
        public void Insert(SetBindingModel model)
        {
            int maxId = source.Sets.Count > 0 ? source.Sets.Max(rec => rec.Id) : 0;
            var element = new Set
            {
                Id = maxId + 1,
                SetDishes = new Dictionary<int, int>()
            };
            source.Sets.Add(CreateModel(model, element));
        }
        public void Update(SetBindingModel model)
        {
            var element = source.Sets.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Набор не найден");
            }
            CreateModel(model, element);
        }
        public void Delete(SetBindingModel model)
        {
            Set element = source.Sets.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Sets.Remove(element);
            }
            else
            {
                throw new Exception("Набор не найден");
            }
        }
        private Set CreateModel(SetBindingModel model, Set set)
        {
            set.SetName = model.SetName;
            set.Price = model.Price;
            // удаляем убранные
            foreach (var key in set.SetDishes.Keys.ToList())
            {
                if (!model.SetDishes.ContainsKey(key))
                {
                    set.SetDishes.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
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
            return new SetViewModel
            {
                Id = set.Id,
                SetName = set.SetName,
                Price = set.Price,
                SetDishes = set.SetDishes.ToDictionary(recSD => recSD.Key, resSD =>
                (source.Dishes.FirstOrDefault(recC => recC.Id == resSD.Key)?.DishName, resSD.Value))
            };
        }
    }
}
