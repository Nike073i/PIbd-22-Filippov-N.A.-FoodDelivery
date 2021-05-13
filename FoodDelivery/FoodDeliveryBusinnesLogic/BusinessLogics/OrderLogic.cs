using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.Enums;
using FoodDeliveryBusinnesLogic.Interfaces;
using FoodDeliveryBusinnesLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace FoodDeliveryBusinnesLogic.BusinessLogics
{
    public class OrderLogic
    {
        private readonly object locker = new object();
        private readonly IOrderStorage _orderStorage;
        private readonly IStoreStorage _storeStorage;
        public OrderLogic(IOrderStorage orderStorage, IStoreStorage storeStorage)
        {
            _orderStorage = orderStorage;
            _storeStorage = storeStorage;
        }
        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            if (model == null)
            {
                return _orderStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<OrderViewModel> { _orderStorage.GetElement(model) };
            }
            return _orderStorage.GetFilteredList(model);
        }
        public void CreateOrder(CreateOrderBindingModel model)
        {
            _orderStorage.Insert(new OrderBindingModel
            {
                ClientId = model.ClientId,
                SetId = model.SetId,
                Count = model.Count,
                Sum = model.Sum,
                DateCreate = DateTime.Now,
                Status = OrderStatus.Принят
            });
        }
        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            lock (locker)
            {
                var order = _orderStorage.GetElement(new OrderBindingModel
                {
                    Id = model.OrderId
                });
                if (order == null)
                {
                    throw new Exception("Не найден заказ");
                }
                if (order.Status != OrderStatus.Принят && order.Status != OrderStatus.Требуются_материалы)
                {
                    throw new Exception("Заказ не в статусе \"Принят\" или \"Требуются материалы\"");
                }
                if (order.ImplementerId.HasValue && order.ImplementerId != model.ImplementerId)
                {
                    throw new Exception("У заказа уже есть исполнитель");
                }
                if (!_storeStorage.CheckAvailabilityAndWriteOff(order.SetId, order.Count))
                {
                    order.Status = OrderStatus.Требуются_материалы;
                }
                else
                {
                    order.Status = OrderStatus.Выполняется;
                    order.DateImplement = DateTime.Now;
                }
                _orderStorage.Update(new OrderBindingModel
                {
                    Id = order.Id,
                    ClientId = order.ClientId,
                    SetId = order.SetId,
                    Count = order.Count,
                    Sum = order.Sum,
                    DateCreate = order.DateCreate,
                    DateImplement = order.DateImplement,
                    ImplementerId = model.ImplementerId,
                    Status = order.Status
                });
            }
        }
        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel
            {
                Id = model.OrderId
            });
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != OrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                SetId = order.SetId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                ImplementerId = order.ImplementerId,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Готов
            });
        }
        public void PayOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel
            {
                Id = model.OrderId
            });
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                SetId = order.SetId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                ImplementerId = order.ImplementerId,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Оплачен
            });
        }
    }
}
