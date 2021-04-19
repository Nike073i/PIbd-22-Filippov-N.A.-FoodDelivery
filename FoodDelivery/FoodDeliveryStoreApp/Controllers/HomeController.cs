using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.ViewModels;
using FoodDeliveryStoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FoodDeliveryStoreApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;
        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IActionResult Index()
        {
            if (!Program.authorized)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<StoreViewModel>>("api/store/getstorelist"));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (!Program.authorized)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<StoreViewModel>($"api/store/getstore?storeId={id}"));
        }
        [HttpPost]
        public IActionResult UpdateAction(int store, string storeName, string fullNameResponsible)
        {
            if (!string.IsNullOrEmpty(storeName) && !string.IsNullOrEmpty(fullNameResponsible))
            {
                var currentStore = APIClient.GetRequest<StoreViewModel>($"api/store/getstore?storeId={store}");
                APIClient.PostRequest("api/store/createorupdatestore", new StoreBindingModel
                {
                    Id = store,
                    StoreName = storeName,
                    FullNameResponsible = fullNameResponsible,
                    CreationDate = currentStore.CreationDate,
                    StoreDishes = currentStore.StoreDishes
                });
                return Redirect("~/Home/Index");
            }
            throw new Exception("Введите название и ФИО ответственного");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore
        = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }

        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }
        [HttpPost]
        public void EnterAction(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                Program.authorized = configuration["Password"].Equals(password);
                if (!Program.authorized)
                {
                    throw new Exception("Неверный пароль");
                }
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите пароль");
        }
        [HttpGet]
        public IActionResult Create()
        {
            if (!Program.authorized)
            {
                return Redirect("~/Home/Enter");
            }
            return View();
        }
        [HttpPost]
        public void CreateAction(string storeName, string fullNameResponsible)
        {
            if (!string.IsNullOrEmpty(storeName) && !string.IsNullOrEmpty(fullNameResponsible))
            {
                APIClient.PostRequest("api/store/createorupdatestore", new StoreBindingModel
                {
                    StoreName = storeName,
                    FullNameResponsible = fullNameResponsible,
                    StoreDishes = new Dictionary<int, (string, int)>()
                });
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите название и ФИО ответственного");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!Program.authorized)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<StoreViewModel>($"api/store/getstore?storeId={id}"));
        }
        [HttpPost]
        public IActionResult DeleteAction(int store)
        {
            APIClient.PostRequest("api/store/deletestore", new StoreBindingModel
            {
                Id = store,
            });
            return Redirect("~/Home/Index");
        }

        [HttpGet]
        public IActionResult Replenisment()
        {
            if (!Program.authorized)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Stores = APIClient.GetRequest<List<StoreViewModel>>("api/store/getstorelist");
            ViewBag.Dishes = APIClient.GetRequest<List<DishViewModel>>("api/store/getdishlist");
            return View();
        }
        [HttpPost]
        public IActionResult ReplenismentAction(int store, int dish, int count)
        {
            if (store > 0 && dish > 0 && count > 0)
            {
                APIClient.PostRequest("api/store/adddishestostore", new AddDishesToStoreBindingModel
                {
                    StoreId = store,
                    DishId = dish,
                    Count = count
                });
            }
            return Redirect("~/Home/Index");
        }
    }
}
