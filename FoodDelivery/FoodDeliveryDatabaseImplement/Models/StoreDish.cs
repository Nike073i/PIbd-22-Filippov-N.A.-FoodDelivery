using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryDatabaseImplement.Models
{
    public class StoreDish
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int DishId { get; set; }

        [Required]
        public int Count { get; set; }
        public virtual Dish Dish { get; set; }
        public virtual Store Store { get; set; }
    }
}
