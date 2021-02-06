using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.Interfaces;
using FoodDeliveryBusinnesLogic.ViewModels;
using FoodDeliveryListImplement.Models;
using System;
using System.Collections.Generic;

namespace FoodDeliveryListImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        private readonly DataListSingleton source;
        public OrderStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<OrderViewModel> GetFullList()
        {
            List<OrderViewModel> result = new List<OrderViewModel>();
            foreach (var component in source.Orders)
            {
                result.Add(CreateModel(component));
            }
            return result;
        }

        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<OrderViewModel> result = new List<OrderViewModel>();
            foreach (var component in source.Orders)
            {
                if (component.SetId.ToString().Contains(model.SetId.ToString()))
                {
                    result.Add(CreateModel(component));
                }
            }
            return result;
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var component in source.Orders)
            {
                if (component.Id == model.Id || component.SetId ==
               model.SetId)
                {
                    return CreateModel(component);
                }
            }
            return null;
        }
        public void Insert(OrderBindingModel model)
        {
            Order tempComponent = new Order { Id = 1 };
            foreach (var component in source.Orders)
            {
                if (component.Id >= tempComponent.Id)
                {
                    tempComponent.Id = component.Id + 1;
                }
            }
            source.Orders.Add(CreateModel(model, tempComponent));
        }
        public void Update(OrderBindingModel model)
        {
            Order tempComponent = null;
            foreach (var component in source.Orders)
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
        public void Delete(OrderBindingModel model)
        {
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id == model.Id.Value)
                {
                    source.Orders.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Order CreateModel(OrderBindingModel model, Order order)
        {
            order.SetId = model.SetId;
            order.Count = model.Count;
            order.Sum = model.Sum;
            order.Status = model.Status;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            return order;
        }
        private OrderViewModel CreateModel(Order order)
        {
            string setName = null;
            foreach (var set in source.Sets)
            {
                if (set.Id == order.SetId)
                {
                    setName = set.SetName;
                }
            }
            return new OrderViewModel
            {
                Id = order.Id,
                SetId = order.SetId,
                SetName = setName,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                Status = order.Status,
                DateImplement = order.DateImplement
            };
        }
    }
}
