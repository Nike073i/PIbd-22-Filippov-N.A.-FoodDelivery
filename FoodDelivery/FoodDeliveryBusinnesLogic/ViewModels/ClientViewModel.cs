using FoodDeliveryBusinnesLogic.Attributes;
using System.Runtime.Serialization;

namespace FoodDeliveryBusinnesLogic.ViewModels
{
    [DataContract]
    public class ClientViewModel
    {
        [Column(title: "Номер", width: 50)]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Column(title: "ФИО клиента", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ClientFIO { get; set; }

        [DataMember]
        [Column(title: "Почта", width: 100)]
        public string Email { get; set; }

        [DataMember]
        [Column(title: "Пароль", width: 100)]
        public string Password { get; set; }
    }
}
