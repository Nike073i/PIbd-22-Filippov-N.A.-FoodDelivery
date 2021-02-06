using System.Collections.Generic;

namespace FoodDeliveryBusinnesLogic.BindingModels
{
    public class SetBindingModel
    {
        public int? Id { get; set; }
        public string SetName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> SetDishes { get; set; }
    }
}
