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
    public class StoreStorage : IStoreStorage
    {
        public List<StoreViewModel> GetFullList()
        {
            using (var context = new FoodDeliveryDatabase())
            {
                return context.Stores
                    .Include(rec => rec.StoreDishes)
                    .ThenInclude(rec => rec.Dish)
                    .ToList()
                    .Select(rec => new StoreViewModel
                    {
                        Id = rec.Id,
                        StoreName = rec.StoreName,
                        FullNameResponsible = rec.FullNameResponsible,
                        CreationDate = rec.CreationDate,
                        StoreDishes = rec.StoreDishes.ToDictionary(recSD => recSD.DishId, recSD => (recSD.Dish?.DishName, recSD.Count))
                    })
                    .ToList();
            }
        }
        public List<StoreViewModel> GetFilteredList(StoreBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new FoodDeliveryDatabase())
            {
                return context.Stores
                    .Include(rec => rec.StoreDishes)
                    .ThenInclude(rec => rec.Dish)
                    .Where(rec => rec.StoreName
                    .Contains(model.StoreName))
                    .ToList()
                    .Select(rec => new StoreViewModel
                    {
                        Id = rec.Id,
                        StoreName = rec.StoreName,
                        FullNameResponsible = rec.FullNameResponsible,
                        CreationDate = rec.CreationDate,
                        StoreDishes = rec.StoreDishes.ToDictionary(recSD => recSD.DishId, recSD => (recSD.Dish?.DishName, recSD.Count))
                    })
                    .ToList();
            }
        }
        public StoreViewModel GetElement(StoreBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new FoodDeliveryDatabase())
            {
                var store = context.Stores
                    .Include(rec => rec.StoreDishes)
                    .ThenInclude(rec => rec.Dish)
                    .FirstOrDefault(rec => rec.StoreName == model.StoreName || rec.Id == model.Id);
                return store != null ?
                new StoreViewModel
                {
                    Id = store.Id,
                    StoreName = store.StoreName,
                    FullNameResponsible = store.FullNameResponsible,
                    CreationDate = store.CreationDate,
                    StoreDishes = store.StoreDishes.ToDictionary(recSD => recSD.DishId, recSD => (recSD.Dish?.DishName, recSD.Count))
                } : null;
            }
        }
        public void Insert(StoreBindingModel model)
        {
            using (var context = new FoodDeliveryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Store newStore = new Store()
                        {
                            StoreName = model.StoreName,
                            FullNameResponsible = model.FullNameResponsible,
                            CreationDate = DateTime.Now
                        };
                        context.Stores.Add(newStore);
                        context.SaveChanges();
                        CreateModel(model, newStore, context);
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
        public void Update(StoreBindingModel model)
        {
            using (var context = new FoodDeliveryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var store = context.Stores.FirstOrDefault(rec => rec.Id == model.Id);
                        if (store == null)
                        {
                            throw new Exception("Склад не найден");
                        }
                        store.StoreName = model.StoreName;
                        store.FullNameResponsible = model.FullNameResponsible;
                        store.CreationDate = model.CreationDate;
                        CreateModel(model, store, context);
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
        public void Delete(StoreBindingModel model)
        {
            using (var context = new FoodDeliveryDatabase())
            {
                Store store = context.Stores.FirstOrDefault(rec => rec.Id == model.Id);
                if (store != null)
                {
                    context.Stores.Remove(store);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Склад не найден");
                }
            }
        }
        private Store CreateModel(StoreBindingModel model, Store store, FoodDeliveryDatabase context)
        {
            if (model.Id.HasValue)
            {
                var storeDish = context.StoreDishes
                    .Where(rec => rec.StoreId == model.Id.Value)
                    .ToList();
                // удалили те, которых нет в модели
                context.StoreDishes.RemoveRange(storeDish
                    .Where(rec => !model.StoreDishes
                    .ContainsKey(rec.DishId))
                    .ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateDish in storeDish)
                {
                    if (model.StoreDishes.ContainsKey(updateDish.DishId))
                    {
                        updateDish.Count = model.StoreDishes[updateDish.DishId].Item2;
                        model.StoreDishes.Remove(updateDish.DishId);
                    }
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var sd in model.StoreDishes)
            {
                context.StoreDishes.Add(new StoreDish
                {
                    StoreId = store.Id,
                    DishId = sd.Key,
                    Count = sd.Value.Item2
                });
                context.SaveChanges();
            }
            return store;
        }
        public bool CheckAvailabilityAndWriteOff(int SetId, int SetCount)
        {
            using (var context = new FoodDeliveryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var setDishes = context.SetDishes.Where(x => x.SetId == SetId);
                        if (setDishes.Count() == 0)
                        {
                            throw new Exception("Набор не найден");
                        }
                        foreach (var setDish in setDishes)
                        {
                            int required = setDish.Count * SetCount;
                            var storeDishes = context.StoreDishes.Where(x => x.DishId == setDish.DishId);
                            int inStock = storeDishes.Sum(x => x.Count);
                            if (inStock < required)
                            {
                                throw new Exception("Недостаточно блюд на складе");
                            }
                            foreach (var rec in storeDishes)
                            {
                                int toRemove = required > rec.Count ? rec.Count : required;
                                rec.Count -= toRemove;
                                required -= toRemove;
                                if (required == 0) break;
                            }
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
