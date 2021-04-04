using FoodDeliveryBusinnesLogic.ViewModels;
using System.Collections.Generic;

namespace FoodDeliveryBusinnesLogic.HelperModels
{
    class PdfOrdersByDateInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportOrdersByDateViewModel> Orders { get; set; }
    }
}
