using System.ComponentModel;

namespace FoodDeliveryBusinnesLogic.ViewModels
{
    public class DishViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название блюда")]
        public string DishName { get; set; }
    }
}
