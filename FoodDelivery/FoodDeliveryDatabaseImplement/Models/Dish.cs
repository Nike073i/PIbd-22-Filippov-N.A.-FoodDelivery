using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryDatabaseImplement.Models
{
    public class Dish
    {
        public int Id { get; set; }

        [Required]
        public string DishName { get; set; }

        [ForeignKey("DishId")]
        public virtual List<SetDish> SetDishes { get; set; }

        [ForeignKey("DishId")]
        public virtual List<StoreDish> StoreDishes { get; set; }
    }
}
