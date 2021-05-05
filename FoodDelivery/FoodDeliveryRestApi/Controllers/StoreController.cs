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
        private readonly StoreLogic logicS;
        private readonly DishLogic logicD;
        public StoreController(StoreLogic logicS, DishLogic logicD)
        {
            this.logicS = logicS;
            this.logicD = logicD;
        }
        [HttpGet]
        public List<StoreViewModel> GetStoreList() => logicS.Read(null);
        [HttpGet]
        public StoreViewModel GetStore(int storeId) => logicS.Read(new StoreBindingModel
        {
            Id = storeId
        })?[0];
        [HttpGet]
        public List<DishViewModel> GetDishList() => logicD.Read(null);
        [HttpPost]
        public void DeleteStore(StoreBindingModel model) => logicS.Delete(model);
        [HttpPost]
        public void CreateOrUpdateStore(StoreBindingModel model) => logicS.CreateOrUpdate(model);
        [HttpPost]
        public void AddDishesToStore(AddDishesToStoreBindingModel model) => logicS.AddDishes(model);
    }
}
