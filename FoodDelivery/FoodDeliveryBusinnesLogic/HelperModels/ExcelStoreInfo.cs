using FoodDeliveryBusinnesLogic.ViewModels;
using System.Collections.Generic;

namespace FoodDeliveryBusinnesLogic.HelperModels
{
    public class ExcelStoreInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportStoreDishViewModel> StoreDishes { get; set; }
    }
}
