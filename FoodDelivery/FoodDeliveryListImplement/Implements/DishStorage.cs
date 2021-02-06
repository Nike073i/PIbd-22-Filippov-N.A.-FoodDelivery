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
            foreach (var component in source.Dishes)
            {
                result.Add(CreateModel(component));
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
            foreach (var component in source.Dishes)
            {
                if (component.DishName.Contains(model.DishName))
                {
                    result.Add(CreateModel(component));
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
            foreach (var component in source.Dishes)
            {
                if (component.Id == model.Id || component.DishName ==
               model.DishName)
                {
                    return CreateModel(component);
                }
            }
            return null;
        }
        public void Insert(DishBindingModel model)
        {
            Dish tempComponent = new Dish { Id = 1 };
            foreach (var component in source.Dishes)
            {
                if (component.Id >= tempComponent.Id)
                {
                    tempComponent.Id = component.Id + 1;
                }
            }
            source.Dishes.Add(CreateModel(model, tempComponent));
        }
        public void Update(DishBindingModel model)
        {
            Dish tempComponent = null;
            foreach (var component in source.Dishes)
            {
                if (component.Id == model.Id)
                {
                    tempComponent = component;
                }
            }
            if (tempComponent == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempComponent);
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
            throw new Exception("Элемент не найден");
        }
        private Dish CreateModel(DishBindingModel model, Dish component)
        {
            component.DishName = model.DishName;
            return component;
        }
        private DishViewModel CreateModel(Dish component)
        {
            return new DishViewModel
            {
                Id = component.Id,
                DishName = component.DishName
            };
        }
    }
}
