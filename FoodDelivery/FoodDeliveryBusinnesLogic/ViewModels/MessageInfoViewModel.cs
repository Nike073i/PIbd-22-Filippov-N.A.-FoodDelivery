using FoodDeliveryBusinnesLogic.Attributes;
using System;
using System.Runtime.Serialization;

namespace FoodDeliveryBusinnesLogic.ViewModels
{
    [DataContract]
    public class MessageInfoViewModel
    {
        [DataMember]
        [Column(visible: false)]
        public string MessageId { get; set; }
        [Column(title: "Отправитель", width: 160)]
        [DataMember]
        public string SenderName { get; set; }
        [Column(title: "Дата письма", width: 75)]
        [DataMember]
        public DateTime DateDelivery { get; set; }
        [Column(title: "Заголовок", width: 125)]
        [DataMember]
        public string Subject { get; set; }
        [Column(title: "Текст", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string Body { get; set; }
    }
}
