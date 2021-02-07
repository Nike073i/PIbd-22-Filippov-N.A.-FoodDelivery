using System.Collections.Generic;

namespace FoodDeliveryListImplement.Models
{
    public class Set
    {
        public int Id { get; set; }
        public string SetName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, int> SetDishes { get; set; }
    }
}
