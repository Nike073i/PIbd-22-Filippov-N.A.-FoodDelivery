using FoodDeliveryBusinnesLogic.Attributes;
using FoodDeliveryBusinnesLogic.Enums;
using System;
using System.Runtime.Serialization;

namespace FoodDeliveryBusinnesLogic.ViewModels
{
    [DataContract]
    public class OrderViewModel
    {
        [Column(title: "Номер", width: 50)]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ClientId { get; set; }

        [DataMember]
        [Column(title: "Клиент", width: 150)]
        public string ClientFIO { get; set; }

        [DataMember]
        public int SetId { get; set; }

        [DataMember]
        [Column(title: "Набор", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string SetName { get; set; }

        [DataMember]
        [Column(title: "Количество", width: 75)]
        public int Count { get; set; }

        [DataMember]
        [Column(title: "Сумма", width: 75)]
        public decimal Sum { get; set; }

        [DataMember]
        [Column(title: "Статус", width: 75)]
        public OrderStatus Status { get; set; }

        [DataMember]
        [Column(title: "Дата создания", width: 75)]
        public DateTime DateCreate { get; set; }

        [DataMember]
        public int? ImplementerId { get; set; }

        [DataMember]
        [Column(title: "Исполнитель", width: 80)]
        public string ImplementerFIO { get; set; }

        [DataMember]
        [Column(title: "Дата выполнения", width: 75)]
        public DateTime? DateImplement { get; set; }
    }
}
