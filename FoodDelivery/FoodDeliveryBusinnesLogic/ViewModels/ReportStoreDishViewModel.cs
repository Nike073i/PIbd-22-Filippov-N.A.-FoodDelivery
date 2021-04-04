using System;
using System.Collections.Generic;

namespace FoodDeliveryBusinnesLogic.ViewModels
{
    public class ReportStoreDishViewModel
    {
        public string StoreName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> Dishes { get; set; }
    }
}
