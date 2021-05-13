using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.Enums;
using FoodDeliveryBusinnesLogic.Interfaces;
using FoodDeliveryBusinnesLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FoodDeliveryBusinnesLogic.BusinessLogics
{
    public class WorkModeling
    {
        private readonly IImplementerStorage _implementerStorage;
        private readonly IOrderStorage _orderStorage;
        private readonly OrderLogic _orderLogic;
        private readonly Random rnd;

        public WorkModeling(IImplementerStorage implementerStorage, IOrderStorage orderStorage, OrderLogic orderLogic)
        {
            this._implementerStorage = implementerStorage;
            this._orderStorage = orderStorage;
            this._orderLogic = orderLogic;
            rnd = new Random(1000);
        }

        public void DoWork()
        {
            var implementers = _implementerStorage.GetFullList();
            var orders = _orderStorage.GetFilteredList(new OrderBindingModel { FreeOrders = true });
            foreach (var implementer in implementers)
            {
                WorkerWorkAsync(implementer, orders);
            }
        }

        private async void WorkerWorkAsync(ImplementerViewModel implementer, List<OrderViewModel> orders)
        {
            // ищем заказы, которые уже в работе (вдруг исполнителя прервали)
            var runOrders = await Task.Run(() => _orderStorage.GetFilteredList(new OrderBindingModel
            {
                ImplementerId = implementer.Id,
                Status = OrderStatus.Выполняется
            }));
            foreach (var order in runOrders)
            {
                ExecuteOrder(implementer, order);
            }
            var OrdersWithoutDishes = await Task.Run(() => _orderStorage.GetFilteredList(new OrderBindingModel
            {
                ImplementerId = implementer.Id,
                Status = OrderStatus.Требуются_материалы
            }));

            foreach (var order in OrdersWithoutDishes)
            {
                try
                {
                    _orderLogic.TakeOrderInWork(new ChangeStatusBindingModel { OrderId = order.Id, ImplementerId = implementer.Id });
                    if (_orderStorage.GetElement(new OrderBindingModel { Id = order.Id }).Status == OrderStatus.Требуются_материалы)
                    {
                        continue;
                    }
                    ExecuteOrder(implementer, order);
                }
                catch (Exception) { }
            }
            foreach (var order in orders)
            {
                try
                {
                    _orderLogic.TakeOrderInWork(new ChangeStatusBindingModel { OrderId = order.Id, ImplementerId = implementer.Id });
                    if (_orderStorage.GetElement(new OrderBindingModel { Id = order.Id }).Status == OrderStatus.Требуются_материалы)
                    {
                        continue;
                    }
                    ExecuteOrder(implementer, order);
                }
                catch (Exception) { }
            }
        }
        private void ExecuteOrder(ImplementerViewModel implementer, OrderViewModel order)
        {
            // делаем
            Thread.Sleep(implementer.WorkingTime * rnd.Next(1, 5) * order.Count);
            _orderLogic.FinishOrder(new ChangeStatusBindingModel
            {
                OrderId = order.Id,
                ImplementerId = implementer.Id
            });
            // отдыхаем
            Thread.Sleep(implementer.PauseTime);
        }
    }
}
