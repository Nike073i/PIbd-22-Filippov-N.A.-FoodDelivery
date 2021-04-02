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
        private readonly OrderLogic _main;
        public MainController(OrderLogic order, SetLogic set, OrderLogic main)
        {
            _order = order;
            _set = set;
            _main = main;
        }
        [HttpGet]
        public List<SetViewModel> GetSetList() => _set.Read(null)?.ToList();
        [HttpGet]
        public SetViewModel GetSet(int productId) => _set.Read(new SetBindingModel
        {
            Id = productId
        })?[0];
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel
        {
            ClientId = clientId
        });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) => _main.CreateOrder(model);
    }
}
