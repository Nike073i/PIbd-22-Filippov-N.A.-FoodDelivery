using System.Collections.Generic;
using System.ComponentModel;

namespace FoodDeliveryBusinnesLogic.ViewModels
{
    public class SetViewModel
    {
        public int? Id { get; set; }

        [DisplayName("Название набора")]
        public string SetName { get; set; }

        [DisplayName("Цена")]
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> SetDishes { get; set; }
    }
}
