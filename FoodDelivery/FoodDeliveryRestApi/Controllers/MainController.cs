using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.BusinessLogics;
using FoodDeliveryBusinnesLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FoodDeliveryRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly OrderLogic _order;
        private readonly SetLogic _set;
        public MainController(OrderLogic order, SetLogic set)
        {
            _order = order;
            _set = set;
        }
        [HttpGet]
        public List<SetViewModel> GetSetList() => _set.Read(null)?.ToList();
        [HttpGet]
        public SetViewModel GetSet(int setId) => _set.Read(new SetBindingModel
        {
            Id = setId
        })?[0];
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel
        {
            ClientId = clientId
        });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) => _order.CreateOrder(model);
    }
}
