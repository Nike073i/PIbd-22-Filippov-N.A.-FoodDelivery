using FoodDeliveryBusinnesLogic.Attributes;
using System.Runtime.Serialization;

namespace FoodDeliveryBusinnesLogic.ViewModels
{
    [DataContract]
    public class ClientViewModel
    {
        [Column(title: "Номер", width: 100, visible: false)]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Column(title: "ФИО клиента", width: 150)]
        public string ClientFIO { get; set; }

        [DataMember]
        [Column(title: "Почта", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Email { get; set; }

        [DataMember]
        [Column(title: "Пароль", width: 150)]
        public string Password { get; set; }
    }
}
