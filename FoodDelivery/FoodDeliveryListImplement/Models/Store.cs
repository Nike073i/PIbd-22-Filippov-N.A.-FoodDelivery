using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDeliveryListImplement.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string FullNameResponsible { get; set; }
        public DateTime CreationDate { get; set; }
        public Dictionary<int, int> StoreDishes { get; set; }
    }
}
