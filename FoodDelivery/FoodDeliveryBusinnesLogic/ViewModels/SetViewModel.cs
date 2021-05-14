using FoodDeliveryBusinnesLogic.Attributes;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FoodDeliveryBusinnesLogic.ViewModels
{
    [DataContract]
    public class SetViewModel
    {
        [DataMember]
        [Column(title: "Номер", width: 50)]
        public int Id { get; set; }

        [DataMember]
        [Column(title: "Название набора", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string SetName { get; set; }

        [DataMember]
        [Column(title: "Цена", width: 75)]
        public decimal Price { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> SetDishes { get; set; }
    }
}
