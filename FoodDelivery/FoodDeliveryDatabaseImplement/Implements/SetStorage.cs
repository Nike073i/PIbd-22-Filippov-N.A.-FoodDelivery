using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.Interfaces;
using FoodDeliveryBusinnesLogic.ViewModels;
using FoodDeliveryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDeliveryDatabaseImplement.Implements
{
    public class SetStorage : ISetStorage
    {
        public List<SetViewModel> GetFullList()
        {
            using (var context = new FoodDeliveryDatabase())
            {
                return context.Sets.Include(rec => rec.SetDishes)
               .ThenInclude(rec => rec.Dish)
               .ToList()
               .Select(rec => new SetViewModel
               {
                   Id = rec.Id,
                   SetName = rec.SetName,
                   Price = rec.Price,
                   SetDishes = rec.SetDishes.ToDictionary(recSD => recSD.DishId, recSD =>
               (recSD.Dish?.DishName, recSD.Count))
               })
               .ToList();
            }
        }
        public List<SetViewModel> GetFilteredList(SetBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new FoodDeliveryDatabase())
            {
                return context.Sets
                .Include(rec => rec.SetDishes)
               .ThenInclude(rec => rec.Dish)
               .Where(rec => rec.SetName.Contains(model.SetName))
               .ToList()
               .Select(rec => new SetViewModel
               {
                   Id = rec.Id,
                   SetName = rec.SetName,
                   Price = rec.Price,
                   SetDishes = rec.SetDishes.ToDictionary(recSC =>
                   recSC.DishId, recSD => (recSD.Dish?.DishName, recSD.Count))
               }).ToList();
            }
        }
        public SetViewModel GetElement(SetBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new FoodDeliveryDatabase())
            {
                var set = context.Sets
                .Include(rec => rec.SetDishes)
               .ThenInclude(rec => rec.Dish)
               .FirstOrDefault(rec => rec.SetName == model.SetName || rec.Id == model.Id);
                return set != null ?
                new SetViewModel
                {
                    Id = set.Id,
                    SetName = set.SetName,
                    Price = set.Price,
                    SetDishes = set.SetDishes.ToDictionary(recSD =>
                    recSD.DishId, recSD => (recSD.Dish?.DishName, recSD.Count))
                } : null;
            }
        }
        public void Insert(SetBindingModel model)
        {
            using (var context = new FoodDeliveryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Set newSet = new Set() { SetName = model.SetName, Price = model.Price };
                        context.Sets.Add(newSet);
                        context.SaveChanges();
                        CreateModel(model, newSet, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Update(SetBindingModel model)
        {
            using (var context = new FoodDeliveryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var set = context.Sets.FirstOrDefault(rec => rec.Id ==
                       model.Id);
                        if (set == null)
                        {
                            throw new Exception("Набор не найден");
                        }
                        set.SetName = model.SetName;
                        set.Price = model.Price;
                        CreateModel(model, set, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(SetBindingModel model)
        {
            using (var context = new FoodDeliveryDatabase())
            {
                Set set = context.Sets.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (set != null)
                {
                    context.Sets.Remove(set);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Набор не найден");
                }
            }
        }
        private Set CreateModel(SetBindingModel model, Set set,
       FoodDeliveryDatabase context)
        {
            if (model.Id.HasValue)
            {
                var setDishes = context.SetDishes.Where(rec =>
               rec.SetId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.SetDishes.RemoveRange(setDishes.Where(rec =>
               !model.SetDishes.ContainsKey(rec.DishId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateDish in setDishes)
                {
                    updateDish.Count = model.SetDishes[updateDish.DishId].Item2;
                    model.SetDishes.Remove(updateDish.DishId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var sd in model.SetDishes)
            {
                context.SetDishes.Add(new SetDish
                {
                    SetId = set.Id,
                    DishId = sd.Key,
                    Count = sd.Value.Item2
                });
                context.SaveChanges();
            }
            return set;
        }
    }
}
