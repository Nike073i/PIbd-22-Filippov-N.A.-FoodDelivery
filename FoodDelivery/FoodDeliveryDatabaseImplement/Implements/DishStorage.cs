using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.Interfaces;
using FoodDeliveryBusinnesLogic.ViewModels;
using FoodDeliveryDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDeliveryDatabaseImplement.Implements
{
    public class DishStorage : IDishStorage
    {
        public List<DishViewModel> GetFullList()
        {
            using (var context = new FoodDeliveryDatabase())
            {
                return context.Dishes
                .Select(rec => new DishViewModel
                {
                    Id = rec.Id,
                    DishName = rec.DishName
                }).ToList();
            }
        }
        public List<DishViewModel> GetFilteredList(DishBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new FoodDeliveryDatabase())
            {
                return context.Dishes
                .Where(rec => rec.DishName.Contains(model.DishName))
               .Select(rec => new DishViewModel
               {
                   Id = rec.Id,
                   DishName = rec.DishName
               }).ToList();
            }
        }
        public DishViewModel GetElement(DishBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new FoodDeliveryDatabase())
            {
                var dish = context.Dishes
                .FirstOrDefault(rec => rec.DishName == model.DishName || rec.Id == model.Id);
                return dish != null ?
                new DishViewModel
                {
                    Id = dish.Id,
                    DishName = dish.DishName
                } : null;
            }
        }
        public void Insert(DishBindingModel model)
        {
            using (var context = new FoodDeliveryDatabase())
            {
                context.Dishes.Add(CreateModel(model, new Dish()));
                context.SaveChanges();
            }
        }
        public void Update(DishBindingModel model)
        {
            using (var context = new FoodDeliveryDatabase())
            {
                var dish = context.Dishes.FirstOrDefault(rec => rec.Id == model.Id);
                if (dish == null)
                {
                    throw new Exception("Блюдо не найдено");
                }
                CreateModel(model, dish);
                context.SaveChanges();
            }
        }
        public void Delete(DishBindingModel model)
        {
            using (var context = new FoodDeliveryDatabase())
            {
                Dish dish = context.Dishes.FirstOrDefault(rec => rec.Id == model.Id);
                if (dish != null)
                {
                    context.Dishes.Remove(dish);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Блюдо не найдено");
                }
            }
        }
        private Dish CreateModel(DishBindingModel model, Dish dish)
        {
            dish.DishName = model.DishName;
            return dish;
        }
    }
}
