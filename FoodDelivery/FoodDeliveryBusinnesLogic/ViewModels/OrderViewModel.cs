using FoodDeliveryBusinnesLogic.Enums;
using System;
using System.ComponentModel;

namespace FoodDeliveryBusinnesLogic.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int SetId { get; set; }

        [DisplayName("Набор")]
        public string SetName { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }

        [DisplayName("Сумма")]
        public decimal Sum { get; set; }

        [DisplayName("Статус")]
        public OrderStatus Status { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        [DisplayName("Дата выполнения")]
        public DateTime? DateImplement { get; set; }
    }
}
