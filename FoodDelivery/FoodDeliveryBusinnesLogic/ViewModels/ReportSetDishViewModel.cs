using System;
using System.Collections.Generic;

namespace FoodDeliveryBusinnesLogic.ViewModels
{
    public class ReportSetDishViewModel
    {
        public string SetName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> Dishes { get; set; }
    }
}
