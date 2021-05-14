using FoodDeliveryBusinnesLogic.Attributes;

namespace FoodDeliveryBusinnesLogic.ViewModels
{
    public class DishViewModel
    {
        [Column(title: "Номер", width: 50)]
        public int Id { get; set; }

        [Column(title: "Название блюда", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string DishName { get; set; }
    }
}
