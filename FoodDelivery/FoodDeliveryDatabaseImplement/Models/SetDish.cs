using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryDatabaseImplement.Models
{
    public class SetDish
    {
        public int Id { get; set; }
        public int SetId { get; set; }
        public int DishId { get; set; }

        [Required]
        public int Count { get; set; }
        public virtual Dish Dish { get; set; }
        public virtual Set Set { get; set; }
    }
}
