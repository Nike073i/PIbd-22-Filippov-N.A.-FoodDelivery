using System.ComponentModel;
using System.Runtime.Serialization;

namespace FoodDeliveryBusinnesLogic.ViewModels
{
    [DataContract]
    public class DishViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Название блюда")]
        public string DishName { get; set; }
    }
}
