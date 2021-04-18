using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.BusinessLogics;
using FoodDeliveryBusinnesLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FoodDeliveryRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly StoreLogic logic;
        public StoreController(StoreLogic logic)
        {
            this.logic = logic;
        }
        [HttpGet]
        public List<StoreViewModel> GetStoreList() => logic.Read(null);
        [HttpGet]
        public StoreViewModel GetStore(int storeId) => logic.Read(new StoreBindingModel
        {
            Id = storeId
        })?[0];
        [HttpPost]
        public void DeleteStore(StoreBindingModel model) => logic.Delete(model);
        [HttpPost]
        public void CreateOrUpdateStore(StoreBindingModel model) => logic.CreateOrUpdate(model);
        [HttpPost]
        public void AddDishesToStore(AddDishesToStoreBindingModel model) => logic.AddDishes(model);
    }
}
