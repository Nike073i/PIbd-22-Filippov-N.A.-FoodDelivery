using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryDatabaseImplement.Models
{
    public class Store
    {
        public int Id { get; set; }

        [Required]
        public string StoreName { get; set; }

        [Required]
        public string FullNameResponsible { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [ForeignKey("StoreId")]
        public virtual List<StoreDish> StoreDishes { get; set; }
    }
}
