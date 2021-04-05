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
        public ReportLogic(ISetStorage setStorage, IDishStorage
       dishStorage, IOrderStorage orderStorage)
        {
            _setStorage = setStorage;
            _dishStorage = dishStorage;
            _orderStorage = orderStorage;
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
    }
}
