using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.Interfaces;
using FoodDeliveryBusinnesLogic.ViewModels;
using FoodDeliveryFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDeliveryFileImplement.Implements
{
    public class DishStorage : IDishStorage
    {
        private readonly FileDataListSingleton source;
        public DishStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<DishViewModel> GetFullList() => source.Dishes.Select(CreateModel).ToList();

        public List<DishViewModel> GetFilteredList(DishBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Dishes.Where(rec => rec.DishName.Contains(model.DishName)).Select(CreateModel).ToList();
        }
        public DishViewModel GetElement(DishBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var dish = source.Dishes.FirstOrDefault(rec => rec.DishName == model.DishName || rec.Id == model.Id);
            return dish != null ? CreateModel(dish) : null;
        }
        public void Insert(DishBindingModel model)
        {
            int maxId = source.Dishes.Count > 0 ? source.Dishes.Max(rec => rec.Id) : 0;
            var newDish = new Dish { Id = maxId + 1 };
            source.Dishes.Add(CreateModel(model, newDish));
        }
        public void Update(DishBindingModel model)
        {
            var dish = source.Dishes.FirstOrDefault(rec => rec.Id == model.Id);
            if (dish == null)
            {
                throw new Exception("Блюдо не найдено");
            }
            CreateModel(model, dish);
        }
        public void Delete(DishBindingModel model)
        {
            Dish dish = source.Dishes.FirstOrDefault(rec => rec.Id == model.Id);
            if (dish != null)
            {
                source.Dishes.Remove(dish);
            }
            else
            {
                throw new Exception("Блюдо не найдено");
            }
        }
        private Dish CreateModel(DishBindingModel model, Dish dish)
        {
            dish.DishName = model.DishName;
            return dish;
        }
        private DishViewModel CreateModel(Dish dish)
        {
            return new DishViewModel
            {
                Id = dish.Id,
                DishName = dish.DishName
            };
        }
    }
}
