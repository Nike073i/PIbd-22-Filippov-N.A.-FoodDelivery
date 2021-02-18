using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDeliveryBusinnesLogic.BindingModels
{
    public class StoreBindingModel
    {
        public int? Id { get; set; }

        public string StoreName { get; set; }

        public string FullNameResponsible { get; set; }

        public DateTime CreationDate { get; set; }

        public Dictionary<int, (string, int)> StoreDishes { get; set; }
    }   
}
