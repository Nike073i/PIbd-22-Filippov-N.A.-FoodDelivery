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
        public HomeController()
        {
        }
        public IActionResult Index()
        {
            if (!Program.authorized)
            {
                return Redirect("~/Home/Enter");
            }
            return
            View(APIClient.GetRequest<List<StoreViewModel>>("api/store/getstorelist"));
        }

        [HttpGet]
        public IActionResult Update()
        {
            ViewBag.Stores = APIClient.GetRequest<List<StoreViewModel>>("api/store/getstorelist");
            return View();
        }
        [HttpPost]
        public void Update(int store, string storeName, string fullNameResponsible)
        {
            if (!string.IsNullOrEmpty(storeName) && !string.IsNullOrEmpty(fullNameResponsible))
            {
                var currentStore = APIClient.GetRequest<StoreViewModel>($"api/store/getstore?storeId={store}");
                APIClient.PostRequest("api/store/deletestore", new StoreBindingModel
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
        public void Enter(string password, IConfiguration configuration)
        {
            if (!string.IsNullOrEmpty(password))
            {
                Program.authorized = configuration["Password"].Equals(password);
                if (Program.authorized)
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
            return View();
        }
        [HttpPost]
        public void Create(string storeName, string fullNameResponsible)
        {
            APIClient.PostRequest("api/store/createstore", new StoreBindingModel
            {
                StoreName = storeName,
                FullNameResponsible = fullNameResponsible
            });
            Response.Redirect("Index");
        }

        [HttpGet]
        public IActionResult Delete()
        {
            ViewBag.Stores = APIClient.GetRequest<List<StoreViewModel>>("api/store/getstorelist");
            return View();
        }
        [HttpPost]
        public void Delete(int store)
        {
            APIClient.PostRequest("api/store/deletestore", new StoreBindingModel
            {
                Id = store,
            });
            Response.Redirect("Index");
            return;
        }

        [HttpGet]
        public IActionResult Replenisment()
        {
            ViewBag.Stores = APIClient.GetRequest<List<StoreViewModel>>("api/store/getstorelist");
            return View();
        }
        [HttpPost]
        public void Replenisment(int store, int dish, int count)
        {
            if (count == 0)
            {
                return;
            }
            APIClient.PostRequest("api/store/adddishestostore", new AddDishesToStoreBindingModel
            {
                StoreId = store,
                DishId = dish,
                Count = count
            });
            Response.Redirect("Index");
        }
    }
}
