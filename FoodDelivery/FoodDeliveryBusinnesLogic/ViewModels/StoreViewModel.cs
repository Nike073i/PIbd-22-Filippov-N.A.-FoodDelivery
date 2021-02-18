using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FoodDeliveryBusinnesLogic.ViewModels
{
    public class StoreViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название")]
        public string StoreName { get; set; }

        [DisplayName("ФИО ответственного")]
        public string FullNameResponsible { get; set; }

        [DisplayName("Дата создания")]
        public DateTime CreationDate { get; set; }

        public Dictionary<int, (string, int)> StoreDishes { get; set; }
    }
}
