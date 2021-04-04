using FoodDeliveryBusinnesLogic.ViewModels;
using System.Collections.Generic;

namespace FoodDeliveryBusinnesLogic.HelperModels
{
    class WordStoreInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<StoreViewModel> Stores { get; set; }
    }
}
