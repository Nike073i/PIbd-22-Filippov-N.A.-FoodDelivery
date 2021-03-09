using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryDatabaseImplement.Models
{
    public class Set
    {
        public int Id { set; get; }

        [Required]
        public string SetName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [ForeignKey("SetId")]
        public virtual List<SetDish> SetDishes { get; set; }

        [ForeignKey("SetId")]
        public virtual List<Order> Orders { get; set; }
    }
}
