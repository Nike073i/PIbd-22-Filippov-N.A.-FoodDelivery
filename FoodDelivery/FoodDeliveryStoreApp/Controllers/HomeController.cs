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
        public IActionResult Update()
        {
            if (!Program.authorized)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Stores = APIClient.GetRequest<List<StoreViewModel>>("api/store/getstorelist");
            return View();
        }
        [HttpPost]
        public void Update(int store, string storeName, string fullNameResponsible)
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
                    StoreDishes = currentStore.StoreDishes,
                });
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Выберите склад, введите название и ФИО ответственного");
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
        public void Enter(string password)
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
        public void Create(string storeName, string fullNameResponsible)
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
        public IActionResult Delete()
        {
            if (!Program.authorized)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Stores = APIClient.GetRequest<List<StoreViewModel>>("api/store/getstorelist");
            return View();
        }
        [HttpPost]
        public void Delete(int store)
        {
            if (store > 0)
            {
                APIClient.PostRequest("api/store/deletestore", new StoreBindingModel
                {
                    Id = store,
                });
            }
            Response.Redirect("Index");
        }

        [HttpGet]
        public IActionResult Replenisment()
        {
            if (!Program.authorized)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Stores = APIClient.GetRequest<List<StoreViewModel>>("api/store/getstorelist");
            ViewBag.Dishes = APIClient.GetRequest<List<DishViewModel>>("api/main/getdishlist");
            return View();
        }
        [HttpPost]
        public void Replenisment(int store, int dish, int count)
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
            Response.Redirect("Index");
        }
    }
}
