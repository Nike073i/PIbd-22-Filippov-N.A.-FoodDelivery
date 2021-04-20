using FoodDeliveryBusinnesLogic.ViewModels;
using System.Collections.Generic;

namespace FoodDeliveryBusinnesLogic.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportSetDishViewModel> SetDishes { get; set; }
    }
}
