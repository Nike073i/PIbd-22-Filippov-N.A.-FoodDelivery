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
            var dishes = _dishStorage.GetFullList();
            var sets = _setStorage.GetFullList();
            var list = new List<ReportSetDishViewModel>();
            foreach (var dish in dishes)
            {
                var record = new ReportSetDishViewModel
                {
                    DishName = dish.DishName,
                    Sets = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var set in sets)
                {
                    if (set.SetDishes.ContainsKey(dish.Id))
                    {
                        record.Sets.Add(new Tuple<string, int>(set.SetName,
                       set.SetDishes[dish.Id].Item2));
                        record.TotalCount += set.SetDishes[dish.Id].Item2;
                    }
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

        /// Сохранение блюд в файл-Word
        public void SaveComponentsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список наборов",
                Sets = _setStorage.GetFullList()
            });
        }

        /// Сохранение блюд с указанием наборов в файл-Excel
        public void SaveProductComponentToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список блюд",
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
