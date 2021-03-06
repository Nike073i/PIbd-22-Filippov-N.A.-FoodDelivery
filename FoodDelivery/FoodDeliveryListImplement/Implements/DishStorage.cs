using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.Interfaces;
using FoodDeliveryBusinnesLogic.ViewModels;
using FoodDeliveryListImplement.Models;
using System;
using System.Collections.Generic;

namespace FoodDeliveryListImplement.Implements
{
    public class DishStorage : IDishStorage
    {
        private readonly DataListSingleton source;
        public DishStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<DishViewModel> GetFullList()
        {
            List<DishViewModel> result = new List<DishViewModel>();
            foreach (var dish in source.Dishes)
            {
                result.Add(CreateModel(dish));
            }
            return result;
        }

        public List<DishViewModel> GetFilteredList(DishBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<DishViewModel> result = new List<DishViewModel>();
            foreach (var dish in source.Dishes)
            {
                if (dish.DishName.Contains(model.DishName))
                {
                    result.Add(CreateModel(dish));
                }
            }
            return result;
        }
        public DishViewModel GetElement(DishBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var dish in source.Dishes)
            {
                if (dish.Id == model.Id || dish.DishName == model.DishName)
                {
                    return CreateModel(dish);
                }
            }
            return null;
        }
        public void Insert(DishBindingModel model)
        {
            Dish tempDish = new Dish { Id = 1 };
            foreach (var dish in source.Dishes)
            {
                if (dish.Id >= tempDish.Id)
                {
                    tempDish.Id = dish.Id + 1;
                }
            }
            source.Dishes.Add(CreateModel(model, tempDish));
        }
        public void Update(DishBindingModel model)
        {
            Dish tempDish = null;
            foreach (var dish in source.Dishes)
            {
                if (dish.Id == model.Id)
                {
                    tempDish = dish;
                }
            }
            if (tempDish == null)
            {
                throw new Exception("Набор не найден");
            }
            CreateModel(model, tempDish);
        }
        public void Delete(DishBindingModel model)
        {
            for (int i = 0; i < source.Dishes.Count; ++i)
            {
                if (source.Dishes[i].Id == model.Id.Value)
                {
                    source.Dishes.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Набор не найден");
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
