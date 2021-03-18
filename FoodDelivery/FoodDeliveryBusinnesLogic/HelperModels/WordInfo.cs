using FoodDeliveryBusinnesLogic.ViewModels;
using System.Collections.Generic;

namespace FoodDeliveryBusinnesLogic.HelperModels
{
    class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<DishViewModel> Dishes { get; set; }
    }
}
