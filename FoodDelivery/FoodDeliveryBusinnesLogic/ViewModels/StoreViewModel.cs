using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace FoodDeliveryBusinnesLogic.ViewModels
{
    [DataContract]
    public class StoreViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Название")]
        public string StoreName { get; set; }

        [DataMember]
        [DisplayName("ФИО ответственного")]
        public string FullNameResponsible { get; set; }

        [DataMember]
        [DisplayName("Дата создания")]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> StoreDishes { get; set; }
    }
}
