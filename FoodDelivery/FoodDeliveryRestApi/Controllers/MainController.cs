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
        private readonly DishLogic _dish;
        public MainController(OrderLogic order, SetLogic set, DishLogic dish)
        {
            _order = order;
            _set = set;
            _dish = dish;
        }
        [HttpGet]
        public List<SetViewModel> GetSetList() => _set.Read(null)?.ToList();
        [HttpGet]
        public List<DishViewModel> GetDishList() => _dish.Read(null);
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
