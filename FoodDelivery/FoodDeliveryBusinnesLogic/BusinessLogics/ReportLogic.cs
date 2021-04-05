using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.HelperModels;
using FoodDeliveryBusinnesLogic.Interfaces;
using FoodDeliveryBusinnesLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDeliveryBusinnesLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IDishStorage _dishStorage;
        private readonly ISetStorage _setStorage;
        private readonly IOrderStorage _orderStorage;
        private readonly IStoreStorage _storeStorage;
        public ReportLogic(ISetStorage setStorage, IDishStorage
       dishStorage, IOrderStorage orderStorage, IStoreStorage storeStorage)
        {
            _setStorage = setStorage;
            _dishStorage = dishStorage;
            _orderStorage = orderStorage;
            _storeStorage = storeStorage;
        }
        public List<ReportSetDishViewModel> GetSetDish()
        {
            var sets = _setStorage.GetFullList();
            var list = new List<ReportSetDishViewModel>();
            foreach (var set in sets)
            {
                var record = new ReportSetDishViewModel
                {
                    SetName = set.SetName,
                    Dishes = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var dish in set.SetDishes)
                {
                    record.Dishes.Add(new Tuple<string, int>(dish.Value.Item1, dish.Value.Item2));
                    record.TotalCount += dish.Value.Item2;
                }
                list.Add(record);
            }
            return list;
        }

        /// Получение списка заказов за определенный период
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return _orderStorage.GetFilteredList(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                SetName = x.SetName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
           .ToList();
        }

        public List<ReportStoreDishViewModel> GetStoreDish()
        {
            var stores = _storeStorage.GetFullList();
            var list = new List<ReportStoreDishViewModel>();
            foreach (var store in stores)
            {
                var record = new ReportStoreDishViewModel
                {
                    StoreName = store.StoreName,
                    Dishes = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var dish in store.StoreDishes)
                {
                    record.Dishes.Add(new Tuple<string, int>(dish.Value.Item1, dish.Value.Item2));
                    record.TotalCount += dish.Value.Item2;
                }
                list.Add(record);
            }
            return list;
        }

        public List<ReportOrdersByDateViewModel> GetOrdersByDate()
        {
            return _orderStorage.GetFullList()
                .GroupBy(order => order.DateCreate.ToShortDateString())
                .Select(rec => new ReportOrdersByDateViewModel
                {
                    Date = Convert.ToDateTime(rec.Key),
                    Count = rec.Count(),
                    Sum = rec.Sum(order => order.Sum)
                })
                .ToList();
        }

        /// Сохранение наборов в файл-Word
        public void SaveSetsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список наборов",
                Sets = _setStorage.GetFullList()
            });
        }

        /// Сохранение наборов с указанием компонентов в файл-Excel
        public void SaveSetDishesToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список наборов с компонентами",
                SetDishes = GetSetDish()
            });
        }

        /// Сохранение заказов в файл-Pdf
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }

        /// Сохранение складов в файл-Word
        public void SaveStoresToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateStoreDoc(new WordStoreInfo
            {
                FileName = model.FileName,
                Title = "Список складов",
                Stores = _storeStorage.GetFullList()
            });
        }

        /// Сохранение складов с хранящимися блюдам в файл-Excel
        public void SaveStoreDishToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateStoreDoc(new ExcelStoreInfo
            {
                FileName = model.FileName,
                Title = "Список складов с блюдами",
                StoreDishes = GetStoreDish()
            });
        }

        /// Сохранение заказов в файл-Pdf
        public void SaveOrdersByDateToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateOrdersByDateDoc(new PdfOrdersByDateInfo
            {
                FileName = model.FileName,
                Title = "Отчет по заказам по датам",
                Orders = GetOrdersByDate()
            });
        }
    }
}
